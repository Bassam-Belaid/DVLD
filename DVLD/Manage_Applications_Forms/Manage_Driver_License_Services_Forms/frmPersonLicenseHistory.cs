using DVLDBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms
{
    public partial class frmPersonLicenseHistory : Form
    {
        private int _PersonID;

        public frmPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void _LoadInternationalLicensesHistory(DataTable dataTable)
        {
            dgvInternationalLicenses.DataSource = dataTable;
            
            lblNumberOfRecords2.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "Int.License.ID";
            dataTable.Columns[1].ColumnName = "Application.ID";
            dataTable.Columns[2].ColumnName = "L.License.ID";
            dataTable.Columns[3].ColumnName = "Issue Date";
            dataTable.Columns[4].ColumnName = "Expiration Date";
            dataTable.Columns[5].ColumnName = "Is Active";
        }

        private void _LoadLocalLicensesHistory(DataTable dataTable)
        {
            dgvLocalLicenses.DataSource = dataTable;
            lblNumberOfRecords1.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "Lic.ID";
            dataTable.Columns[1].ColumnName = "App.ID";
            dataTable.Columns[2].ColumnName = "Class Name";
            dataTable.Columns[3].ColumnName = "Issue Date";
            dataTable.Columns[4].ColumnName = "Expiration Date";
            dataTable.Columns[5].ColumnName = "Is Active";
        }

        private void frmPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            ctrlPersonsFilter1.LoadPersonInfo(_PersonID);
            _LoadLocalLicensesHistory(clsLicense.GetAllLocalLicensesForApplicant(_PersonID));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbcMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tbcMenu.SelectedIndex == 0)
                _LoadLocalLicensesHistory(clsLicense.GetAllLocalLicensesForApplicant(_PersonID));

            else
                _LoadInternationalLicensesHistory(clsInternationalLicense.GetAllInternationalLicensesForApplicant(_PersonID));
        }
    }
}
