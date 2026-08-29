namespace UserProfileManager.Data
{
    public class DatabaseInitializer
    {
        private readonly SqliteConnectionFactory _connectionFactory;
        public DatabaseInitializer(SqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task InitializeDatabaseAsync()
        {
            await using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText =
            @"
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
                ";
            await command.ExecuteNonQueryAsync();
        }
    }
}
