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
using static DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_Local_Driving_License_Applications_Tests.frmAddEditTest;

namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_Local_Driving_License_Applications_Tests
{
    public partial class frmTakeTest : Form
    {

        private clsLocalDrivingApplication _LocalDrivingApplication;
        private clsTestType.enTestTypes _TestType;
        private decimal _TestPaidFees;
        private DateTime _AppointmentDate;
        private clsTest _Test;
        private int _TestAppointmentID;

        public frmTakeTest(clsTestType.enTestTypes TestType, int LDLAppID, int TestAppointmentID)
        {
            InitializeComponent();
            _LocalDrivingApplication = clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationInfoByLDLAppID(LDLAppID);
            _TestType = TestType;
            _TestPaidFees = clsTestType.GetTestTypeFeesByTestType(_TestType);
            _AppointmentDate = DateTime.Now;
            _TestAppointmentID = TestAppointmentID;
            _Test = null;
            lblTestDate.Text = clsTestAppointment.GetTestAppointmentDateForLocalDrivingLicenseApplication(TestType, LDLAppID).ToString("dd/MM/yyyy");
            _SetTestTypeDetails();
            _LoadLocalDrivingApplicationInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _TestResult() 
        {
            if(rdbPass.Checked)
                return true;

            return false;
        }

        private bool _Save()
        {
            _Test = new clsTest();
            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult = _TestResult();
            _Test.Notes = (string.IsNullOrEmpty(txtNotes.Text)) ? null : txtNotes.Text;
            _Test.CreatedByUserID = clsGlobal.CurrentUser.GetUserID();

            return _Test.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
                            "Confirm Take",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                if (_Save())
                {
                    MessageBox.Show("The Test Has Been Taken Successfully",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    lblTestID.Text = _Test.GetTestID().ToString();
                    btnSave.Enabled = false;
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

        private void _LoadLocalDrivingApplicationInfo()
        {
            lblDLAppID.Text = _LocalDrivingApplication.GetLocalDrivingApplicationID().ToString();
            lblLicenseClass.Text = clsLicenseClass.GetLicenseClassNameByLicenseClassID(_LocalDrivingApplication.LicenseClassID);
            lblApplicantName.Text = clsPerson.GetPersonFullNameByPersonID(_LocalDrivingApplication.ApplicantPersonID);
            lblTrials.Text = clsTest.CountNumberOfTestTrials(_LocalDrivingApplication.GetLocalDrivingApplicationID(), _TestType).ToString();
            lblTestFees.Text = _TestPaidFees.ToString();
        }

        private void _SetTestTypeDetails()
        {
            gpxTestType.Text = clsTestType.GetTestTypeTitleByTestTypeID(_TestType);

            switch (_TestType)
            {
                case clsTestType.enTestTypes.eVisionTest:
                    pbxTestIcon.Image = Properties.Resources.Vision_Test_2;
                    break;

                case clsTestType.enTestTypes.eWrittenTest:
                    pbxTestIcon.Image = Properties.Resources.Written_Test;
                    break;

                case clsTestType.enTestTypes.eStreetTest:
                    pbxTestIcon.Image = Properties.Resources.Street_Test;
                    break;
            }
        }
    }
}
