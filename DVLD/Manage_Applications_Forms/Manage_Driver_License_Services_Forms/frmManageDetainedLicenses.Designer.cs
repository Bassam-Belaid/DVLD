namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms
{
    partial class frmManageDetainedLicenses
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.txtSearchInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDetainedLicensesFilter = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmsManageDetainedLicensesList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showApplicationDetialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseDetainedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReleaseDetain = new System.Windows.Forms.Button();
            this.btnAddNewDetain = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbxReleaseStatus = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.cmsManageDetainedLicensesList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecords.Location = new System.Drawing.Point(109, 567);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(0, 20);
            this.lblNumberOfRecords.TabIndex = 39;
            // 
            // txtSearchInput
            // 
            this.txtSearchInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchInput.Location = new System.Drawing.Point(332, 282);
            this.txtSearchInput.Name = "txtSearchInput";
            this.txtSearchInput.Size = new System.Drawing.Size(201, 26);
            this.txtSearchInput.TabIndex = 38;
            this.txtSearchInput.Visible = false;
            this.txtSearchInput.TextChanged += new System.EventHandler(this.txtSearchInput_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 37;
            this.label3.Text = "Filter By";
            // 
            // cbxDetainedLicensesFilter
            // 
            this.cbxDetainedLicensesFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDetainedLicensesFilter.FormattingEnabled = true;
            this.cbxDetainedLicensesFilter.Items.AddRange(new object[] {
            "None",
            "Detain ID",
            "Is Released",
            "National No",
            "Full Name",
            "Release Application ID"});
            this.cbxDetainedLicensesFilter.Location = new System.Drawing.Point(99, 281);
            this.cbxDetainedLicensesFilter.Name = "cbxDetainedLicensesFilter";
            this.cbxDetainedLicensesFilter.Size = new System.Drawing.Size(201, 28);
            this.cbxDetainedLicensesFilter.TabIndex = 36;
            this.cbxDetainedLicensesFilter.SelectedValueChanged += new System.EventHandler(this.cbxDetainedLicensesFilter_SelectedValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.cmsManageDetainedLicensesList;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView1.Location = new System.Drawing.Point(12, 321);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1294, 231);
            this.dataGridView1.TabIndex = 35;
            // 
            // cmsManageDetainedLicensesList
            // 
            this.cmsManageDetainedLicensesList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showApplicationDetialsToolStripMenuItem,
            this.showLicenseToolStripMenuItem,
            this.showPersonLicenseHistoryToolStripMenuItem,
            this.releaseDetainedLicenseToolStripMenuItem});
            this.cmsManageDetainedLicensesList.Name = "cmsManageLocalApplications";
            this.cmsManageDetainedLicensesList.Size = new System.Drawing.Size(242, 156);
            this.cmsManageDetainedLicensesList.Opened += new System.EventHandler(this.cmsManageDetainedLicensesList_Opened);
            // 
            // showApplicationDetialsToolStripMenuItem
            // 
            this.showApplicationDetialsToolStripMenuItem.Image = global::DVLD.Properties.Resources.Show_Application_Details;
            this.showApplicationDetialsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showApplicationDetialsToolStripMenuItem.Name = "showApplicationDetialsToolStripMenuItem";
            this.showApplicationDetialsToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.showApplicationDetialsToolStripMenuItem.Text = "Show Person Details";
            this.showApplicationDetialsToolStripMenuItem.Click += new System.EventHandler(this.showApplicationDetialsToolStripMenuItem_Click);
            // 
            // showLicenseToolStripMenuItem
            // 
            this.showLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.Driving_License;
            this.showLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showLicenseToolStripMenuItem.Name = "showLicenseToolStripMenuItem";
            this.showLicenseToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.showLicenseToolStripMenuItem.Text = "Show License Details";
            this.showLicenseToolStripMenuItem.Click += new System.EventHandler(this.showLicenseToolStripMenuItem_Click);
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Image = global::DVLD.Properties.Resources.License_History_1;
            this.showPersonLicenseHistoryToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // releaseDetainedLicenseToolStripMenuItem
            // 
            this.releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            this.releaseDetainedLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.ReleaseDetainLicense_2;
            this.releaseDetainedLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.releaseDetainedLicenseToolStripMenuItem.Name = "releaseDetainedLicenseToolStripMenuItem";
            this.releaseDetainedLicenseToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.releaseDetainedLicenseToolStripMenuItem.Text = "Release Detained License";
            this.releaseDetainedLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseDetainedLicenseToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 567);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 34;
            this.label2.Text = "# Records";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(494, 234);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(331, 33);
            this.label1.TabIndex = 31;
            this.label1.Text = "List Detained Licenses";
            // 
            // btnReleaseDetain
            // 
            this.btnReleaseDetain.BackgroundImage = global::DVLD.Properties.Resources.ReleaseDetainLicense;
            this.btnReleaseDetain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReleaseDetain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReleaseDetain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReleaseDetain.Location = new System.Drawing.Point(1122, 239);
            this.btnReleaseDetain.Name = "btnReleaseDetain";
            this.btnReleaseDetain.Size = new System.Drawing.Size(89, 70);
            this.btnReleaseDetain.TabIndex = 41;
            this.btnReleaseDetain.UseVisualStyleBackColor = true;
            this.btnReleaseDetain.Click += new System.EventHandler(this.btnReleaseDetain_Click);
            // 
            // btnAddNewDetain
            // 
            this.btnAddNewDetain.BackgroundImage = global::DVLD.Properties.Resources.Detain_License;
            this.btnAddNewDetain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddNewDetain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewDetain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewDetain.Location = new System.Drawing.Point(1217, 239);
            this.btnAddNewDetain.Name = "btnAddNewDetain";
            this.btnAddNewDetain.Size = new System.Drawing.Size(89, 70);
            this.btnAddNewDetain.TabIndex = 40;
            this.btnAddNewDetain.UseVisualStyleBackColor = true;
            this.btnAddNewDetain.Click += new System.EventHandler(this.btnAddNewDetain_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1159, 558);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(147, 38);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Detain_License_2;
            this.pictureBox1.Location = new System.Drawing.Point(556, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(206, 206);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // cbxReleaseStatus
            // 
            this.cbxReleaseStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxReleaseStatus.FormattingEnabled = true;
            this.cbxReleaseStatus.Items.AddRange(new object[] {
            "All",
            "Released",
            "Not Released"});
            this.cbxReleaseStatus.Location = new System.Drawing.Point(332, 281);
            this.cbxReleaseStatus.Name = "cbxReleaseStatus";
            this.cbxReleaseStatus.Size = new System.Drawing.Size(201, 28);
            this.cbxReleaseStatus.TabIndex = 42;
            this.cbxReleaseStatus.Visible = false;
            this.cbxReleaseStatus.SelectedValueChanged += new System.EventHandler(this.cbxReleaseStatus_SelectedValueChanged);
            // 
            // frmManageDetainedLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 609);
            this.Controls.Add(this.cbxReleaseStatus);
            this.Controls.Add(this.btnReleaseDetain);
            this.Controls.Add(this.btnAddNewDetain);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.txtSearchInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxDetainedLicensesFilter);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageDetainedLicenses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Detained Licenses";
            this.Load += new System.EventHandler(this.frmManageDetainedLicenses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.cmsManageDetainedLicensesList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddNewDetain;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.TextBox txtSearchInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDetainedLicensesFilter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReleaseDetain;
        private System.Windows.Forms.ContextMenuStrip cmsManageDetainedLicensesList;
        private System.Windows.Forms.ToolStripMenuItem showApplicationDetialsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedLicenseToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbxReleaseStatus;
    }
}