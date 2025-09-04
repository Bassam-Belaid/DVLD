using System;
using System.Windows.Forms;
using DVLD.Manage_Applications_Forms;
using DVLD.Manage_Applications_Forms.Manage_Application_Types_Forms;
using DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms;
using DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_New_Driver_License_Forms;
using DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_New_International_Driver_License_Forms;
using DVLD.Manage_Applications_Forms.Manage_Test_Types_Forms;
using DVLD.Manage_Drivers_Forms;
using DVLD.Manage_People_Forms;
using DVLD.Manage_Users_Forms;
using DVLDBusinessLayer;

namespace DVLD
{
    public partial class frmMainForm : Form
    {
        public frmMainForm()
        {
            InitializeComponent();
        }

        private void _Logout()
        {
            clsGlobal.CurrentUser = null;
            this.Close();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eManagePeople))
            {
                frmManagePeople ManagePeople = new frmManagePeople();
                ManagePeople.ShowDialog();
            }
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eManageDrivers))
            {
                frmManageDrivers ManageDrivers = new frmManageDrivers();
                ManageDrivers.ShowDialog();
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eManageUsers))
            {
                frmManageUsers ManageUsers = new frmManageUsers();
                ManageUsers.ShowDialog();
            }
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails UserDetails = new frmUserDetails(clsGlobal.CurrentUser.GetUserID());
            UserDetails.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword ChangePassword = new frmChangePassword(clsGlobal.CurrentUser.GetUserID(), clsGlobal.CurrentUser.Password);
            ChangePassword.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure You Want To Log Out ?",
                            "Confirm Log Out",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
                _Logout();
        }

        private void frmMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clsGlobal.IsUserLoggedIn())
            {

                MessageBox.Show("To Close The App Please Go To Account Settings And Log Out.",
                        "Close Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void manageApplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eManageApplicationTypes))
            {
                frmManageApplicationTypes ManageApplicationTypes = new frmManageApplicationTypes();
                ManageApplicationTypes.ShowDialog();
            }
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eManageTestTypes))
            {
                frmManageTestTypes ManageTestTypes = new frmManageTestTypes();
                ManageTestTypes.ShowDialog();
            }
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eAddNewLocalDrivingLicenseApplication))
            {
                frmAddEditLocalDrivingLicenseApplication AddNewLocalDrivingLicenseApplication = new frmAddEditLocalDrivingLicenseApplication();
                AddNewLocalDrivingLicenseApplication.ShowDialog();
            }
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eManageLocalDrivingLicenseApplications))
            {
            frmManageLocalDrivingLicenseApplications ManageLocalDrivingLicenseApplications = new frmManageLocalDrivingLicenseApplications();
            ManageLocalDrivingLicenseApplications.ShowDialog();
            }
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eAddNewInternationlLicense))
            {
                frmAddNewInternationalDrivingLicenseApplication AddNewInternationalDrivingLicenseApplication = new frmAddNewInternationalDrivingLicenseApplication();
                AddNewInternationalDrivingLicenseApplication.ShowDialog();
            }
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eManageInternationalLicenseApplications))
            {
                frmManageInternationalDrivingLicenseApplications ManageInternationalDrivingLicenseApplications = new frmManageInternationalDrivingLicenseApplications();
                ManageInternationalDrivingLicenseApplications.ShowDialog();
            }
        }

        private void renewDriverLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eRenewDriverLicense))
            {
                frmRenewDriverLicense RenewDriverLicense = new frmRenewDriverLicense();
                RenewDriverLicense.ShowDialog();
            }
        }
    }
}
