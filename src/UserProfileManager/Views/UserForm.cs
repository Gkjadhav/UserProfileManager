using System.ComponentModel;
using UserProfileManager.Presenters;
using UserProfileManager.Services;

namespace UserProfileManager.Views;

public partial class UserForm : Form, IUserFormView
{
    private readonly UserPresenter _presenter;
    private int? _userId;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? UserId
    {
        get => _userId;
        set
        {
            _userId = value;
            ApplyModeToTitle();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FullName
    {
        get => txtFullName.Text;
        set => txtFullName.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Username
    {
        get => txtUsername.Text;
        set => txtUsername.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Email
    {
        get => txtEmail.Text;
        set => txtEmail.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? UserInfo
    {
        get => string.IsNullOrWhiteSpace(txtUserInfo.Text) ? null : txtUserInfo.Text;
        set => txtUserInfo.Text = value ?? string.Empty;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? LinkedInProfile
    {
        get => string.IsNullOrWhiteSpace(txtLinkedIn.Text) ? null : txtLinkedIn.Text;
        set => txtLinkedIn.Text = value ?? string.Empty;
    }

    public event EventHandler? SaveRequested;

    public UserForm(IUserService userService)
    {
        InitializeComponent();

        _presenter = new UserPresenter(this, userService);

        btnSave.Click += BtnSave_Click;
        btnCancel.Click += BtnCancel_Click;
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        SaveRequested?.Invoke(this, EventArgs.Empty);
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void ApplyModeToTitle()
    {
        var isEdit = _userId.HasValue;

        Text = isEdit ? "Edit User" : "New User";
        lblFormTitle.Text = isEdit ? "Edit user details" : "Add a new user";
        btnSave.Text = isEdit ? "Save Changes" : "Save";
    }

    public void ShowValidationErrors(IReadOnlyList<string> errors)
    {
        lblErrors.Text = string.Join(Environment.NewLine, errors);
        lblErrors.Visible = errors.Count > 0;
    }

    public void CloseSuccessfully()
    {
        DialogResult = DialogResult.OK;
        Close();
    }
}
