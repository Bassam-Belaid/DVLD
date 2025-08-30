using DVLDBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_New_Driver_License_Forms
{
    public partial class frmAddEditLocalDrivingLicenseApplication : Form
    {
        private enum _enMode { eAddNew = 0, eUpdate = 1 };

        private _enMode _Mode;

        private int _PersonID;
        private bool _IsInfoLoaded;

        private clsLocalDrivingApplication _LocalDrivingApplication;

        public frmAddEditLocalDrivingLicenseApplication(int LDLAppID = -1)
        {
            InitializeComponent();

            if (LDLAppID != -1)
            {
                _Mode = _enMode.eUpdate;
                lblTitle.Text = "Update Local Driving License Application";
                _LocalDrivingApplication = clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationInfoByLDLAppID(LDLAppID);
                _PersonID = _LocalDrivingApplication.ApplicantPersonID;
                ctrlPersonsFilter1.LoadPersonInfo(_PersonID);
                _LoadApplicationInfo();
            }
            else
            {
                _IsInfoLoaded = false;
            }
        }

        private bool _IsPersonSelected()
        {
            _PersonID = ctrlPersonsFilter1.GetLoadedPersonID();

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

        private void _LoadAllLicenseClasses()
        {
            DataTable dataTable = clsLicenseClass.GetAllLicenseClasses();
            foreach (DataRow row in dataTable.Rows)
            {
                cbxLicenseClasses.Items.Add(row["ClassName"].ToString());
            }

            cbxLicenseClasses.SelectedItem = clsLicenseClass._DefaultSelectedClass;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadApplicationInfo() 
        {
            _LoadAllLicenseClasses();
            cbxLicenseClasses.SelectedItem = clsLicenseClass.GetLicenseClassNameByLicenseClassID(_LocalDrivingApplication.LicenseClassID);
            lblApplicationID.Text = _LocalDrivingApplication.GetLocalDrivingApplicationID().ToString();
            lblApplicationDate.Text = _LocalDrivingApplication.ApplicationDate.ToString("dd/MM/yyyy");
            lblApplicationFees.Text = _LocalDrivingApplication.PaidFees.ToString();
            lblCreatedUser.Text = clsUser.GetUserNameByUserID(_LocalDrivingApplication.CreatedByUserID);
            _IsInfoLoaded = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tbcMenu.SelectedIndex = 1;

                if (!_IsInfoLoaded)
                {
                    _LoadAllLicenseClasses();
                    lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    lblApplicationFees.Text = clsLocalDrivingApplication.ApplicationFees.ToString();
                    lblCreatedUser.Text = clsGlobal.CurrentUser.UserName;
                    _IsInfoLoaded = true;
                }
        }

        private void tbcMenu_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tbcMenu.SelectedIndex == 1 && !_IsPersonSelected())
                tbcMenu.SelectedIndex = 0;
        }

        private bool _IsApplicantHasAnActiveLocalDrivingLicenseApplicationWithSameLicenseClass(int ApplicantPersonID, string LicenseClass) 
        {
            int LocalDrivingApplicationID = clsLocalDrivingApplication.IsApplicantHasAnActiveLocalDrivingLicenseApplicationWithSameLicenseClass(ApplicantPersonID, LicenseClass);
           
            if (LocalDrivingApplicationID != -1) 
            {
                MessageBox.Show("Choose Onther License Class, The Selected Person Already Have An Active Application With Selected Class With ID = " + LocalDrivingApplicationID,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                return true;
            }

            return false;
        }

        private bool _IsApplicantHasAnActiveLicenseWithSameLicenseClass(int ApplicantPersonID, string LicenseClass)
        {
            int LicenseID = clsLocalDrivingApplication.IsApplicantHasAnActiveLicenseWithSameLicenseClass(ApplicantPersonID, LicenseClass);

            if (LicenseID != -1)
            {
                MessageBox.Show("Choose Onther License Class, The Selected Person Already Have An Active License With Selected Class With ID = " + LicenseID,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                return true;
            }

            return false;
        }

        private bool _Save()
        {
            if (_Mode == _enMode.eAddNew)
            {
                _LocalDrivingApplication = new clsLocalDrivingApplication();
                _LocalDrivingApplication.ApplicantPersonID = _PersonID;
            }

            _LocalDrivingApplication.LicenseClassID = clsLicenseClass.GetLicenseClassIDByLicenseClassName(_GetSelectedLicenseClass());

            return _LocalDrivingApplication.Save();
        }

        private string _GetSelectedLicenseClass() 
        {
            string SelectedLicenseClass = cbxLicenseClasses.SelectedItem.ToString();
            
            return SelectedLicenseClass;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string Message = (_Mode == _enMode.eAddNew) ? "The Application Has Been Added Successfully" : "The Application Has Been Updated Successfully";
            string Title = (_Mode == _enMode.eAddNew) ? "Confirm Add" : "Confirm Update";

                DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
                            Title,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (_IsApplicantHasAnActiveLicenseWithSameLicenseClass(_PersonID, _GetSelectedLicenseClass()) || _IsApplicantHasAnActiveLocalDrivingLicenseApplicationWithSameLicenseClass(_PersonID, _GetSelectedLicenseClass()))
                return;

            if (Result == DialogResult.Yes)
            {
                if (_Save())
                {
                    MessageBox.Show(Message,
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    lblApplicationID.Text = _LocalDrivingApplication.GetLocalDrivingApplicationID().ToString();
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
