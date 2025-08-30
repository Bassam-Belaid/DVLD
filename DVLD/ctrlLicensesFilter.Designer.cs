namespace DVLD
{
    partial class ctrlLicensesFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxLicenseFilter = new System.Windows.Forms.GroupBox();
            this.btnFindPerson = new System.Windows.Forms.Button();
            this.txtSearchInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlLicenseCard2 = new DVLD.ctrlLicenseCard();
            this.gbxLicenseFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxLicenseFilter
            // 
            this.gbxLicenseFilter.Controls.Add(this.btnFindPerson);
            this.gbxLicenseFilter.Controls.Add(this.txtSearchInput);
            this.gbxLicenseFilter.Controls.Add(this.label1);
            this.gbxLicenseFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxLicenseFilter.Location = new System.Drawing.Point(10, 2);
            this.gbxLicenseFilter.Margin = new System.Windows.Forms.Padding(2);
            this.gbxLicenseFilter.Name = "gbxLicenseFilter";
            this.gbxLicenseFilter.Padding = new System.Windows.Forms.Padding(2);
            this.gbxLicenseFilter.Size = new System.Drawing.Size(505, 65);
            this.gbxLicenseFilter.TabIndex = 79;
            this.gbxLicenseFilter.TabStop = false;
            this.gbxLicenseFilter.Text = "Filter";
            // 
            // btnFindPerson
            // 
            this.btnFindPerson.BackgroundImage = global::DVLD.Properties.Resources.Driving_License;
            this.btnFindPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFindPerson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFindPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindPerson.Location = new System.Drawing.Point(392, 22);
            this.btnFindPerson.Margin = new System.Windows.Forms.Padding(2);
            this.btnFindPerson.Name = "btnFindPerson";
            this.btnFindPerson.Size = new System.Drawing.Size(68, 36);
            this.btnFindPerson.TabIndex = 14;
            this.btnFindPerson.UseVisualStyleBackColor = true;
            this.btnFindPerson.Click += new System.EventHandler(this.btnFindLicense_Click);
            // 
            // txtSearchInput
            // 
            this.txtSearchInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchInput.Location = new System.Drawing.Point(137, 27);
            this.txtSearchInput.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearchInput.Name = "txtSearchInput";
            this.txtSearchInput.Size = new System.Drawing.Size(227, 26);
            this.txtSearchInput.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "License ID :";
            // 
            // ctrlLicenseCard2
            // 
            this.ctrlLicenseCard2.Location = new System.Drawing.Point(10, 72);
            this.ctrlLicenseCard2.Name = "ctrlLicenseCard2";
            this.ctrlLicenseCard2.Size = new System.Drawing.Size(763, 401);
            this.ctrlLicenseCard2.TabIndex = 80;
            // 
            // ctrlLicensesFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlLicenseCard2);
            this.Controls.Add(this.gbxLicenseFilter);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ctrlLicensesFilter";
            this.Size = new System.Drawing.Size(784, 483);
            this.gbxLicenseFilter.ResumeLayout(false);
            this.gbxLicenseFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxLicenseFilter;
        private System.Windows.Forms.Button btnFindPerson;
        private System.Windows.Forms.TextBox txtSearchInput;
        private System.Windows.Forms.Label label1;
        private ctrlLicenseCard ctrlLicenseCard2;
    }
}
