using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DVLD.Manage_Applications_Forms.Manage_Application_Types_Forms
{
    public partial class frmUpdateApplicationType : Form
    {
        private clsApplicationType _ApplicationType;
        private decimal _Fees;

        private Dictionary<string, bool> _Fields = new Dictionary<string, bool>()
        {
            { "ApplicationTypeTitle", false },
            { "ApplicationTypeFees", false },
        };

        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();

            _LoadApplicationTypeInfo(ApplicationTypeID);
        }

        private void _LoadApplicationTypeInfo(int ApplicationTypeID)
        {
            _ApplicationType = clsApplicationType.GetApplicationTypeByApplicationTypeID(ApplicationTypeID);

            lblApplicationTypeID.Text = ApplicationTypeID.ToString();
            txtApplicationTypeTitle.Text = _ApplicationType.GetApplicationTypeTitle();
            txtApplicationTypeFees.Text = _ApplicationType.GetApplicationFees().ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _IsValidApplicationTypeTitle()
        {
            if (string.IsNullOrEmpty(txtApplicationTypeTitle.Text))
            {
                errorProvider1.SetError(txtApplicationTypeTitle, "This Field Should Have A Value!");
                return false;
            }

            else if (clsApplicationType.IsApplicationTypeExistsByApplicationTypeTitle(txtApplicationTypeTitle.Text) && (_ApplicationType.GetApplicationTypeTitle() != txtApplicationTypeTitle.Text))
            {
                errorProvider1.SetError(txtApplicationTypeTitle, "This Application Type Title Is Already Taken.");
                return false;
            }

            errorProvider1.SetError(txtApplicationTypeTitle, "");
            return true;
        }

        private bool _IsValidApplicationTypeFees()
        {
            if (string.IsNullOrEmpty(txtApplicationTypeFees.Text))
            {
                errorProvider1.SetError(txtApplicationTypeFees, "This Field Should Have A Value!");
                return false;
            }

            else if (decimal.TryParse(txtApplicationTypeFees.Text, out decimal Result)) 
                _Fees = Result;
            
            else
            {
                errorProvider1.SetError(txtApplicationTypeFees, "Please Enter A Valid Fees Value.");
                return false;
            }

            errorProvider1.SetError(txtApplicationTypeFees, "");
            return true;
        }

        private bool _IsAllFieldsAreValid()
        {
            _Fields["ApplicationTypeTitle"] = _IsValidApplicationTypeTitle();
            _Fields["ApplicationTypeFees"] = _IsValidApplicationTypeFees();

            foreach (bool Field in _Fields.Values) 
            {
                if(!Field)
                    return false;
            }

            return true;
        }

        private bool _Save()
        {
            _ApplicationType.SetApplicationTypeTitle(txtApplicationTypeTitle.Text);
            _ApplicationType.SetApplicationFees(_Fees);

            return _ApplicationType.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
                          "Confirm Update",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                if (_IsAllFieldsAreValid() && _Save())
                {
                    MessageBox.Show("The Application Type Has Been Updated Successfully",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("The Operation Was Canceled. Please Check Your Information And Try Again.",
                                    "Operation Canceled",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
    }
}
