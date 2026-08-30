using UserProfileManager.Data;
using Microsoft.Extensions.DependencyInjection;
using UserProfileManager.Repositories;
using UserProfileManager.Services;
using UserProfileManager.Views;

namespace UserProfileManager;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var dataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "UserProfileManager", "Data");

        Directory.CreateDirectory(dataFolder);

        var dbPath = Path.Combine(dataFolder, "users.db");
        var connectionString = $"Data Source={dbPath};";

        var connectionFactory = new SqliteConnectionFactory(connectionString);
        var databaseInitializer = new DatabaseInitializer(connectionFactory);

        try
        {
            databaseInitializer.InitializeDatabaseAsync().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"The application could not start because the local database could not be initialized.\n\n{ex.Message}",
                "Startup Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        var services = new ServiceCollection();
        services.AddSingleton(connectionFactory);
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IUserService, UserService>();
        services.AddTransient<MainForm>();
        services.AddTransient<UserForm>();

        using var serviceProvider = services.BuildServiceProvider();

        Application.Run(serviceProvider.GetRequiredService<MainForm>());
    }    
}