# UserProfileManager

![.NET CI](https://github.com/Gkjadhav/UserProfileManager/actions/workflows/dotnet-ci.yml/badge.svg)
![CodeQL](https://github.com/Gkjadhav/UserProfileManager/actions/workflows/codeql.yml/badge.svg)

A Windows desktop application for managing user profiles — create, edit, delete, search, and
paginate through user records, backed by a local SQLite database. Built with C# / .NET 10 /
WinForms, following a layered architecture (UI → Service → Repository → SQLite) with the UI
layer implemented as MVP (Model-View-Presenter) for testability.

Packaged and distributed as an MSIX package — see [Installation](#installation).

## Features

- **Create / Edit / Delete** user profiles (Full Name, Username, Email, User Info, LinkedIn Profile)
- **Search** across name, username, and email
- **Pagination** over the results grid (page size fixed, Previous/Next controls)
- **Validation**: required fields, email format, LinkedIn URL format, username/email uniqueness
  — enforced both in application code (friendly error messages) and at the database level
  (`UNIQUE` constraints, race-condition-proof)
- **Single-instance enforcement** — launching a second copy brings the existing window to the
  foreground instead of opening a duplicate
- Local persistence via SQLite, stored under `%LOCALAPPDATA%\UserProfileManager\Data\users.db`
- Packaged as an installable **MSIX** application

## Technology Stack

| Layer | Technology |
|---|---|
| UI | WinForms (.NET 10), MVP pattern |
| Business logic | C#, custom `ServiceResult<T>` / `PagedResult<T>` result types |
| Data access | `Microsoft.Data.Sqlite`, parameterized SQL (no ORM) |
| Dependency Injection | `Microsoft.Extensions.DependencyInjection` |
| Validation | `System.ComponentModel.DataAnnotations` + custom validators |
| Testing | xUnit, real temp-file SQLite (no ADO.NET mocking) |
| CI/CD | GitHub Actions, Dependabot, CodeQL |
| Packaging | MSIX (Windows Application Packaging Project), self-signed certificate |

## Architecture

```
View (WinForms Form, implements IMainView / IUserFormView)
   │
   ▼
Presenter (MainPresenter / UserPresenter — UI-facing logic, unit-testable via fake views)
   │
   ▼
Service (IUserService — validation orchestration, business rules)
   │
   ▼
Repository (IUserRepository — parameterized SQL, row-to-model mapping)
   │
   ▼
SQLite (SQLiteConnectionFactory) — %LOCALAPPDATA%\UserProfileManager\Data\users.db
```

The UI never touches SQL directly. Each layer only knows about the one below it through an
interface, which is what makes the Service and Presenter layers testable without a real database
or real WinForms controls.

**MVP over MVVM**: WinForms lacks WPF's native two-way data-binding infrastructure that MVVM is
built around, so a straight MVP split (a thin interface per view, a presenter that drives it) fits
the framework better than forcing MVVM onto it.

## Database

```sql
CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Username TEXT NOT NULL UNIQUE,
    Email TEXT NOT NULL UNIQUE,
    UserInfo TEXT,
    LinkedInProfile TEXT,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT
);
```

- SQLite chosen over SQL Server: this is a single-user local desktop app — no server process, no
  connection string management, ships as a single file. `Microsoft.Data.Sqlite` is used directly
  rather than an ORM since the query surface is small and the mapping is trivial by hand.
- `Username` and `Email` are both `UNIQUE` at the database level, not just checked in code — the
  DB constraint is the actual race-condition-proof guarantee; the application-level check exists
  purely to give a friendly error message before the DB is even hit.
- All queries are parameterized — no string-concatenated SQL anywhere, by construction rules out
  SQL injection on this surface.
- Dates are stored as ISO-8601 `TEXT` (SQLite has no native datetime type) and are set explicitly
  in the Service layer (`DateTime.UtcNow`), not via a DB default, so timestamp logic stays
  testable and every insert is explicit.
- The database file lives under `%LOCALAPPDATA%\UserProfileManager\Data\`, never inside the
  install directory — install directories aren't guaranteed writable, and binaries vs. mutable
  user data have fundamentally different lifecycles (an uninstall/upgrade shouldn't touch data).

## Getting Started

**Prerequisites**: [.NET 10 SDK](https://dotnet.microsoft.com/download) (pinned via `global.json`),
Windows (WinForms is Windows-only).

```bash
git clone https://github.com/Gkjadhav/UserProfileManager.git
cd UserProfileManager
dotnet restore src/UserProfileManager/UserProfileManager.csproj
dotnet build src/UserProfileManager/UserProfileManager.csproj --configuration Release
dotnet run --project src/UserProfileManager/UserProfileManager.csproj
```

The SQLite database is created automatically on first run at
`%LOCALAPPDATA%\UserProfileManager\Data\users.db`.

## Running Tests

```bash
dotnet test tests/UserProfileManager.Tests/UserProfileManager.Tests.csproj
```

13 tests covering validation (valid user, missing required field, invalid email/LinkedIn URL via
`[Theory]`) and service behavior (duplicate username rejection, update persists, delete persists,
search, edit-excludes-self on the uniqueness check). Tests run against a real temporary SQLite
file per test class (`IDisposable` fixture, cleaned up after each run) rather than mocking
`Microsoft.Data.Sqlite` — the repository's SQL is simple enough that a real (if temporary)
database gives more confidence than a mock would.

## CI/CD

[`dotnet-ci.yml`](.github/workflows/dotnet-ci.yml) runs on every push and PR to `main`:
checkout → setup .NET 10 → restore → build → test (with code coverage collection), on a
`windows-latest` runner (required — WinForms doesn't build on Linux runners).

Every restore/build/test step targets the two real `.csproj` files explicitly rather than the
`.slnx` solution. This matters: the solution also contains the MSIX packaging project
(`.wapproj`), and the `dotnet` CLI can never build a `.wapproj` on any machine — it imports
MSBuild targets (`Microsoft.DesktopBridge.props`) that only exist under a full Visual Studio
install, not under the .NET SDK. Letting CI touch the solution file directly breaks the build with
an `MSB4019` import error; scoping every command to the actual project files avoids the packaging
project entirely, and MSIX packaging stays a manual, VS-driven step (see below).

Branch protection on `main` requires the `build-and-test` status check, blocks force-push, and
requires PR conversation resolution. All feature work happens on a `feature/*` branch, merged via
squash-merge PR — that's what keeps `main`'s history one clean commit per feature despite messy
in-progress commits on the branch itself.

Also enabled: **Dependabot** alerts (NuGet dependency CVE scanning) and **CodeQL** static analysis
— both free for a public repo, zero ongoing maintenance cost.

## MSIX Packaging & Installation

The app is packaged via a separate **Windows Application Packaging Project**
(`packaging/UserProfileManager.Package`), signed with a self-signed certificate. Packaging is a
manual step done from Visual Studio (**Publish → Create App Packages...**), not automated in CI —
see [CI/CD](#cicd) for why, and `PLAN.md` for the full reasoning.

**To install the app** (no Visual Studio or source needed): grab the latest release —

**[Releases → v1.0.0](https://github.com/Gkjadhav/UserProfileManager/releases/tag/v1.0.0)**

Two install paths are documented on the release page:
- **One-click**: extract the zip, right-click `Add-AppDevPackage.ps1` → *Run with PowerShell*. This
  trusts the self-signed certificate and installs the app in one step.
- **Manual**: install the included `.cer` into `Local Machine → Trusted People` yourself, then
  double-click the `.msixbundle` (or the standalone `x64.msix`/`x86.msix`) to install — useful if
  you'd rather not run a script from an unfamiliar source, which is a completely reasonable call.

A self-signed certificate only makes the package installable on a machine that has explicitly
chosen to trust it — this is a demo/interview convenience, not how a real product would ship. A
production release would use a CA-issued or enterprise code-signing certificate so the OS trusts
the publisher without a manual import step.

## Design Decisions

A few choices worth calling out beyond what's already justified inline above:

- **Dependency Injection via `Microsoft.Extensions.DependencyInjection`**, wired entirely in
  `Program.cs` as the composition root. `SQLiteConnectionFactory`/`IUserRepository`/`IUserService`
  are Singletons (one instance for the app's lifetime is correct — they're stateless or own a
  single connection factory); Presenters are Transient (fresh per form instance).
- **No `App.config`/`ConfigurationManager`** for the connection string — the SQLite path is a
  per-user runtime value computed from `%LOCALAPPDATA%`, not a static value that differs by
  deployment environment. Config-file connection strings earn their keep when the same code must
  point at genuinely different servers per environment (e.g. SQL Server Dev/Test/Prod) — that's
  not this app's situation.
- **A single justified `Mutex`** for single-instance enforcement — checked first thing in `Main`,
  named with a `Global\` prefix. The concrete risk it prevents: two processes writing the same
  SQLite file at once. Nothing else in the app uses `lock`/`Semaphore`; no other concrete race
  condition exists to justify one.
- **No migrations framework** — one fixed table, `CREATE TABLE IF NOT EXISTS` run idempotently at
  startup. Schema versioning would be real overhead for a schema that isn't changing.
- **Design patterns considered and rejected**: Abstract Factory (no family of interchangeable
  objects exists here), a hand-rolled Singleton class (superseded by the DI container's lifetime
  management), broad locking (no shared-mutable-state race condition beyond the one above).
  Knowing when *not* to reach for a pattern was treated as equally important as knowing the
  pattern itself.

## Future Improvements

Deliberately out of scope for this project, but worth naming to show they were considered:

- Swapping SQLite for a SQL Server backend (the repository interface is already the seam for this)
- Authentication/authorization (currently a single-user local app)
- Structured logging/telemetry
- A CA-issued or enterprise code-signing certificate for real distribution
- Automating the MSIX build/sign/release pipeline in GitHub Actions (deferred deliberately — see
  [CI/CD](#cicd) — this needs the signing `.pfx`/password handled as encrypted CI secrets, real
  added complexity worth tackling only after the manual flow is solid)
- A schema migrations framework, if the schema ever needs to evolve post-release

## Project Structure

```
UserProfileManager.slnx
global.json
src/UserProfileManager/
    Views/          MainForm, UserForm + IMainView/IUserFormView
    Presenters/      MainPresenter, UserPresenter
    Models/          User
    Data/            SQLiteConnectionFactory, DatabaseInitializer
    Repositories/    IUserRepository, UserRepository
    Services/        IUserService, UserService, ServiceResult<T>, PagedResult<T>
    Validators/      UserValidator
    Utilities/       UrlValidator, WindowHelper
    Program.cs       Composition root — DI container, DB init, single-instance guard
tests/UserProfileManager.Tests/
packaging/UserProfileManager.Package/   MSIX packaging project (.wapproj)
.github/workflows/  dotnet-ci.yml, codeql.yml
```
