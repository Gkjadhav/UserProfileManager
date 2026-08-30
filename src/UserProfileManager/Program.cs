using Microsoft.Extensions.DependencyInjection;
using UserProfileManager.Data;
using UserProfileManager.Repositories;
using UserProfileManager.Services;
using UserProfileManager.Utilities;
using UserProfileManager.Views;

namespace UserProfileManager;

internal static class Program
{
    private const string MutexName = @"Global\UserProfileManager_SingleInstance";
    private const string ApplicationTitle = "UserProfileManager";

    [STAThread]
    private static void Main()
    {
        using var mutex = new Mutex(true, MutexName, out bool isNewInstance);

        if (!isNewInstance)
        {
            WindowHelper.BringToFront(ApplicationTitle);
            return;
        }

        ApplicationConfiguration.Initialize();

        if (!InitializeDatabase(out var connectionFactory))
            return;

        using var serviceProvider = ConfigureServices(connectionFactory);

        Application.Run(serviceProvider.GetRequiredService<MainForm>());
    }

    private static bool InitializeDatabase(out SqliteConnectionFactory connectionFactory)
    {
        var dbPath = GetDatabasePath();
        var connectionString = $"Data Source={dbPath};";

        connectionFactory = new SqliteConnectionFactory(connectionString);
        var databaseInitializer = new DatabaseInitializer(connectionFactory);

        try
        {
            databaseInitializer.InitializeDatabaseAsync().GetAwaiter().GetResult();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"The application could not start because the local database could not be initialized.\n\n{ex.Message}",
                "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }
    }

    private static string GetDatabasePath()
    {
        var dataFolder = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            ApplicationTitle, "Data");

        Directory.CreateDirectory(dataFolder);

        return Path.Combine(dataFolder, "users.db");
    }

    private static ServiceProvider ConfigureServices(SqliteConnectionFactory connectionFactory)
    {
        var services = new ServiceCollection();

        services.AddSingleton(connectionFactory);
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IUserService, UserService>();
        services.AddTransient<MainForm>();
        services.AddTransient<UserForm>();

        return services.BuildServiceProvider();
    }
}