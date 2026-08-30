using UserProfileManager.Models;
using UserProfileManager.Services;
using UserProfileManager.Views;

namespace UserProfileManager.Presenters;

public class UserPresenter
{
    private readonly IUserFormView _view;
    private readonly IUserService _userService;

    public UserPresenter(IUserFormView view, IUserService userService)
    {
        _view = view;
        _userService = userService;

        _view.SaveRequested += OnSaveRequested;
    }

    private async void OnSaveRequested(object? sender, EventArgs e)
    {
        try
        {
            var user = new User
            {
                Id = _view.UserId ?? 0,
                FullName = _view.FullName,
                Username = _view.Username,
                Email = _view.Email,
                UserInfo = _view.UserInfo,
                LinkedInProfile = _view.LinkedInProfile
            };

            ServiceResult<User> result;

            if (_view.UserId.HasValue)
            {
                result = await _userService.UpdateUserAsync(user);
            }
            else
            {
                result = await _userService.AddUserAsync(user);
            }

            if (!result.Success)
            {
                _view.ShowValidationErrors(result.Errors ?? []);
                return;
            }

            _view.CloseSuccessfully();
        }
        catch (Exception)
        {
            _view.ShowValidationErrors(["Unable to save the user. Please try again."]);
        }
    }
}