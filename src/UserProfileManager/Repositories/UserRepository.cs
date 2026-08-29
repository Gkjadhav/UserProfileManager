using Microsoft.Data.Sqlite;
using System.Globalization;
using UserProfileManager.Data;
using UserProfileManager.Models;

namespace UserProfileManager.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqliteConnectionFactory _sqliteConnectionFactory;

        public UserRepository(SqliteConnectionFactory sqliteConnectionFactory)  
        {
            _sqliteConnectionFactory = sqliteConnectionFactory;
        }

        public async Task<User> AddAsync(User user)
        {
            await using var connection = _sqliteConnectionFactory.CreateConnection();
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Users (FullName, Username, Email, UserInfo, LinkedInProfile, CreatedAt, UpdatedAt)
                VALUES ($fullName, $username, $email, $userInfo, $linkedInProfile, $createdAt, $updatedAt);";
           
            command.Parameters.AddWithValue("$fullName", user.FullName);
            command.Parameters.AddWithValue("$username", user.Username);
            command.Parameters.AddWithValue("$email", user.Email);
            command.Parameters.AddWithValue("$userInfo", user.UserInfo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("$linkedInProfile", user.LinkedInProfile ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("$createdAt", user.CreatedAt.ToString("o"));
            command.Parameters.AddWithValue("$updatedAt", user.UpdatedAt?.ToString("o") ?? (object)DBNull.Value);

            await command.ExecuteNonQueryAsync();

            await using var idCommand = connection.CreateCommand();

            idCommand.CommandText = "SELECT last_insert_rowid();";

            var result = await idCommand.ExecuteScalarAsync();

            user.Id = Convert.ToInt32(result);

            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var connection = _sqliteConnectionFactory.CreateConnection();
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Users WHERE Id = $id;";

            command.Parameters.AddWithValue("$id", id);

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<IReadOnlyList<User>> GetPageAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            await using var connection = _sqliteConnectionFactory.CreateConnection();
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = $@"SELECT * FROM Users 
                                    {BuildSearchClause(searchTerm, command)}
                                    ORDER BY Id
                                    LIMIT $limit
                                    OFFSET $offset;";

            command.Parameters.AddWithValue("$limit", pageSize);
            command.Parameters.AddWithValue("$offset", (pageNumber - 1) * pageSize);

            var users = new List<User>();
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var user = MapUser(reader);
                users.Add(user);
            }

            return users;
        }
        private static string BuildSearchClause(string? searchTerm, SqliteCommand command)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) //explicity states what we are doing instead of usign %%
                return string.Empty;

            command.Parameters.AddWithValue("$search", $"%{searchTerm}%");
            return "WHERE FullName LIKE $search OR Username LIKE $search OR Email LIKE $search";
        }
        private static User MapUser(SqliteDataReader reader)
        {
            int id = reader.GetOrdinal("Id");
            int fullName = reader.GetOrdinal("FullName");
            int username = reader.GetOrdinal("Username");
            int email = reader.GetOrdinal("Email");
            int userInfo = reader.GetOrdinal("UserInfo");
            int linkedInProfile = reader.GetOrdinal("LinkedInProfile");
            int createdAt = reader.GetOrdinal("CreatedAt");
            int updatedAt = reader.GetOrdinal("UpdatedAt");

            return new User
            {
                Id = reader.GetInt32(id),

                FullName = reader.GetString(fullName),

                Username = reader.GetString(username),

                Email = reader.GetString(email),

                UserInfo = reader.IsDBNull(userInfo)
                    ? null
                    : reader.GetString(userInfo),

                LinkedInProfile = reader.IsDBNull(linkedInProfile)
                    ? null
                    : reader.GetString(linkedInProfile),

                CreatedAt = DateTime.Parse(
                    reader.GetString(createdAt),
                    null,
                    DateTimeStyles.RoundtripKind),

                UpdatedAt = reader.IsDBNull(updatedAt)
                    ? null
                    : DateTime.Parse(
                        reader.GetString(updatedAt),
                        null,
                        DateTimeStyles.RoundtripKind)
            };
        }

        public async Task<int> GetTotalCountAsync(string? searchTerm = null)
        {
            await using var connection = _sqliteConnectionFactory.CreateConnection();
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = $@"SELECT COUNT(*) FROM Users 
                                    {BuildSearchClause(searchTerm, command)}";
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            await using var connection = _sqliteConnectionFactory.CreateConnection();
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Users
                SET FullName = $fullName,
                    Username = $username,
                    Email = $email,
                    UserInfo = $userInfo,
                    LinkedInProfile = $linkedInProfile,
                    UpdatedAt = $updatedAt
                WHERE Id = $id;";

            command.Parameters.AddWithValue("$fullName", user.FullName);
            command.Parameters.AddWithValue("$username", user.Username);
            command.Parameters.AddWithValue("$email", user.Email);
            command.Parameters.AddWithValue("$userInfo", user.UserInfo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("$linkedInProfile", user.LinkedInProfile ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("$updatedAt", user.UpdatedAt?.ToString("o") ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("$id", user.Id);

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> UsernameExistsAsync(string username, int? excludeUserId = null)
        {
            await using var connection = _sqliteConnectionFactory.CreateConnection();
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = @"
                                    SELECT EXISTS(
                                        SELECT 1 FROM Users
                                        WHERE Username = $username
                                            AND ($excludeId IS NULL OR Id != $excludeId)
                                     );";

            command.Parameters.AddWithValue("$username", username);

            command.Parameters.AddWithValue(
                "$excludeId",
                excludeUserId.HasValue
                    ? (object)excludeUserId.Value
                    : DBNull.Value);

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt64(result) == 1;
        }
    }
}
