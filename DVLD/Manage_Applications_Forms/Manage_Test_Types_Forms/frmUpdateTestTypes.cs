using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD.Manage_Applications_Forms.Manage_Test_Types_Forms
{
    public partial class frmUpdateTestTypes : Form
    {
        private clsTestType _TestType;
        private decimal _Fees;

        private Dictionary<string, bool> _Fields = new Dictionary<string, bool>()
        {
            { "TestTypeTitle", false },
            { "TestTypeDescription", false },
            { "TestTypeFees", false },
        };
        public frmUpdateTestTypes(int TestTypeID)
        {
            InitializeComponent();

            _LoadTestTypeInfo(TestTypeID);
        }

        private void _LoadTestTypeInfo(int TestTypeID)
        {
            _TestType = clsTestType.GetTestTypeByTestTypeID(TestTypeID);

            lblTestTypeID.Text = TestTypeID.ToString();
            txtTestTypeTitle.Text = _TestType.GetTestTypeTitle();
            txtTestTypeDescription.Text = _TestType.GetTestTypeDescription();
            txtTestTypeFees.Text = _TestType.GetTestTypeFees().ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _IsValidTestTypeTitle()
        {
            if (string.IsNullOrEmpty(txtTestTypeTitle.Text))
            {
                errorProvider1.SetError(txtTestTypeTitle, "This Field Should Have A Value!");
                return false;
            }

            else if (clsTestType.IsTestTypeExistsByTestTypeTitle(txtTestTypeTitle.Text) && (_TestType.GetTestTypeTitle() != txtTestTypeTitle.Text))
            {
                errorProvider1.SetError(txtTestTypeTitle, "This Test Type Title Is Already Taken.");
                return false;
            }

            errorProvider1.SetError(txtTestTypeTitle, "");
            return true;
        }

        private bool _IsValidTestTypeDescription()
        {
            if (string.IsNullOrEmpty(txtTestTypeDescription.Text))
            {
                errorProvider1.SetError(txtTestTypeDescription, "This Field Should Have A Value!");
                return false;
            }

            errorProvider1.SetError(txtTestTypeDescription, "");
            return true;
        }

        private bool _IsValidTestTypeFees()
        {
            if (string.IsNullOrEmpty(txtTestTypeFees.Text))
            {
                errorProvider1.SetError(txtTestTypeFees, "This Field Should Have A Value!");
                return false;
            }

            else if (decimal.TryParse(txtTestTypeFees.Text, out decimal Result))
                _Fees = Result;

            else
            {
                errorProvider1.SetError(txtTestTypeFees, "Please Enter A Valid Fees Value.");
                return false;
            }

            errorProvider1.SetError(txtTestTypeFees, "");
            return true;
        }

        private bool _IsAllFieldsAreValid()
        {
            _Fields["TestTypeTitle"] = _IsValidTestTypeTitle();
            _Fields["TestTypeDescription"] = _IsValidTestTypeDescription();
            _Fields["TestTypeFees"] = _IsValidTestTypeFees();

            foreach (bool Field in _Fields.Values) 
            {
                if(!Field)
                    return false;
            }

            return true;
        }

        private bool _Save()
        {
            _TestType.SetTestTypeTitle(txtTestTypeTitle.Text);
            _TestType.SetTestTypeDescription(txtTestTypeDescription.Text);
            _TestType.SetTestTypeFees(_Fees);

            return _TestType.Save();
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
                    MessageBox.Show("The Test Type Has Been Updated Successfully",
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
