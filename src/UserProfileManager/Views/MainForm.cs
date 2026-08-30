using Microsoft.Extensions.DependencyInjection;
using UserProfileManager.Models;
using UserProfileManager.Presenters;
using UserProfileManager.Services;

namespace UserProfileManager.Views;

public partial class MainForm : Form, IMainView
{
    private readonly IServiceProvider _serviceProvider;
    private readonly MainPresenter _presenter;

    private const int PageSize = 10;

    public event EventHandler<string>? SearchTextChanged;
    public event EventHandler? NextPageRequested;
    public event EventHandler? PreviousPageRequested;
    public event EventHandler<User>? DeleteUserRequested;

    public MainForm(IServiceProvider serviceProvider, IUserService userService)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;

        _presenter = new MainPresenter(this, userService);

        bindingSourceUsers.DataSource = new List<User>();

        txtSearch.TextChanged += TxtSearch_TextChanged;
        btnPrevious.Click += BtnPrevious_Click;
        btnNext.Click += BtnNext_Click;
        btnNewUser.Click += BtnNewUser_Click;
        dgvUsers.CellContentClick += DgvUsers_CellContentClick;

        Shown += MainForm_Shown;
    }

    private async void MainForm_Shown(object? sender, EventArgs e)
    {
        await _presenter.LoadUsersAsync(1, null);
    }

    private void TxtSearch_TextChanged(object? sender, EventArgs e)
    {
        SearchTextChanged?.Invoke(this, txtSearch.Text);
    }

    private void BtnPrevious_Click(object? sender, EventArgs e)
    {
        PreviousPageRequested?.Invoke(this, EventArgs.Empty);
    }

    private void BtnNext_Click(object? sender, EventArgs e)
    {
        NextPageRequested?.Invoke(this, EventArgs.Empty);
    }

    private async void BtnNewUser_Click(object? sender, EventArgs e)
    {
        using var userForm = _serviceProvider.GetRequiredService<UserForm>();

        userForm.UserId = null;

        var result = userForm.ShowDialog(this);

        if (result == DialogResult.OK)
        {
            await _presenter.RefreshAsync();
        }
    }

    private async void DgvUsers_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        if (e.ColumnIndex != colEdit.Index &&
            e.ColumnIndex != colDelete.Index)
        {
            return;
        }

        if (dgvUsers.Rows[e.RowIndex].DataBoundItem is not User user)
            return;

        if (e.ColumnIndex == colEdit.Index)
        {
            using var userForm =
                _serviceProvider.GetRequiredService<UserForm>();

            userForm.UserId = user.Id;
            userForm.FullName = user.FullName;
            userForm.Username = user.Username;
            userForm.Email = user.Email;
            userForm.UserInfo = user.UserInfo;
            userForm.LinkedInProfile = user.LinkedInProfile;

            var result = userForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                await _presenter.RefreshAsync();
            }
        }
        else if (e.ColumnIndex == colDelete.Index)
        {
            DeleteUserRequested?.Invoke(this, user);
        }
    }

    public void DisplayUsers(IReadOnlyList<User> users)
    {
        bindingSourceUsers.DataSource = users;
    }

    public void SetPagingInfo(int currentPage, int totalPages,  int totalCount)
    {
        int start = totalCount == 0
            ? 0
            : ((currentPage - 1) * PageSize) + 1;

        int end = Math.Min(
            currentPage * PageSize,
            totalCount);

        lblPagingInfo.Text =
            $"Showing {start} - {end} of {totalCount}";

        lblPageStatus.Text =
            $"Page {currentPage} of {Math.Max(totalPages, 1)}";
    }

    public void ShowError(string message)
    {
        MessageBox.Show( this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public bool ConfirmDelete(string userDisplayName)
    {
        return MessageBox.Show( this, $"Delete {userDisplayName}? This cannot be undone.", "Confirm Delete",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
    }
}