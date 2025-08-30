namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_New_Driver_License_Forms
{
    partial class frmLocalDrivingLicenseApplicationDetials
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
            this.ctrlLocalDrivingLicenseApplicationCard1 = new DVLD.ctrlLocalDrivingLicenseApplicationCard();
            this.SuspendLayout();
            // 
            // ctrlLocalDrivingLicenseApplicationCard1
            // 
            this.ctrlLocalDrivingLicenseApplicationCard1.Location = new System.Drawing.Point(15, 50);
            this.ctrlLocalDrivingLicenseApplicationCard1.Name = "ctrlLocalDrivingLicenseApplicationCard1";
            this.ctrlLocalDrivingLicenseApplicationCard1.Size = new System.Drawing.Size(770, 350);
            this.ctrlLocalDrivingLicenseApplicationCard1.TabIndex = 1;
            // 
            // frmLocalDrivingLicenseApplicationDetials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ctrlLocalDrivingLicenseApplicationCard1);
            this.Name = "frmLocalDrivingLicenseApplicationDetials";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Driving License Application Detials";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlLocalDrivingLicenseApplicationCard ctrlLocalDrivingLicenseApplicationCard1;
    }
}