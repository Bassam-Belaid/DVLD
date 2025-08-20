using DVLDBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Manage_Drivers_Forms
{
    public partial class frmManageDrivers : Form
    {
        private enum _enFilter
        {

            eNone = 0,
            eDriverID = 1,
            ePersonID = 2,
            eNationalNo = 3,
            eFullName = 4,
        }

        private _enFilter _Filter;

        public frmManageDrivers()
        {
            InitializeComponent();
        }

        private void _LoadDriversList(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
            lblNumberOfRecords.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "Driver ID";
            dataTable.Columns[1].ColumnName = "Person ID";
            dataTable.Columns[2].ColumnName = "National No";
            dataTable.Columns[3].ColumnName = "Full Name";
            dataTable.Columns[4].ColumnName = "Date";
            dataTable.Columns[5].ColumnName = "Active Licenses";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            cbxManageDriversFilter.SelectedItem = "None";
            _LoadDriversList(clsDriver.GetAllDrivers());
        }

        private _enFilter _GetFilterType(string Filter)
        {

            txtSearchInput.Text = "";

            switch (Filter)
            {
                case "Driver ID":
                    return _enFilter.eDriverID;
                case "Person ID":
                    return _enFilter.ePersonID;
                case "National No":
                    return _enFilter.eNationalNo;
                case "Full Name":
                    return _enFilter.eFullName;
                default:
                    return _enFilter.eNone;

            }
        }

        private void _FilterByDriverID()
        {
            if (int.TryParse(txtSearchInput.Text, out int DriverID))
            {
                _LoadDriversList(clsDriver.GetDriversFilteredByDriverID(DriverID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid Driver ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FilterByPersonID()
        {
            if (int.TryParse(txtSearchInput.Text, out int PersonID))
            {
                _LoadDriversList(clsDriver.GetDriversFilteredByPersonID(PersonID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid Person ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FilterByNationalNo()
        {

            _LoadDriversList(clsDriver.GetPeopleFilteredByNationalNo(txtSearchInput.Text));

        }

        private void _FilterByFullName()
        {

            _LoadDriversList(clsDriver.GetPeopleFilteredByFullname(txtSearchInput.Text));

        }

        private void _FilterDriversList(_enFilter Filter)
        {

            if (string.IsNullOrEmpty(txtSearchInput.Text))
            {
                _LoadDriversList(clsDriver.GetAllDrivers());
                return;
            }

            switch (Filter)
            {
                case _enFilter.eDriverID:
                    _FilterByDriverID();
                    break;

                case _enFilter.ePersonID:
                    _FilterByPersonID();
                    break;

                case _enFilter.eNationalNo:
                    _FilterByNationalNo();
                    break;

                case _enFilter.eFullName:
                    _FilterByFullName();
                    break;
            }

        }

        private void cbxManageDriversFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            _Filter = _GetFilterType(cbxManageDriversFilter.SelectedItem.ToString());
            txtSearchInput.Visible = (_Filter != _enFilter.eNone);
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterDriversList(_Filter);
        }
    }
}
