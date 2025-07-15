using DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_Local_Driving_License_Applications_Tests;
using DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_New_Driver_License_Forms;
using DVLDBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;


namespace DVLD.Manage_Applications_Forms
{
    public partial class frmManageLocalDrivingLicenseApplications : Form
    {
        private enum _enFilter
        {

            eNone = 0,
            eLDLAppID = 1,
            eNationalNo = 2,
            eFullName = 3,
            eStatus = 4,

        }

        private _enFilter _Filter;

        public frmManageLocalDrivingLicenseApplications()
        {
            InitializeComponent();
            cbxLocalDrivingLicenseApplicationsFilter.SelectedItem = "None";
            _LocalDrivingLicenseApplicationsList(clsLocalDrivingApplication.GetAllLocalDrivingLicenseApplications());
        }

        private void _LocalDrivingLicenseApplicationsList(DataTable dataTable)
        {

            dataGridView1.DataSource = dataTable;
            lblNumberOfRecords.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "L.D.L App ID";
            dataTable.Columns[1].ColumnName = "Driving Class";
            dataTable.Columns[2].ColumnName = "National No";
            dataTable.Columns[3].ColumnName = "Full Name";
            dataTable.Columns[4].ColumnName = "Application Date";
            dataTable.Columns[5].ColumnName = "Passed Tests";
            dataTable.Columns[6].ColumnName = "Status";

        }

        private _enFilter _GetFilterType(string Filter)
        {

            txtSearchInput.Text = "";

            switch (Filter)
            {

                case "L.D.L App ID":
                    return _enFilter.eLDLAppID;
                case "National No":
                    return _enFilter.eNationalNo;
                case "Full Name":
                    return _enFilter.eFullName;
                case "Status":
                    return _enFilter.eStatus;
                default:
                    return _enFilter.eNone;

            }
        }

        private void _FilterByLDLAppID()
        {
            if (int.TryParse(txtSearchInput.Text, out int LDLAppID))
            {
                _LocalDrivingLicenseApplicationsList(clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationsFilteredByLDLAppID(LDLAppID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid Local Driving License Application ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FilterByNationalNo()
        {
            _LocalDrivingLicenseApplicationsList(clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationsFilteredByNationalNo(txtSearchInput.Text));
        }

        private void _FilterByFullName()
        {
            _LocalDrivingLicenseApplicationsList(clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationsFilteredByFullName(txtSearchInput.Text));
        }

        private void _FilterByStatus()
        {
            _LocalDrivingLicenseApplicationsList(clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationsFilteredByStatus(txtSearchInput.Text));
        }

        private void _FilterLocalDrivingLicenseApplicationsList(_enFilter Filter)
        {
            if (string.IsNullOrEmpty(txtSearchInput.Text))
            {
                _LocalDrivingLicenseApplicationsList(clsLocalDrivingApplication.GetAllLocalDrivingLicenseApplications());
                return;
            }

            switch (Filter)
            {

                case _enFilter.eLDLAppID:
                    _FilterByLDLAppID();
                    break;

                case _enFilter.eNationalNo:
                    _FilterByNationalNo();
                    break;

                case _enFilter.eFullName:
                    _FilterByFullName();
                    break;

                case _enFilter.eStatus:
                    _FilterByStatus();
                    break;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.Permissions.eAddNewLocalDrivingLicenseApplication))
            {
                 frmAddNewLocalDrivingLicenseApplication AddNewLocalDrivingLicenseApplication = new frmAddNewLocalDrivingLicenseApplication();
                 AddNewLocalDrivingLicenseApplication.ShowDialog();
                 _LocalDrivingLicenseApplicationsList(clsLocalDrivingApplication.GetAllLocalDrivingLicenseApplications());

            }
        }

        private void cbxLocalDrivingLicenseApplicationsFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            _Filter = _GetFilterType(cbxLocalDrivingLicenseApplicationsFilter.SelectedItem.ToString());
            txtSearchInput.Visible = (_Filter != _enFilter.eNone);
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterLocalDrivingLicenseApplicationsList(_Filter);
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.Permissions.eCancelLocalDrivingLicenseApplication))
            {
                int LocalDrivingApplicationID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                DialogResult Result = MessageBox.Show("Are You Sure You Want To Cancel The Application [" + LocalDrivingApplicationID.ToString() + "] ?",
                                "Confirm Cancel",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                if (Result == DialogResult.Yes)
                {
                    if (clsLocalDrivingApplication.CancelLocalDrivingLicenseApplication(LocalDrivingApplicationID))
                    {
                        MessageBox.Show("The Application Has Been Canceled Successfully",
                                        "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        _LocalDrivingLicenseApplicationsList(clsLocalDrivingApplication.GetAllLocalDrivingLicenseApplications());
                    }
                    else
                    {
                        MessageBox.Show("Failed To Cancel The Application. Please Try Again.",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void deleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.Permissions.eDeleteLocalDrivingLicenseApplication))
            {
                int LocalDrivingApplicationID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                DialogResult Result = MessageBox.Show("Are You Sure You Want To Delete The Application [" + LocalDrivingApplicationID.ToString() + "] ?",
                                "Confirm Delete",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                if (Result == DialogResult.Yes)
                {
                    if (clsLocalDrivingApplication.DeleteLocalDrivingLicenseApplication(LocalDrivingApplicationID))
                    {
                        MessageBox.Show("The Application Has Been Deleted Successfully",
                                        "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        _LocalDrivingLicenseApplicationsList(clsLocalDrivingApplication.GetAllLocalDrivingLicenseApplications());
                    }
                    else
                    {
                        MessageBox.Show("Failed To Delete The Application. Please Try Again.",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void _DisableAllTests() 
        {
            visionTestToolStripMenuItem.Enabled = false;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            scheduleStreetTestToolStripMenuItem.Enabled = false;
        }

        private void _SetTestsStatus(int LocalDrivingApplicationID) 
        {
           byte NumberOfTakenTests = clsLocalDrivingApplication.NumberOfTestsThatTakenByLocalDrivingLicenseApplication(LocalDrivingApplicationID);

            if (NumberOfTakenTests == 255)
            {
                visionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
                return;
            }
            if (NumberOfTakenTests == clsTestType.NumberOfTestTypes) 
            {
                visionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
                return;

            }
            else if (NumberOfTakenTests == 0)
            {
                visionTestToolStripMenuItem.Enabled = true;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
                return;

            }
            else if (NumberOfTakenTests == 1)
            {
                visionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = true;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
                return;

            }
            else if (NumberOfTakenTests == 2)
            {
                visionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = true;
                return;
            }
        }

        private void scheduleTestsToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            int LocalDrivingApplicationID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            _SetTestsStatus(LocalDrivingApplicationID);
        }
       
        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingApplicationID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            frmScheduleVisionTestAppointment ScheduleVisionTestAppointment = new frmScheduleVisionTestAppointment(LocalDrivingApplicationID);
            ScheduleVisionTestAppointment.ShowDialog();
            _LocalDrivingLicenseApplicationsList(clsLocalDrivingApplication.GetAllLocalDrivingLicenseApplications());
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
