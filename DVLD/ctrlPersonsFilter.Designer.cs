namespace DVLD
{
    partial class ctrlPersonsFilter
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
            this.ctrlPersonCard1 = new DVLD.ctrlPersonCard();
            this.gbxPersonsFilter = new System.Windows.Forms.GroupBox();
            this.btnFindPerson = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.txtSearchInput = new System.Windows.Forms.TextBox();
            this.cbxPersonsFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxPersonsFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlPersonCard1
            // 
            this.ctrlPersonCard1.Location = new System.Drawing.Point(24, 119);
            this.ctrlPersonCard1.Name = "ctrlPersonCard1";
            this.ctrlPersonCard1.Size = new System.Drawing.Size(905, 290);
            this.ctrlPersonCard1.TabIndex = 0;
            // 
            // gbxPersonsFilter
            // 
            this.gbxPersonsFilter.Controls.Add(this.btnFindPerson);
            this.gbxPersonsFilter.Controls.Add(this.btnAddNew);
            this.gbxPersonsFilter.Controls.Add(this.txtSearchInput);
            this.gbxPersonsFilter.Controls.Add(this.cbxPersonsFilter);
            this.gbxPersonsFilter.Controls.Add(this.label1);
            this.gbxPersonsFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxPersonsFilter.Location = new System.Drawing.Point(24, 3);
            this.gbxPersonsFilter.Name = "gbxPersonsFilter";
            this.gbxPersonsFilter.Size = new System.Drawing.Size(905, 100);
            this.gbxPersonsFilter.TabIndex = 1;
            this.gbxPersonsFilter.TabStop = false;
            this.gbxPersonsFilter.Text = "Filter";
            // 
            // btnFindPerson
            // 
            this.btnFindPerson.BackgroundImage = global::DVLD.Properties.Resources.Find_Person;
            this.btnFindPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFindPerson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFindPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindPerson.Location = new System.Drawing.Point(526, 19);
            this.btnFindPerson.Name = "btnFindPerson";
            this.btnFindPerson.Size = new System.Drawing.Size(89, 70);
            this.btnFindPerson.TabIndex = 12;
            this.btnFindPerson.UseVisualStyleBackColor = true;
            this.btnFindPerson.Click += new System.EventHandler(this.btnFindPerson_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.BackgroundImage = global::DVLD.Properties.Resources.Add_New_Person;
            this.btnAddNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNew.Location = new System.Drawing.Point(626, 19);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(89, 70);
            this.btnAddNew.TabIndex = 11;
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // txtSearchInput
            // 
            this.txtSearchInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchInput.Location = new System.Drawing.Point(314, 41);
            this.txtSearchInput.Name = "txtSearchInput";
            this.txtSearchInput.Size = new System.Drawing.Size(201, 26);
            this.txtSearchInput.TabIndex = 10;
            // 
            // cbxPersonsFilter
            // 
            this.cbxPersonsFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPersonsFilter.FormattingEnabled = true;
            this.cbxPersonsFilter.Items.AddRange(new object[] {
            "National No",
            "Person ID",
            "Phone",
            "Email"});
            this.cbxPersonsFilter.Location = new System.Drawing.Point(102, 40);
            this.cbxPersonsFilter.Name = "cbxPersonsFilter";
            this.cbxPersonsFilter.Size = new System.Drawing.Size(201, 28);
            this.cbxPersonsFilter.TabIndex = 9;
            this.cbxPersonsFilter.SelectedIndexChanged += new System.EventHandler(this.cbxPersonsFilter_SelectedValueChanged);
            this.cbxPersonsFilter.SelectedValueChanged += new System.EventHandler(this.cbxPersonsFilter_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter By :";
            // 
            // ctrlPersonsFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gbxPersonsFilter);
            this.Controls.Add(this.ctrlPersonCard1);
            this.Name = "ctrlPersonsFilter";
            this.Size = new System.Drawing.Size(956, 418);
            this.gbxPersonsFilter.ResumeLayout(false);
            this.gbxPersonsFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPersonCard ctrlPersonCard1;
        private System.Windows.Forms.GroupBox gbxPersonsFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchInput;
        private System.Windows.Forms.ComboBox cbxPersonsFilter;
        private System.Windows.Forms.Button btnFindPerson;
        private System.Windows.Forms.Button btnAddNew;
    }
}
