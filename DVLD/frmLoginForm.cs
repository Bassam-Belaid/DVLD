using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Util;

namespace DVLD
{
    public partial class frmLoginForm : Form
    {
        public frmLoginForm()
        {
            InitializeComponent();
        }
        
        private void ShowEmptyFieldsMessage()
        {
            MessageBox.Show("Please Enter Both Username And Password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _ShowInvalidLoginMessage()
        {
            MessageBox.Show("Invalid Username Or Password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _ShowInactiveUserMessage()
        {
            MessageBox.Show("Your Account Is Not Active. Please Contact Admin.", "Account Inactive", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _ShowSuccessLoginMessage()
        {
            MessageBox.Show("Login Successful! Welcome.", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool _IsFieldsEmpty() 
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                ShowEmptyFieldsMessage();
                return true;
            }

            return false;
        }

        private void _ManageRememberMeSettings()
        {
            if (ckbxRememberMe.Checked)
                clsFileHandler.SaveDataToFile(txtUserName.Text, clsPasswordEncryption.EncryptPassword(txtPassword.Text));
            
            else if (clsFileHandler.IsDataInFile())
                clsFileHandler.ClearFileData();
        }

        private void _LoadUserInfo() 
        {
            if (clsFileHandler.IsDataInFile())
            {
                List<string> List = clsFileHandler.LoadDataFromFile();
                
                txtUserName.Text = List[0];
                txtPassword.Text = clsPasswordEncryption.DecryptPassword(List[1]);
                ckbxRememberMe.Checked = true;
            }
        }
        
        private bool _Login(string UserName, string Password)
        {
            if (clsUser.IsUserExistsByUserName(UserName))
            {
                clsGlobal.CurrentUser = clsUser.GetUserInfoByUserName(UserName);

                if (Password == clsGlobal.CurrentUser.Password)
                {

                    if (clsGlobal.CurrentUser.IsActive)
                        {
                            _ShowSuccessLoginMessage();
                            return true;
                        }

                        else
                        {
                            _ShowInactiveUserMessage();
                            return false;
                        }
                }
            }

            _ShowInvalidLoginMessage();
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           if(_IsFieldsEmpty()) 
                return;

            if (_Login(txtUserName.Text, txtPassword.Text))
            {
                _ManageRememberMeSettings();

                frmMainForm MainForm = new frmMainForm();
                this.Hide();
                MainForm.ShowDialog();
                this.Show();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTestForm_Load(object sender, EventArgs e)
        {
           _LoadUserInfo();
        }
    }
}
