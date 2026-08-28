namespace UserProfileManager.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? UserInfo { get; set; }
        public string? LinkedInProfile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
