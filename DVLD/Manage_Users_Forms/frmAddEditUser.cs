using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Manage_Users_Forms
{
    public partial class frmAddEditUser : Form
    {
        private enum _enMode { eAddNew = 0, eUpdate = 1 };

        private _enMode _Mode;

        private int _CurrentY;
        private int _Permissions;
        private int _PersonID;
        private clsUser _User;

        private Dictionary<string, bool> _Fields = new Dictionary<string, bool>()
        {
            { "UserName", false },
            { "Password", false },
            { "ConfirmPassword", false },
            { "Permissions", false },
        };

        public frmAddEditUser(int UserID = -1)
        {
            InitializeComponent();

            if (UserID != -1)
            {
                _Mode = _enMode.eUpdate;
                lblTitle.Text = "Update User";
                _User = clsUser.GetUserInfoByUserID(UserID);
                _FillUserInfo();
            }
            else
            {
                _PersonID = -1;
                _User = null;
                _CurrentY = 0;
                _Permissions = 0;
            }

            _LoadPermissionsList();
        }

        private bool _IsPersonSelected()
        {
            _PersonID = ctrlPersonsFilter1.GetLoadedPersonID();

            if (clsUser.IsUserExistsByPersonID(_PersonID))
            {
                MessageBox.Show("User Exists With The Provided Person ID [" + _PersonID.ToString() + "].",
                                   "Invalid Input",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);

                return false;
            }

            if (_PersonID == -1)
            {
                MessageBox.Show("Please Select Person.",
                                   "Invalid Input",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {       
            tbcMenu.SelectedIndex = 1;
            lblUserID.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _IsAllFieldsAreValid()
        {
            _Fields["UserName"] = _IsValidUserName();
            _Fields["Password"] = _IsValidPassword();
            _Fields["ConfirmPassword"] = _IsValidConfirmPassword();
            _Fields["Permissions"] = (_Permissions != 0);

            foreach(bool Field in _Fields.Values)
            {
                if(!Field)
                    return false;
            }

            return true;
        }

        private bool _Save()
        {
            if(_Mode == _enMode.eAddNew)
                _User = new clsUser();

            if(_PersonID == -1)
                return false;

            _User.PersonID = _PersonID;

            if (!string.IsNullOrWhiteSpace(txtUserName.Text))
                _User.UserName = txtUserName.Text;
            

            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                _User.Password = txtPassword.Text;
           
            _User.IsActive = ckbxActiveStatus.Checked;
            _User.Permissions = _Permissions;

            return _User.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Message = (_Mode == _enMode.eAddNew) ? "The User Has Been Added Successfully" : "The User Has Been Updated Successfully";
            string Title = (_Mode == _enMode.eAddNew) ? "Confirm Add" : "Confirm Update";

            DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
                            Title,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                if (_IsAllFieldsAreValid() && _Save())
                {
                    MessageBox.Show(Message,
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    lblUserID.Text = _User.GetUserID().ToString();
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

        private bool _IsValidUserName()
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "This Field Should Have A Value!");
                return false;
            }
            else if (clsUser.IsUserExistsByUserName(txtUserName.Text))
            {
                if (_Mode == _enMode.eAddNew || (_Mode == _enMode.eUpdate && _User.UserName != txtUserName.Text))
                {
                    errorProvider1.SetError(txtUserName, "This UserName Is Already Taken.");
                    return false;
                }
            }
           
                errorProvider1.SetError(txtUserName, "");
                return true;
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

        private void tbcMenu_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_Mode == _enMode.eAddNew)
            {
                if (tbcMenu.SelectedIndex == 1 && !_IsPersonSelected())
                    tbcMenu.SelectedIndex = 0;
            }
        }

        private void _LoadPermissionsList()
        {
            foreach (string Permissions in clsUserPermission.PermissionDescriptions.Values)
            {
                cbxPermissionsList.Items.Add(Permissions);
            }
        }

        private void ReorderSelectedPermissionsList()
        {
            _CurrentY = 0;

            foreach (Control Control in plPermissionsList.Controls)
            {
                if (Control is ctrlListContent)
                {
                    Control.Location = new Point(10, _CurrentY); 
                    _CurrentY += Control.Height + 10;
                }
            }

            _CalculatePermissions();
        }

        private bool _IsPermissionSelected(string Permission)
        {
            foreach (ctrlListContent Control in plPermissionsList.Controls)
            {
                if (Control.Content.Text == Permission)
                    return true;
            }

            return false;
        }

        private void _CalculatePermissions()
        {
            _Permissions = 0;

            foreach (ctrlListContent Control in plPermissionsList.Controls)
            {
                _Permissions += clsUserPermission.GetPermissionValueByPermissionDescription(Control.Content.Text);
            }
        }

        private void _AddPermission(string Permissions)
        {
            ctrlListContent ListContent = new ctrlListContent();
            ListContent.Content.Text = Permissions;
            ListContent.Location = new Point(10, _CurrentY);
            ListContent.CloseHandlerRequest += ReorderSelectedPermissionsList;
            _CurrentY += ListContent.Height + 10;
            
            plPermissionsList.Controls.Add(ListContent);

            _CalculatePermissions();
        }

        private void cbxPermissionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_IsPermissionSelected(cbxPermissionsList.SelectedItem.ToString()))
                return;

           _AddPermission(cbxPermissionsList.SelectedItem.ToString());
        }

        private void btnFillPasswords_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ctrlPasswordGenerator.Password))
            {
                MessageBox.Show("The Operation Was Canceled. Please Generate Password.",
                                    "Operation Canceled",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                return;
            }

            txtPassword.Text = ctrlPasswordGenerator.Password;
            txtConfirmPassword.Text = ctrlPasswordGenerator.Password;
        }

        private void _FillUserInfo()
        {
            _PersonID = _User.PersonID;
            ctrlPersonsFilter1.LoadPersonInfo(_PersonID);
            lblUserID.Text = _User.GetUserID().ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            ckbxActiveStatus.Checked = _User.IsActive;
            ckbxAllPermissions.Checked = (_User.Permissions == -1);

            if(!ckbxAllPermissions.Checked)
                _LoadUserCurrentPermissionsList();
        }

        private void _LoadUserCurrentPermissionsList()
        {
            foreach (string Permissions in clsUserPermission.PermissionDescriptions.Values)
            {
                if((_User.Permissions & clsUserPermission.GetPermissionValueByPermissionDescription(Permissions)) == clsUserPermission.GetPermissionValueByPermissionDescription(Permissions))
                    _AddPermission(Permissions);
            }
        }

        private void ckbxAllPermissions_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbxAllPermissions.Checked)
            {
                cbxPermissionsList.Enabled = false;
                plPermissionsList.Visible = false;
                _Permissions = -1;
            }

            else
            {
                cbxPermissionsList.Enabled = true;
                plPermissionsList.Visible = true;
                _CalculatePermissions();
            }
        }

    }
}
