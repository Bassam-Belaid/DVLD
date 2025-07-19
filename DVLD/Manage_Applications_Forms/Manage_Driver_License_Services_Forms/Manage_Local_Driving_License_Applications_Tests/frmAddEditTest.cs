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

namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_Local_Driving_License_Applications_Tests
{
    public partial class frmAddEditTest : Form
    {
        private clsLocalDrivingApplication _LocalDrivingApplication;
        private clsTestType.enTestTypes _TestType;

        public frmAddEditTest(clsTestType.enTestTypes TestType, int LDLAppID)
        {
            InitializeComponent();
            _LocalDrivingApplication = clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationInfoByLDLAppID(LDLAppID);
            _TestType = TestType;
            _SetTestTypeDetails();
            _LoadLocalDrivingApplicationInfo();
        }

        private void _SetApplicantIcon(bool Gender)
        {
            pbxApplicant.Image = (Gender) ? Properties.Resources.Male : Properties.Resources.Female;
        }

        private void _LoadLocalDrivingApplicationInfo() 
        {
            lblDLAppID.Text = _LocalDrivingApplication.GetLocalDrivingApplicationID().ToString();
            lblLicenseClass.Text = clsLicenseClass.GetLicenseClassNameByLicenseClassID(_LocalDrivingApplication.LicenseClassID);
            lblApplicantName.Text = clsPerson.GetPersonFullNameByPersonID(_LocalDrivingApplication.ApplicantPersonID);
            _SetApplicantIcon(clsPerson.GetPersonGenderByPersonID(_LocalDrivingApplication.ApplicantPersonID));
            dtpTestDate.MinDate = DateTime.Now;
            lblTrials.Text = clsTest.CountNumberOfTestTrials(_LocalDrivingApplication.GetLocalDrivingApplicationID(), _TestType).ToString();
            lblTestFees.Text = clsTestType.GetTestTypeFeesByTestType(_TestType).ToString();
        }

        private void _LoadVisionTest() 
        {
            gpxTestType.Text = clsTestType.GetTestTypeTitleByTestTypeID(clsTestType.enTestTypes.eVisionTest);
            pbxTestIcon.Image = Properties.Resources.Vision_Test_2;
        }

        private void _SetTestTypeDetails() 
        {
            switch (_TestType) 
            {
                case clsTestType.enTestTypes.eVisionTest:
                    _LoadVisionTest(); 
                    break;
            }
        }
    }
}
