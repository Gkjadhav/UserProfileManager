using UserProfileManager.Models;

namespace UserProfileManager.Views
{
    public interface IMainView
    {
        void DisplayUsers(IReadOnlyList<User> users);
        void SetPagingInfo(int currentPage, int totalPages, int totalCount);
        void ShowError(string message);
        bool ConfirmDelete(string userDisplayName);


        event EventHandler<string>? SearchTextChanged;
        event EventHandler? NextPageRequested;
        event EventHandler? PreviousPageRequested;
        event EventHandler<User>? DeleteUserRequested;
    }
}
