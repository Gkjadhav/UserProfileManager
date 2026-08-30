namespace UserProfileManager.Views;

partial class UserForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lblFormTitle = new Label();
        lblFullName = new Label();
        txtFullName = new TextBox();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblUserInfo = new Label();
        txtUserInfo = new TextBox();
        lblLinkedIn = new Label();
        txtLinkedIn = new TextBox();
        lblErrors = new Label();
        btnCancel = new Button();
        btnSave = new Button();
        SuspendLayout();
        // 
        // lblFormTitle
        // 
        lblFormTitle.AutoSize = true;
        lblFormTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblFormTitle.ForeColor = Color.FromArgb(31, 36, 48);
        lblFormTitle.Location = new Point(24, 20);
        lblFormTitle.Name = "lblFormTitle";
        lblFormTitle.Size = new Size(143, 25);
        lblFormTitle.TabIndex = 0;
        lblFormTitle.Text = "Add a new user";
        // 
        // lblFullName
        // 
        lblFullName.AutoSize = true;
        lblFullName.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblFullName.ForeColor = Color.FromArgb(91, 98, 112);
        lblFullName.Location = new Point(24, 66);
        lblFullName.Name = "lblFullName";
        lblFullName.Size = new Size(62, 15);
        lblFullName.TabIndex = 1;
        lblFullName.Text = "Full Name";
        // 
        // txtFullName
        // 
        txtFullName.BorderStyle = BorderStyle.FixedSingle;
        txtFullName.Font = new Font("Segoe UI", 9.5F);
        txtFullName.Location = new Point(24, 84);
        txtFullName.Name = "txtFullName";
        txtFullName.Size = new Size(392, 24);
        txtFullName.TabIndex = 2;
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblUsername.ForeColor = Color.FromArgb(91, 98, 112);
        lblUsername.Location = new Point(24, 124);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(64, 15);
        lblUsername.TabIndex = 3;
        lblUsername.Text = "Username";
        // 
        // txtUsername
        // 
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 9.5F);
        txtUsername.Location = new Point(24, 142);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(392, 24);
        txtUsername.TabIndex = 4;
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblEmail.ForeColor = Color.FromArgb(91, 98, 112);
        lblEmail.Location = new Point(24, 182);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(36, 15);
        lblEmail.TabIndex = 5;
        lblEmail.Text = "Email";
        // 
        // txtEmail
        // 
        txtEmail.BorderStyle = BorderStyle.FixedSingle;
        txtEmail.Font = new Font("Segoe UI", 9.5F);
        txtEmail.Location = new Point(24, 200);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(392, 24);
        txtEmail.TabIndex = 6;
        // 
        // lblUserInfo
        // 
        lblUserInfo.AutoSize = true;
        lblUserInfo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblUserInfo.ForeColor = Color.FromArgb(91, 98, 112);
        lblUserInfo.Location = new Point(24, 240);
        lblUserInfo.Name = "lblUserInfo";
        lblUserInfo.Size = new Size(103, 15);
        lblUserInfo.TabIndex = 7;
        lblUserInfo.Text = "User Information";
        // 
        // txtUserInfo
        // 
        txtUserInfo.BorderStyle = BorderStyle.FixedSingle;
        txtUserInfo.Font = new Font("Segoe UI", 9.5F);
        txtUserInfo.Location = new Point(24, 258);
        txtUserInfo.Multiline = true;
        txtUserInfo.Name = "txtUserInfo";
        txtUserInfo.Size = new Size(392, 88);
        txtUserInfo.TabIndex = 8;
        // 
        // lblLinkedIn
        // 
        lblLinkedIn.AutoSize = true;
        lblLinkedIn.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblLinkedIn.ForeColor = Color.FromArgb(91, 98, 112);
        lblLinkedIn.Location = new Point(24, 358);
        lblLinkedIn.Name = "lblLinkedIn";
        lblLinkedIn.Size = new Size(121, 15);
        lblLinkedIn.TabIndex = 9;
        lblLinkedIn.Text = "LinkedIn Profile URL";
        // 
        // txtLinkedIn
        // 
        txtLinkedIn.BorderStyle = BorderStyle.FixedSingle;
        txtLinkedIn.Font = new Font("Segoe UI", 9.5F);
        txtLinkedIn.Location = new Point(24, 376);
        txtLinkedIn.Name = "txtLinkedIn";
        txtLinkedIn.PlaceholderText = "https://www.linkedin.com/in/...";
        txtLinkedIn.Size = new Size(392, 24);
        txtLinkedIn.TabIndex = 10;
        // 
        // lblErrors
        // 
        lblErrors.Font = new Font("Segoe UI", 8.5F);
        lblErrors.ForeColor = Color.FromArgb(200, 40, 40);
        lblErrors.Location = new Point(24, 414);
        lblErrors.Name = "lblErrors";
        lblErrors.Size = new Size(392, 54);
        lblErrors.TabIndex = 11;
        lblErrors.Visible = false;
        // 
        // btnCancel
        // 
        btnCancel.BackColor = Color.White;
        btnCancel.Cursor = Cursors.Hand;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnCancel.ForeColor = Color.FromArgb(91, 98, 112);
        btnCancel.Location = new Point(206, 476);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(90, 36);
        btnCancel.TabIndex = 12;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        // 
        // btnSave
        // 
        btnSave.BackColor = Color.FromArgb(79, 70, 229);
        btnSave.Cursor = Cursors.Hand;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(302, 476);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(114, 36);
        btnSave.TabIndex = 13;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = false;
        // 
        // UserForm
        // 
        AcceptButton = btnSave;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        CancelButton = btnCancel;
        ClientSize = new Size(440, 532);
        Controls.Add(lblFormTitle);
        Controls.Add(lblFullName);
        Controls.Add(txtFullName);
        Controls.Add(lblUsername);
        Controls.Add(txtUsername);
        Controls.Add(lblEmail);
        Controls.Add(txtEmail);
        Controls.Add(lblUserInfo);
        Controls.Add(txtUserInfo);
        Controls.Add(lblLinkedIn);
        Controls.Add(txtLinkedIn);
        Controls.Add(lblErrors);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "UserForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "New User";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblFormTitle;
    private Label lblFullName;
    private TextBox txtFullName;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblUserInfo;
    private TextBox txtUserInfo;
    private Label lblLinkedIn;
    private TextBox txtLinkedIn;
    private Label lblErrors;
    private Button btnCancel;
    private Button btnSave;
}
