using DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_New_International_Driver_License_Forms;
using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLD.Manage_People_Forms;
using DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms;

namespace DVLD.Manage_Applications_Forms
{
    public partial class frmManageInternationalDrivingLicenseApplications : Form
    {

        private DataTable _InternationalDrivingLicenseApplicationsList;
        private enum _enFilter
        {
            eNone = 0,
            eIntLicenseID = 1,
            eDriverID = 2,
            eLLicenseID = 3,
        }

        private _enFilter _Filter;

        public frmManageInternationalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadInternationalDrivingLicenseApplicationsList(DataTable dataTable)
        { 
            dataGridView1.DataSource = dataTable;
            lblNumberOfRecords.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "Int.License ID";
            dataTable.Columns[1].ColumnName = "Application ID";
            dataTable.Columns[2].ColumnName = "Driver ID";
            dataTable.Columns[3].ColumnName = "L.License ID";
            dataTable.Columns[4].ColumnName = "Issue Date";
            dataTable.Columns[5].ColumnName = "Expiration Date";
            dataTable.Columns[6].ColumnName = "Is Active";

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalDrivingLicenseApplication AddNewInternationalDrivingLicenseApplication = new frmAddNewInternationalDrivingLicenseApplication();
            AddNewInternationalDrivingLicenseApplication.ShowDialog();
        }

        private void frmManageInternationalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            cbxLocalDrivingLicenseApplicationsFilter.SelectedItem = "None";
            _InternationalDrivingLicenseApplicationsList = clsInternationalLicense.GetAllInternationalLicenseApllications();
            _LoadInternationalDrivingLicenseApplicationsList(_InternationalDrivingLicenseApplicationsList);
        }

        private _enFilter _GetFilterType(string Filter)
        {

            txtSearchInput.Text = "";

            switch (Filter)
            {

                case "Int.License ID":
                    return _enFilter.eIntLicenseID;
                case "Driver ID":
                    return _enFilter.eDriverID;
                case "L.License ID":
                    return _enFilter.eLLicenseID;
                default:
                    return _enFilter.eNone;
            }
        }

        private void _FilterLocalDrivingLicenseApplicationsList(_enFilter Filter)
        {
            if (string.IsNullOrEmpty(txtSearchInput.Text))
            {
                _InternationalDrivingLicenseApplicationsList = clsInternationalLicense.GetAllInternationalLicenseApllications();
                _LoadInternationalDrivingLicenseApplicationsList(_InternationalDrivingLicenseApplicationsList); 
                return;
            }

            switch (Filter)
            {

                case _enFilter.eIntLicenseID:
                    _FilterByIntLicenseID();
                    break;

                case _enFilter.eDriverID:
                    _FilterByDriverID();
                    break;

                case _enFilter.eLLicenseID:
                    _FilterByLLicenseID();
                    break;

            }

        }

        private void _FilterByIntLicenseID()
        {
            if (int.TryParse(txtSearchInput.Text, out int IntLicenseID))
            {
                _LoadInternationalDrivingLicenseApplicationsList(clsInternationalLicense.GetInternationalLicenseApplicationsFilteredByIntLicenseID(_InternationalDrivingLicenseApplicationsList, IntLicenseID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid International Driving License ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FilterByDriverID()
        {
            if (int.TryParse(txtSearchInput.Text, out int DriverID))
            {
                _LoadInternationalDrivingLicenseApplicationsList(clsInternationalLicense.GetInternationalLicenseApplicationsFilteredByDriverID(_InternationalDrivingLicenseApplicationsList, DriverID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid Driver ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FilterByLLicenseID()
        {
            if (int.TryParse(txtSearchInput.Text, out int LLicenseID))
            {
                _LoadInternationalDrivingLicenseApplicationsList(clsInternationalLicense.GetInternationalLicenseApplicationsFilteredByLLicenseID(_InternationalDrivingLicenseApplicationsList, LLicenseID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid Local Driving License ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void showApplicationDetialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = int.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());

            clsPerson Person = clsPerson.GetPersonInfoByPersonID(clsDriver.GetDriverInfoByDriverID(DriverID).GetPersonID());

            frmPersonDetails PersonDetails = new frmPersonDetails(Person.GetPersonID());
            PersonDetails.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            frmInternationalLicenseDetails InternationalLicenseDetails = new frmInternationalLicenseDetails(LicenseID);
            InternationalLicenseDetails.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = int.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());

            clsPerson Person = clsPerson.GetPersonInfoByPersonID(clsDriver.GetDriverInfoByDriverID(DriverID).GetPersonID());
            frmPersonLicenseHistory PersonLicenseHistory = new frmPersonLicenseHistory(Person.GetPersonID());
            PersonLicenseHistory.ShowDialog();
        }
    }
}
