namespace UserProfileManager.Views;

partial class MainForm
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
        components = new System.ComponentModel.Container();
        pnlHeader = new Panel();
        btnNewUser = new Button();
        lblTitle = new Label();
        pnlSearch = new Panel();
        txtSearch = new TextBox();
        pnlPaging = new Panel();
        lblPagingInfo = new Label();
        pnlPagerGroup = new Panel();
        btnPrevious = new Button();
        lblPageStatus = new Label();
        btnNext = new Button();
        dgvUsers = new DataGridView();
        colId = new DataGridViewTextBoxColumn();
        colFullName = new DataGridViewTextBoxColumn();
        colUsername = new DataGridViewTextBoxColumn();
        colEmail = new DataGridViewTextBoxColumn();
        colUserInfo = new DataGridViewTextBoxColumn();
        colLinkedIn = new DataGridViewTextBoxColumn();
        colCreatedAt = new DataGridViewTextBoxColumn();
        colUpdatedAt = new DataGridViewTextBoxColumn();
        colEdit = new DataGridViewButtonColumn();
        colDelete = new DataGridViewButtonColumn();
        bindingSourceUsers = new BindingSource(components);
        pnlHeader.SuspendLayout();
        pnlSearch.SuspendLayout();
        pnlPaging.SuspendLayout();
        pnlPagerGroup.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
        ((System.ComponentModel.ISupportInitialize)bindingSourceUsers).BeginInit();
        SuspendLayout();
        //
        // pnlHeader
        //
        pnlHeader.BackColor = Color.White;
        pnlHeader.Controls.Add(btnNewUser);
        pnlHeader.Controls.Add(lblTitle);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(1180, 64);
        pnlHeader.TabIndex = 0;
        //
        // btnNewUser
        //
        btnNewUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnNewUser.BackColor = Color.FromArgb(79, 70, 229);
        btnNewUser.Cursor = Cursors.Hand;
        btnNewUser.FlatAppearance.BorderSize = 0;
        btnNewUser.FlatStyle = FlatStyle.Flat;
        btnNewUser.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnNewUser.ForeColor = Color.White;
        btnNewUser.Location = new Point(1044, 20);
        btnNewUser.Name = "btnNewUser";
        btnNewUser.Size = new Size(112, 32);
        btnNewUser.TabIndex = 1;
        btnNewUser.Text = "+ New User";
        btnNewUser.UseVisualStyleBackColor = false;
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(31, 36, 48);
        lblTitle.Location = new Point(24, 18);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(63, 28);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Users";
        //
        // pnlSearch
        //
        pnlSearch.BackColor = Color.White;
        pnlSearch.Controls.Add(txtSearch);
        pnlSearch.Dock = DockStyle.Top;
        pnlSearch.Location = new Point(0, 64);
        pnlSearch.Name = "pnlSearch";
        pnlSearch.Size = new Size(1180, 56);
        pnlSearch.TabIndex = 1;
        //
        // txtSearch
        //
        txtSearch.BorderStyle = BorderStyle.FixedSingle;
        txtSearch.Font = new Font("Segoe UI", 9.5F);
        txtSearch.Location = new Point(24, 16);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "  Search by name, username, or email";
        txtSearch.Size = new Size(400, 28);
        txtSearch.TabIndex = 1;
        //
        // pnlPaging
        //
        pnlPaging.BackColor = Color.White;
        pnlPaging.Controls.Add(pnlPagerGroup);
        pnlPaging.Controls.Add(lblPagingInfo);
        pnlPaging.Dock = DockStyle.Bottom;
        pnlPaging.Location = new Point(0, 611);
        pnlPaging.Name = "pnlPaging";
        pnlPaging.Size = new Size(1180, 50);
        pnlPaging.TabIndex = 2;
        //
        // lblPagingInfo
        //
        lblPagingInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        lblPagingInfo.AutoSize = true;
        lblPagingInfo.Font = new Font("Segoe UI", 9F);
        lblPagingInfo.ForeColor = Color.FromArgb(138, 147, 163);
        lblPagingInfo.Location = new Point(24, 17);
        lblPagingInfo.Name = "lblPagingInfo";
        lblPagingInfo.Size = new Size(62, 15);
        lblPagingInfo.TabIndex = 0;
        lblPagingInfo.Text = "Showing...";
        //
        // pnlPagerGroup
        //
        pnlPagerGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        pnlPagerGroup.Controls.Add(btnPrevious);
        pnlPagerGroup.Controls.Add(lblPageStatus);
        pnlPagerGroup.Controls.Add(btnNext);
        pnlPagerGroup.Location = new Point(890, 9);
        pnlPagerGroup.Name = "pnlPagerGroup";
        pnlPagerGroup.Size = new Size(266, 32);
        pnlPagerGroup.TabIndex = 1;
        //
        // btnPrevious
        //
        btnPrevious.BackColor = Color.White;
        btnPrevious.Cursor = Cursors.Hand;
        btnPrevious.FlatAppearance.BorderColor = Color.FromArgb(216, 220, 227);
        btnPrevious.FlatStyle = FlatStyle.Flat;
        btnPrevious.Font = new Font("Segoe UI", 9F);
        btnPrevious.ForeColor = Color.FromArgb(91, 98, 112);
        btnPrevious.Location = new Point(0, 0);
        btnPrevious.Name = "btnPrevious";
        btnPrevious.Size = new Size(90, 30);
        btnPrevious.TabIndex = 0;
        btnPrevious.Text = "< Previous";
        btnPrevious.UseVisualStyleBackColor = false;
        //
        // lblPageStatus
        //
        lblPageStatus.AutoSize = true;
        lblPageStatus.Font = new Font("Segoe UI", 9F);
        lblPageStatus.ForeColor = Color.FromArgb(91, 98, 112);
        lblPageStatus.Location = new Point(104, 8);
        lblPageStatus.Name = "lblPageStatus";
        lblPageStatus.Size = new Size(65, 15);
        lblPageStatus.TabIndex = 1;
        lblPageStatus.Text = "Page 1 of 1";
        //
        // btnNext
        //
        btnNext.BackColor = Color.White;
        btnNext.Cursor = Cursors.Hand;
        btnNext.FlatAppearance.BorderColor = Color.FromArgb(216, 220, 227);
        btnNext.FlatStyle = FlatStyle.Flat;
        btnNext.Font = new Font("Segoe UI", 9F);
        btnNext.ForeColor = Color.FromArgb(91, 98, 112);
        btnNext.Location = new Point(186, 0);
        btnNext.Name = "btnNext";
        btnNext.Size = new Size(80, 30);
        btnNext.TabIndex = 2;
        btnNext.Text = "Next >";
        btnNext.UseVisualStyleBackColor = false;
        //
        // dgvUsers
        //
        dgvUsers.AllowUserToAddRows = false;
        dgvUsers.AllowUserToDeleteRows = false;
        dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(251, 252, 253);
        dgvUsers.AutoGenerateColumns = false;
        dgvUsers.BackgroundColor = Color.White;
        dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgvUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
        dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(138, 147, 163);
        dgvUsers.ColumnHeadersHeight = 36;
        dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvUsers.Columns.AddRange(new DataGridViewColumn[] { colId, colFullName, colUsername, colEmail, colUserInfo, colLinkedIn, colCreatedAt, colUpdatedAt, colEdit, colDelete });
        dgvUsers.DataSource = bindingSourceUsers;
        dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
        dgvUsers.DefaultCellStyle.ForeColor = Color.FromArgb(59, 66, 82);
        dgvUsers.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
        dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 240, 255);
        dgvUsers.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 36, 48);
        dgvUsers.Dock = DockStyle.Fill;
        dgvUsers.EnableHeadersVisualStyles = false;
        dgvUsers.GridColor = Color.FromArgb(242, 243, 245);
        dgvUsers.Location = new Point(0, 120);
        dgvUsers.MultiSelect = false;
        dgvUsers.Name = "dgvUsers";
        dgvUsers.ReadOnly = true;
        dgvUsers.RowHeadersVisible = false;
        dgvUsers.RowTemplate.Height = 42;
        dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvUsers.Size = new Size(1180, 491);
        dgvUsers.TabIndex = 3;
        //
        // colId
        //
        colId.DataPropertyName = "Id";
        colId.DefaultCellStyle.ForeColor = Color.FromArgb(183, 190, 201);
        colId.HeaderText = "ID";
        colId.Name = "colId";
        colId.ReadOnly = true;
        colId.Width = 44;
        //
        // colFullName
        //
        colFullName.DataPropertyName = "FullName";
        colFullName.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        colFullName.DefaultCellStyle.ForeColor = Color.FromArgb(31, 36, 48);
        colFullName.HeaderText = "FULL NAME";
        colFullName.Name = "colFullName";
        colFullName.ReadOnly = true;
        colFullName.MinimumWidth = 300;
        //
        // colUsername
        //
        colUsername.DataPropertyName = "Username";
        colUsername.DefaultCellStyle.ForeColor = Color.FromArgb(138, 147, 163);
        colUsername.HeaderText = "USERNAME";
        colUsername.Name = "colUsername";
        colUsername.ReadOnly = true;
        colUsername.MinimumWidth = 200;
        //
        // colEmail
        //
        colEmail.DataPropertyName = "Email";
        colEmail.DefaultCellStyle.ForeColor = Color.FromArgb(91, 98, 112);
        colEmail.HeaderText = "EMAIL";
        colEmail.MinimumWidth = 250;
        colEmail.Name = "colEmail";
        colEmail.ReadOnly = true;
        //
        // colUserInfo
        //
        colUserInfo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colUserInfo.DataPropertyName = "UserInfo";
        colUserInfo.DefaultCellStyle.ForeColor = Color.FromArgb(138, 147, 163);
        colUserInfo.HeaderText = "USER INFO";
        colUserInfo.MinimumWidth = 200;
        colUserInfo.Name = "colUserInfo";
        colUserInfo.ReadOnly = true;
        //
        // colLinkedIn
        //
        colLinkedIn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colLinkedIn.DataPropertyName = "LinkedInProfile";
        colLinkedIn.DefaultCellStyle.ForeColor = Color.FromArgb(79, 70, 229);
        colLinkedIn.HeaderText = "LINKEDIN";
        colLinkedIn.Name = "colLinkedIn";
        colLinkedIn.ReadOnly = true;
        colLinkedIn.MinimumWidth = 200;
        //
        // colCreatedAt
        //
        colCreatedAt.DataPropertyName = "CreatedAt";
        colCreatedAt.DefaultCellStyle.Font = new Font("Segoe UI", 8F);
        colCreatedAt.DefaultCellStyle.ForeColor = Color.FromArgb(174, 180, 192);
        colCreatedAt.DefaultCellStyle.Format = "MMM dd, yyyy";
        colCreatedAt.HeaderText = "CREATED";
        colCreatedAt.Name = "colCreatedAt";
        colCreatedAt.ReadOnly = true;
        colCreatedAt.Width = 150;
        //
        // colUpdatedAt
        //
        colUpdatedAt.DataPropertyName = "UpdatedAt";
        colUpdatedAt.DefaultCellStyle.Font = new Font("Segoe UI", 8F);
        colUpdatedAt.DefaultCellStyle.ForeColor = Color.FromArgb(174, 180, 192);
        colUpdatedAt.DefaultCellStyle.Format = "MMM dd, yyyy";
        colUpdatedAt.DefaultCellStyle.NullValue = "—";
        colUpdatedAt.HeaderText = "UPDATED";
        colUpdatedAt.Name = "colUpdatedAt";
        colUpdatedAt.ReadOnly = true;
        colUpdatedAt.Width = 150;
        //
        // colEdit
        //
        colEdit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        colEdit.DefaultCellStyle.BackColor = Color.FromArgb(238, 240, 255);
        colEdit.DefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        colEdit.DefaultCellStyle.ForeColor = Color.FromArgb(79, 70, 229);
        colEdit.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 224, 255);
        colEdit.DefaultCellStyle.SelectionForeColor = Color.FromArgb(79, 70, 229);
        colEdit.FlatStyle = FlatStyle.Flat;
        colEdit.HeaderText = "ACTIONS";
        colEdit.Name = "colEdit";
        colEdit.ReadOnly = true;
        colEdit.Text = "Edit";
        colEdit.UseColumnTextForButtonValue = true;
        colEdit.MinimumWidth = 150;
        //
        // colDelete
        //
        colDelete.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        colDelete.DefaultCellStyle.BackColor = Color.FromArgb(253, 238, 238);
        colDelete.DefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        colDelete.DefaultCellStyle.ForeColor = Color.FromArgb(200, 40, 40);
        colDelete.DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 220, 220);
        colDelete.DefaultCellStyle.SelectionForeColor = Color.FromArgb(200, 40, 40);
        colDelete.FlatStyle = FlatStyle.Flat;
        colDelete.HeaderText = "";
        colDelete.Name = "colDelete";
        colDelete.ReadOnly = true;
        colDelete.Text = "Delete";
        colDelete.UseColumnTextForButtonValue = true;
        colDelete.MinimumWidth = 150;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(1180, 500);
        Controls.Add(dgvUsers);
        Controls.Add(pnlPaging);
        Controls.Add(pnlSearch);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        Margin = new Padding(2);
        MinimumSize = new Size(1300, 500);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "UserProfileManager";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlSearch.ResumeLayout(false);
        pnlSearch.PerformLayout();
        pnlPaging.ResumeLayout(false);
        pnlPaging.PerformLayout();
        pnlPagerGroup.ResumeLayout(false);
        pnlPagerGroup.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
        ((System.ComponentModel.ISupportInitialize)bindingSourceUsers).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlHeader;
    private Panel pnlSearch;
    private Panel pnlPaging;
    private DataGridView dgvUsers;
    private Label lblTitle;
    private Button btnNewUser;
    private TextBox txtSearch;
    private Label lblPagingInfo;
    private Panel pnlPagerGroup;
    private Button btnNext;
    private Label lblPageStatus;
    private Button btnPrevious;
    private BindingSource bindingSourceUsers;
    private DataGridViewTextBoxColumn colId;
    private DataGridViewTextBoxColumn colFullName;
    private DataGridViewTextBoxColumn colUsername;
    private DataGridViewTextBoxColumn colEmail;
    private DataGridViewTextBoxColumn colUserInfo;
    private DataGridViewTextBoxColumn colLinkedIn;
    private DataGridViewTextBoxColumn colCreatedAt;
    private DataGridViewTextBoxColumn colUpdatedAt;
    private DataGridViewButtonColumn colEdit;
    private DataGridViewButtonColumn colDelete;
}
