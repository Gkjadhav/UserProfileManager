using UserProfileManager.Models;

namespace UserProfileManager.Views
{
    public interface IUserFormView
    {
        int? UserId { get; set; }
        string FullName { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        string? UserInfo { get; set; }
        string? LinkedInProfile {  get; set; }


        void ShowValidationErrors(IReadOnlyList<string> errors);
        void CloseSuccessfully();


        event EventHandler? SaveRequested;
    }
}
