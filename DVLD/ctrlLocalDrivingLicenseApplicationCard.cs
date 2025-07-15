using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Manage_People_Forms;

namespace DVLD
{
    public partial class ctrlLocalDrivingLicenseApplicationCard : UserControl
    {
        private clsLocalDrivingApplication _LocalDrivingApplication;

        public ctrlLocalDrivingLicenseApplicationCard()
        {
            InitializeComponent();

            _LocalDrivingApplication = null;
        }

        private string _NumberOfPassedTests() 
        {
            return clsLocalDrivingApplication.NumberOfTestsThatTakenByLocalDrivingLicenseApplication(_LocalDrivingApplication.GetLocalDrivingApplicationID()).ToString() + "/" + clsTestType.NumberOfTestTypes.ToString();
        }

        private void _SetApplicantIcon(bool Gender) 
        {
            pbxApplicant.Image = (Gender) ? Properties.Resources.Male : Properties.Resources.Female;
        }

        public void LoadLocalDrivingLicenseApplicationInfoByLDLAppID(int LDLAppID) 
        {
            _LocalDrivingApplication = clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationInfoByLDLAppID(LDLAppID);

            lblDLAppID.Text = LDLAppID.ToString();
            lblLicenseClass.Text = clsLicenseClass.GetLicenseClassNameByLicenseClassID(_LocalDrivingApplication.LicenseClassID);
            lblPassedTests.Text = _NumberOfPassedTests();

            lblApplicationID.Text = _LocalDrivingApplication.GetApplicationID().ToString();
            lblApplicationStatus.Text = _LocalDrivingApplication.GetApplicationStatus();
            lblApplicationFees.Text = _LocalDrivingApplication.PaidFees.ToString();
            lblApplicationType.Text = clsApplicationType.GetApplicationTypeTitleByApplicationTypeID(_LocalDrivingApplication.ApplicationTypeID);
            lblApplicant.Text = clsPerson.GetPersonFullNameByPersonID(_LocalDrivingApplication.ApplicantPersonID);
            lblApplicationDate.Text = _LocalDrivingApplication.ApplicationDate.ToString("dd/MM/yyyy");
            lblLastStatusDate.Text = _LocalDrivingApplication.LastStatusDate.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = clsUser.GetUserNameByUserID(_LocalDrivingApplication.CreatedByUserID);
            _SetApplicantIcon(clsPerson.GetPersonGenderByPersonID(_LocalDrivingApplication.ApplicantPersonID));
        }

        private void llblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.Permissions.eShowPersonDetails))
            {
                frmPersonDetails frmPersonDetails = new frmPersonDetails(_LocalDrivingApplication.ApplicantPersonID);
                frmPersonDetails.ShowDialog();
                LoadLocalDrivingLicenseApplicationInfoByLDLAppID(_LocalDrivingApplication.GetLocalDrivingApplicationID());
            }
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
