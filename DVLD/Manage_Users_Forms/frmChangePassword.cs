using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DVLD.Manage_Users_Forms
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;

        private Dictionary<string, bool> _Fields = new Dictionary<string, bool>()
        {
            { "Password", false },
            { "ConfirmPassword", false },
            { "CurrentPassword", false },
        };

        public frmChangePassword(int UserID, string Password)
        {
            InitializeComponent();

            _UserID = UserID;

            ctrlUserCard1.LoadUserInfoByUserID(UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _Save()
        {
            return clsUser.UpdateUserPassword(_UserID, txtPassword.Text);
        }

        private bool _IsAllFieldsAreValid()
        {
            _Fields["Password"] = _IsValidPassword();
            _Fields["ConfirmPassword"] = _IsValidConfirmPassword();
            _Fields["CurrentPassword"] = _IsValidCurrentPassword();

            foreach (bool Field in _Fields.Values)
            {
                if (!Field)
                    return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
                            "Update Password",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                if (_IsAllFieldsAreValid() && _Save())
                {
                    MessageBox.Show("The Password Has Been Updated Successfully",
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

        private void btnFillPasswords_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ctrlPasswordGenerator.Password))
            {
                MessageBox.Show("The Operation Was Canceled. Please Generate Password.",
                                    "Operation Canceled",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                return;
            }

            txtPassword.Text = ctrlPasswordGenerator.Password;
            txtConfirmPassword.Text = ctrlPasswordGenerator.Password;
            _Fields["Password"] = true;
            _Fields["ConfirmPassword"] = true;
        }

        private bool _IsValidPassword()
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "This Field Should Have A Value!");
                return false;
            }
            else if (txtPassword.Text.Length < 12)
            {
                errorProvider1.SetError(txtPassword, "Password Must Be At Least 12 Characters Long.");
                return false;
            }
            else if (txtPassword.Text.Length > 20)
            {
                errorProvider1.SetError(txtPassword, "Password Must Be At Less Than 20 Characters Long.");
                return false;
            }

            errorProvider1.SetError(txtPassword, "");
            return true;
        }

        private bool _IsValidConfirmPassword()
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                errorProvider1.SetError(txtConfirmPassword, "This Field Should Have A Value!");
                return false;
            }
            else if (txtConfirmPassword.Text != txtPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPassword, "Passwords Do Not Match.");
                return false;
            }


            errorProvider1.SetError(txtConfirmPassword, "");
            return true;
        }

        private bool _IsValidCurrentPassword()
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {     
                errorProvider1.SetError(txtCurrentPassword, "This Field Should Have A Value!");
                return false;
            }
            else if (!clsUser.IsPasswordMatchesCurrentPassword(_UserID, txtCurrentPassword.Text))
            {
                errorProvider1.SetError(txtCurrentPassword, "The Entered Password Does Not Match Your Current Password.");
                return false;
            }
          
                errorProvider1.SetError(txtCurrentPassword, "");
                return true;
        }
    }
}
