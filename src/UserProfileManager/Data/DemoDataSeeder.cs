namespace UserProfileManager.Data
{
    // Seeds a fixed set of demo users on first run (only when the Users table is empty), so a
    // fresh install already has enough rows to exercise search and pagination without asking
    // whoever installs it to add data by hand or drop a users.db file into place manually.
    public class DemoDataSeeder
    {
        private readonly SqliteConnectionFactory _connectionFactory;

        private static readonly string[] Names =
        [
            "Aarav Sharma", "Priya Patel", "Rohan Mehta", "Ananya Iyer", "Vikram Nair",
            "Sneha Reddy", "Karan Malhotra", "Ishita Gupta", "Aditya Verma", "Neha Kapoor",
            "Arjun Rao", "Divya Menon", "Siddharth Joshi", "Pooja Desai", "Rahul Chawla",
            "Kavya Pillai", "Manish Agarwal", "Riya Bansal", "Vivek Choudhary", "Meera Krishnan",
            "Nikhil Saxena", "Tanya Bhatt", "Amitabh Sinha", "Sanya Kohli", "Harsh Vaidya",
            "Lakshmi Subramaniam", "Yash Trivedi", "Simran Chopra", "Devansh Rathi", "Anjali Nambiar",
        ];

        private static readonly string[] Bios =
        [
            "Backend engineer focused on distributed systems.",
            "Full-stack developer, loves clean architecture.",
            "QA lead specializing in test automation.",
            "Product manager with a background in engineering.",
            "DevOps engineer, CI/CD pipelines and cloud infra.",
            "UI/UX designer with a front-end engineering bent.",
            "Data engineer working on ETL pipelines.",
            "Mobile developer, Android and iOS.",
            "Security engineer, application security focus.",
            "Engineering manager, ex-individual contributor.",
        ];

        public DemoDataSeeder(SqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task SeedIfEmptyAsync()
        {
            await using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            await using (var countCommand = connection.CreateCommand())
            {
                countCommand.CommandText = "SELECT COUNT(*) FROM Users;";
                var existingCount = Convert.ToInt64(await countCommand.ExecuteScalarAsync());

                if (existingCount > 0)
                    return;
            }

            var random = new Random(42);
            var baseDate = DateTime.UtcNow.AddDays(-60);

            for (int i = 0; i < Names.Length; i++)
            {
                string fullName = Names[i];
                string slug = fullName.ToLowerInvariant().Replace(" ", ".");
                string username = slug;
                string email = $"{slug}@example.com";
                string userInfo = Bios[i % Bios.Length];
                string linkedInProfile = $"https://www.linkedin.com/in/{slug.Replace(".", "-")}";

                var createdAt = DateTime.SpecifyKind(
                    baseDate.AddDays(random.Next(0, 60)).AddHours(random.Next(0, 24)),
                    DateTimeKind.Utc);

                DateTime? updatedAt = i % 3 == 0
                    ? DateTime.SpecifyKind(createdAt.AddDays(random.Next(1, 20)), DateTimeKind.Utc)
                    : null;

                await using var insert = connection.CreateCommand();
                insert.CommandText = @"
                    INSERT INTO Users (FullName, Username, Email, UserInfo, LinkedInProfile, CreatedAt, UpdatedAt)
                    VALUES ($fullName, $username, $email, $userInfo, $linkedInProfile, $createdAt, $updatedAt);";

                insert.Parameters.AddWithValue("$fullName", fullName);
                insert.Parameters.AddWithValue("$username", username);
                insert.Parameters.AddWithValue("$email", email);
                insert.Parameters.AddWithValue("$userInfo", userInfo);
                insert.Parameters.AddWithValue("$linkedInProfile", linkedInProfile);
                insert.Parameters.AddWithValue("$createdAt", createdAt.ToString("o"));
                insert.Parameters.AddWithValue("$updatedAt", updatedAt?.ToString("o") ?? (object)DBNull.Value);

                await insert.ExecuteNonQueryAsync();
            }
        }
    }
}
