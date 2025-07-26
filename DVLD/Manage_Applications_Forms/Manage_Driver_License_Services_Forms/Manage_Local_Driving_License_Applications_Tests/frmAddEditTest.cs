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
        public enum enMode { eAddNew = 0, eUpdate = 1}

        private enMode _Mode;

        private clsLocalDrivingApplication _LocalDrivingApplication;
        private clsTestType.enTestTypes _TestType;
        private decimal _TestPaidFees;
        private decimal _RetakeTestFees;

        private DateTime _AppointmentDate;

        public frmAddEditTest(enMode Mode, clsTestType.enTestTypes TestType, int LDLAppID)
        {
            InitializeComponent();
            _Mode = Mode;
            _LocalDrivingApplication = clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationInfoByLDLAppID(LDLAppID);
            _TestType = TestType;
            _TestPaidFees = clsTestType.GetTestTypeFeesByTestType(_TestType);
            _RetakeTestFees = 0;
            _AppointmentDate = DateTime.Now;
            _SetTestTypeDetails();
            _LoadLocalDrivingApplicationInfo();
            _SetRetakeTestInfoStatus();
            _SetFormMode();
        }

        private void _SetFormMode() 
        {
            switch (_Mode) 
            {
                case enMode.eAddNew:
                    btnSave.Text = "Save";
                    break;
                
                case enMode.eUpdate:
                    btnSave.Text = "Update";
                    if (gbxRetakeTestInfo.Enabled)
                        lblRetakeAppID.Text = _LocalDrivingApplication.GetApplicationID().ToString();
                    break;
            }
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
            dtpTestDate.MinDate = _AppointmentDate;
            lblTrials.Text = clsTest.CountNumberOfTestTrials(_LocalDrivingApplication.GetLocalDrivingApplicationID(), _TestType).ToString();
            lblTestFees.Text = _TestPaidFees.ToString();
            lblTotalFees.Text = _TestPaidFees.ToString();
        }

        private void _SetRetakeTestInfoStatus() 
        {    
            gbxRetakeTestInfo.Enabled = (clsTest.CountNumberOfTestTrials(_LocalDrivingApplication.GetLocalDrivingApplicationID(), _TestType) != 0);
           
            if (gbxRetakeTestInfo.Enabled)
                _LoadRetakeTestInfo();
        }

        private void _LoadRetakeTestInfo() 
        {
            _RetakeTestFees = clsApplicationType.GetApplicationTypeFeesByApplicationTypeTitle("Retake Test");
            lblRetakeTestFees.Text = _RetakeTestFees.ToString();
            lblTotalFees.Text = (_TestPaidFees + _RetakeTestFees).ToString();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _Save() 
        {
            switch (_Mode)
            {
                case enMode.eAddNew:
                    return clsTestAppointment.AddNewTestAppointmentForLocalDrivingLicenseApplication(_TestType, _LocalDrivingApplication.GetLocalDrivingApplicationID(), _AppointmentDate, _TestPaidFees, clsGlobal.CurrentUser.GetUserID());
                default:
                    return clsTestAppointment.UpdateTestAppointmentForLocalDrivingLicenseApplication(_TestType, _LocalDrivingApplication.GetLocalDrivingApplicationID(), _AppointmentDate);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Message = (_Mode == enMode.eAddNew) ? "The Test Appointment Has Been Added Successfully" : "The Test Appointment Has Been Updated Successfully";
            string Title = (_Mode == enMode.eAddNew) ? "Confirm Add" : "Confirm Update";

            DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
                            Title,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                if (_Save())
                {
                    MessageBox.Show(Message,
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    this.Close();
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

        private void dtpTestDate_ValueChanged(object sender, EventArgs e)
        {
            _AppointmentDate = (DateTime)dtpTestDate.Value;
        }
    }
}
