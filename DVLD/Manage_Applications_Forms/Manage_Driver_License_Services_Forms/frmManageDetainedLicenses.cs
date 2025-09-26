using DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_New_Driver_License_Forms;
using DVLD.Manage_People_Forms;
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

namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms
{
    public partial class frmManageDetainedLicenses : Form
    {
        private enum _enFilter
        {

            eNone = 0,
            eDetainID = 1,
            eIsReleased = 2,
            eNationalNo = 3,
            eFullName = 4,
            eReleaseApplicationID = 5,
        }

        private enum _enReleasedStatusFilter
        {
            eAll = 0,
            eReleased = 1,
            eNotReleased = 2,
        }

        private _enFilter _Filter;

        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewDetain_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eDetainLicense))
            {
                frmDetainLicense DetainLicense = new frmDetainLicense();
                DetainLicense.ShowDialog();
                _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicenses());
            }
        }

        private void btnReleaseDetain_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eReleaseDetainedLicense))
            {
                frmReleaseDetainedLicense ReleaseDetainedLicense = new frmReleaseDetainedLicense();
                ReleaseDetainedLicense.ShowDialog();
                _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicenses());
            }
        }

        private void _LoadDetainedLicensesList(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
            lblNumberOfRecords.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "D.ID";
            dataTable.Columns[1].ColumnName = "L.ID";
            dataTable.Columns[2].ColumnName = "D.Date";
            dataTable.Columns[3].ColumnName = "Is Released";
            dataTable.Columns[4].ColumnName = "Fine Fees";
            dataTable.Columns[5].ColumnName = "Release Date";
            dataTable.Columns[6].ColumnName = "N.No";
            dataTable.Columns[7].ColumnName = "Full Name";
            dataTable.Columns[8].ColumnName = "Release App ID";
        }

        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbxDetainedLicensesFilter.SelectedItem = "None";
            _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicenses()); 
        }

        private _enFilter _GetFilterType(string Filter)
        {
            txtSearchInput.Text = "";

            switch (Filter)
            {

                case "Detain ID":
                    return _enFilter.eDetainID;
                
                case "Is Released":
                    return _enFilter.eIsReleased;

                case "National No":
                    return _enFilter.eNationalNo;

                case "Full Name":
                    return _enFilter.eFullName;

                case "Release Application ID":
                    return _enFilter.eReleaseApplicationID;

                default:
                    return _enFilter.eNone;
            }
        }

        private _enReleasedStatusFilter _GetReleasedStatusFilterType(string ReleasedStatusFilter)
        {
            switch (ReleasedStatusFilter)
            {
                case "Released":
                    return _enReleasedStatusFilter.eReleased;
                case "Not Released":
                    return _enReleasedStatusFilter.eNotReleased;
                default:
                    return _enReleasedStatusFilter.eAll;
            }
        }

        private void _FilterByDetainID()
        {
            if (int.TryParse(txtSearchInput.Text, out int DetainID))
            {
                _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicensesFilteredByDetainID(DetainID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid Detain ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FilterByNationalNo()
        {
            _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicensesFilteredByNationalNo(txtSearchInput.Text));
        }

        private void _FilterByFullName()
        {
            _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicensesFilteredByFullName(txtSearchInput.Text));
        }

        private void _FilterByReleaseApplicationID()
        {
            if (int.TryParse(txtSearchInput.Text, out int ReleaseApplicationID))
            {
                _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicensesFilteredByReleaseApplicationID(ReleaseApplicationID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid Release Application ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FilterLocalDrivingLicenseApplicationsList(_enFilter Filter)
        {
            if (string.IsNullOrEmpty(txtSearchInput.Text))
            {
                _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicenses());
                return;
            }

            switch (Filter)
            {

                case _enFilter.eDetainID:
                    _FilterByDetainID();
                    break;

                case _enFilter.eNationalNo:
                    _FilterByNationalNo();
                    break;

                case _enFilter.eFullName:
                    _FilterByFullName();
                    break;

                case _enFilter.eReleaseApplicationID:
                    _FilterByReleaseApplicationID();
                    break;
            }

        }

        private void _FilterByReleasedStatus(_enReleasedStatusFilter ReleasedStatusFilter)
        {
            switch (ReleasedStatusFilter)
            {
                case _enReleasedStatusFilter.eReleased:
                    _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicensesFilteredByReleaseStatus(true));
                    break;

                case _enReleasedStatusFilter.eNotReleased:
                    _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicensesFilteredByReleaseStatus(false));
                    break;

                default:
                    _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicenses());
                    break;
            }
        }

        private void showApplicationDetialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());

            clsPerson Person = clsPerson.GetPersonInfoByPersonID(clsDriver.GetDriverInfoByDriverID(clsLicense.GetLicenseByLicenseID(LicenseID).DriverID).GetPersonID());

            frmPersonDetails PersonDetails = new frmPersonDetails(Person.GetPersonID());
            PersonDetails.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());

            frmLicenseDetails LicenseDetails = new frmLicenseDetails(LicenseID);
            LicenseDetails.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());

            clsPerson Person = clsPerson.GetPersonInfoByPersonID(clsDriver.GetDriverInfoByDriverID(clsLicense.GetLicenseByLicenseID(LicenseID).DriverID).GetPersonID());
            frmPersonLicenseHistory PersonLicenseHistory = new frmPersonLicenseHistory(Person.GetPersonID());
            PersonLicenseHistory.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eReleaseDetainedLicense))
            {
                int LicenseID = int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                frmReleaseDetainedLicense ReleaseDetainedLicense = new frmReleaseDetainedLicense(LicenseID);
                ReleaseDetainedLicense.ShowDialog();
                _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicenses());
            }
        }

        private void cmsManageDetainedLicensesList_Opened(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            
            clsLicense License = clsLicense.GetLicenseByLicenseID(LicenseID);

            releaseDetainedLicenseToolStripMenuItem.Enabled = License.IsDetained;
        }

        private void cbxDetainedLicensesFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            _Filter = _GetFilterType(cbxDetainedLicensesFilter.SelectedItem.ToString());
            txtSearchInput.Visible = (_Filter != _enFilter.eNone);
            txtSearchInput.Visible = (_Filter != _enFilter.eNone && _Filter != _enFilter.eIsReleased);
            cbxReleaseStatus.Visible = (_Filter == _enFilter.eIsReleased);

            if (_Filter == _enFilter.eNone)
                _LoadDetainedLicensesList(clsDetainLicense.GetAllDetainLicenses());

            if (cbxReleaseStatus.Visible)
                cbxReleaseStatus.SelectedItem = "All";
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterLocalDrivingLicenseApplicationsList(_Filter);
        }

        private void cbxReleaseStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            _FilterByReleasedStatus(_GetReleasedStatusFilterType(cbxReleaseStatus.SelectedItem.ToString()));
        }
    }
}
