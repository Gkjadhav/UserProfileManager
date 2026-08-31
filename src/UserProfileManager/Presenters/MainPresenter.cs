using UserProfileManager.Models;
using UserProfileManager.Services;
using UserProfileManager.Views;

namespace UserProfileManager.Presenters;

public class MainPresenter
{
    private readonly IMainView _view;
    private readonly IUserService _userService;

    private int _currentPage = 1;
    private int _pageSize = 10;
    private string? _searchText;
    private int _totalPages = 1;

    public MainPresenter(IMainView view, IUserService userService)
    {
        _view = view;
        _userService = userService;

        _view.SearchTextChanged += OnSearchTextChanged;
        _view.NextPageRequested += OnNextPageRequested;
        _view.PreviousPageRequested += OnPreviousPageRequested;
        _view.DeleteUserRequested += OnDeleteUserRequested;
    }

    public async Task RefreshAsync()
    {
        await LoadUsersAsync(_currentPage, _searchText);
    }

    public async Task SetPageSizeAsync(int pageSize)
    {
        if (pageSize < 1 || pageSize == _pageSize)
            return;

        _pageSize = pageSize;
        _currentPage = 1;
        await LoadUsersAsync(_currentPage, _searchText);
    }

    public async Task LoadUsersAsync(int page, string? search)
    {
        try
        {
            var result = await _userService.GetUsersAsync( page, _pageSize, search);

            if (!result.Success)
            {
                _view.ShowError(string.Join(Environment.NewLine, result.Errors ?? []));
                return;
            }

            var pagedResult = result.Value!;

            _currentPage = pagedResult.PageNumber;
            _totalPages = pagedResult.TotalPages;
            _searchText = search;

            _view.DisplayUsers(pagedResult.Items);

            _view.SetPagingInfo(
                pagedResult.PageNumber,
                pagedResult.TotalPages,
                pagedResult.TotalCount);
        }
        catch (Exception)
        {
            _view.ShowError("Unable to load users. Please try again.");
        }
    }

    private async void OnSearchTextChanged(object? sender,string searchText)
    {
        _currentPage = 1;
        await LoadUsersAsync( _currentPage, searchText);
    }

    private async void OnNextPageRequested(object? sender, EventArgs e)
    {
        if (_currentPage >= _totalPages) return;
        await LoadUsersAsync(_currentPage + 1, _searchText);
    }

    private async void OnPreviousPageRequested(object? sender, EventArgs e)
    {
        if (_currentPage <= 1) return;

        await LoadUsersAsync(_currentPage - 1,  _searchText);
    }

    private async void OnDeleteUserRequested( object? sender, User user)
    {
        try
        {
            if (!_view.ConfirmDelete(user.FullName)) return;

            var result = await _userService.DeleteUserAsync(user.Id);

            if (!result.Success)
            {
                _view.ShowError(string.Join(Environment.NewLine, result.Errors ?? []));
                return;
            }

            await LoadUsersAsync( _currentPage, _searchText);
        }
        catch (Exception)
        {
            _view.ShowError("Unable to delete the user. Please try again.");
        }
    }
}