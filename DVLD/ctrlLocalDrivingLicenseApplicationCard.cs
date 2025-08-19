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
        public clsLocalDrivingApplication LocalDrivingApplication;

        public ctrlLocalDrivingLicenseApplicationCard()
        {
            InitializeComponent();

            LocalDrivingApplication = null;
        }

        private string _NumberOfPassedTests() 
        {
            return clsLocalDrivingApplication.NumberOfTestsThatTakenByApplicantForLocalDrivingLicenseApplication(LocalDrivingApplication.GetLocalDrivingApplicationID()).ToString() + "/" + clsTestType.NumberOfTestTypes.ToString();
        }

        private void _SetApplicantIcon(bool Gender) 
        {
            pbxApplicant.Image = (Gender) ? Properties.Resources.Male : Properties.Resources.Female;
        }

        public void LoadLocalDrivingLicenseApplicationInfoByLDLAppID(int LDLAppID) 
        {
            LocalDrivingApplication = clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationInfoByLDLAppID(LDLAppID);

            lblDLAppID.Text = LDLAppID.ToString();
            lblLicenseClass.Text = clsLicenseClass.GetLicenseClassNameByLicenseClassID(LocalDrivingApplication.LicenseClassID);
            lblPassedTests.Text = _NumberOfPassedTests();

            lblApplicationID.Text = LocalDrivingApplication.GetApplicationID().ToString();
            lblApplicationStatus.Text = LocalDrivingApplication.GetApplicationStatus();
            lblApplicationFees.Text = LocalDrivingApplication.PaidFees.ToString();
            lblApplicationType.Text = clsApplicationType.GetApplicationTypeTitleByApplicationTypeID(LocalDrivingApplication.ApplicationTypeID);
            lblApplicant.Text = clsPerson.GetPersonFullNameByPersonID(LocalDrivingApplication.ApplicantPersonID);
            lblApplicationDate.Text = LocalDrivingApplication.ApplicationDate.ToString("dd/MM/yyyy");
            lblLastStatusDate.Text = LocalDrivingApplication.LastStatusDate.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = clsUser.GetUserNameByUserID(LocalDrivingApplication.CreatedByUserID);
            _SetApplicantIcon(clsPerson.GetPersonGenderByPersonID(LocalDrivingApplication.ApplicantPersonID));
        }

        private void llblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
                frmPersonDetails frmPersonDetails = new frmPersonDetails(LocalDrivingApplication.ApplicantPersonID);
                frmPersonDetails.ShowDialog();
                LoadLocalDrivingLicenseApplicationInfoByLDLAppID(LocalDrivingApplication.GetLocalDrivingApplicationID());
            
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    
        public int GetLoadedLocalDrivingLicenseApplicationID() 
        {
            int LocalDrivingApplicationID = LocalDrivingApplication.GetLocalDrivingApplicationID();
            
            if (LocalDrivingApplicationID != -1)
                return LocalDrivingApplicationID;
            
            return -1;
        }

        public void Refresh() 
        {
            lblPassedTests.Text = _NumberOfPassedTests();
        }
    }
}
