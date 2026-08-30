using UserProfileManager.Data;
using UserProfileManager.Models;
using UserProfileManager.Repositories;
using UserProfileManager.Services;

namespace UserProfileManager.Tests.Services;

public class UserServiceTests : IDisposable
{
    private readonly string _dbPath;
    private readonly UserService _sut;
    private readonly UserRepository _repository;

    public UserServiceTests()
    {
        _dbPath = Path.Combine( Path.GetTempPath(), $"upm_test_{Guid.NewGuid()}.db");

        var connectionFactory = new SqliteConnectionFactory($"Data Source={_dbPath}");

        var databaseInitializer = new DatabaseInitializer(connectionFactory);

        databaseInitializer.InitializeDatabaseAsync().GetAwaiter().GetResult();

        _repository = new UserRepository(connectionFactory);
        _sut = new UserService(_repository);
    }

    [Fact]
    public async Task AddUserAsync_WithDuplicateUsername_ReturnsFailure()
    {
        // Arrange
        var firstUser = new User
        {
            FullName = "John Doe",
            Username = "johndoe",
            Email = "john@example.com",
            UserInfo = "Developer",
            LinkedInProfile = "https://www.linkedin.com/in/johndoe"
        };

        var duplicateUser = new User
        {
            FullName = "Jane Doe",
            Username = "johndoe",
            Email = "jane@example.com",
            UserInfo = "Tester",
            LinkedInProfile = "https://www.linkedin.com/in/janedoe"
        };

        await _sut.AddUserAsync(firstUser);

        // Act
        var result = await _sut.AddUserAsync(duplicateUser);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task UpdateUserAsync_WithValidUser_PersistsChanges()
    {
        // Arrange
        var user = new User
        {
            FullName = "John Doe",
            Username = "johndoe",
            Email = "john@example.com",
            UserInfo = "Developer",
            LinkedInProfile = "https://www.linkedin.com/in/johndoe"
        };

        await _sut.AddUserAsync(user);

        user.FullName = "John Updated";
        user.Email = "john.updated@example.com";
        user.UserInfo = "Senior Developer";

        // Act
        var result = await _sut.UpdateUserAsync(user);

        // Assert
        Assert.True(result.Success);

        // Verify persistence
        var users = await _repository.GetPageAsync(1, 10);

        var updatedUser = Assert.Single(users);

        Assert.Equal("John Updated", updatedUser.FullName);
        Assert.Equal("john.updated@example.com", updatedUser.Email);
        Assert.Equal("Senior Developer", updatedUser.UserInfo);
    }

    [Fact]
    public async Task DeleteUserAsync_WithExistingUser_RemovesUser()
    {
        // Arrange
        var user = new User
        {
            FullName = "John Doe",
            Username = "johndoe",
            Email = "john@example.com",
            UserInfo = "Developer",
            LinkedInProfile = "https://www.linkedin.com/in/johndoe"
        };

        await _sut.AddUserAsync(user);

        // Act
        var result = await _sut.DeleteUserAsync(user.Id);

        // Assert
        Assert.True(result.Success);

        // Verify deletion
        var users = await _repository.GetPageAsync(1, 10);

        Assert.Empty(users);
    }

    [Fact]
    public async Task GetUsersAsync_WithSearchTerm_ReturnsOnlyMatchingUsers()
    {
        // Arrange
        var alice = new User
        {
            FullName = "Alice Smith",
            Username = "asmith",
            Email = "alice@example.com",
            UserInfo = "Developer",
            LinkedInProfile = "https://www.linkedin.com/in/alice"
        };

        var bob = new User
        {
            FullName = "Bob Jones",
            Username = "bjones",
            Email = "bob@example.com",
            UserInfo = "Tester",
            LinkedInProfile = "https://www.linkedin.com/in/bob"
        };

        await _sut.AddUserAsync(alice);
        await _sut.AddUserAsync(bob);

        // Act
        var result = await _sut.GetUsersAsync(1, 10, "alice");

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Single(result.Value.Items);

        var user = result.Value.Items[0];

        Assert.Equal("Alice Smith", user.FullName);
        Assert.Equal("asmith", user.Username);
    }

    [Fact]
    public async Task UpdateUserAsync_WithUnchangedUsername_DoesNotFlagAsDuplicate()
    {
        // Arrange
        var user = new User
        {
            FullName = "John Doe",
            Username = "johndoe",
            Email = "john@example.com",
            UserInfo = "Developer",
            LinkedInProfile = "https://www.linkedin.com/in/johndoe"
        };

        await _sut.AddUserAsync(user);

        // Change only the name.
        // Username remains "johndoe".
        user.FullName = "John Updated";

        // Act
        var result = await _sut.UpdateUserAsync(user);

        // Assert
        Assert.True(result.Success);
    }

    public void Dispose()
    {
        for (int attempt = 0; attempt < 5; attempt++)
        {
            try
            {
                if (File.Exists(_dbPath))
                {
                    File.Delete(_dbPath);
                }

                return;
            }
            catch (IOException)
            {
                Thread.Sleep(100);
            }
        }
    }
}