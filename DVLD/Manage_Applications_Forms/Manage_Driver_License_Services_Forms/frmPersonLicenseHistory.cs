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
    public partial class frmPersonLicenseHistory : Form
    {
        private clsLocalDrivingApplication _LocalDrivingApplication;

        public frmPersonLicenseHistory(int LocalDrivingApplicationID)
        {
            InitializeComponent();
            _LocalDrivingApplication = clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationInfoByLDLAppID(LocalDrivingApplicationID);
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
            ctrlPersonsFilter1.LoadPersonInfo(_LocalDrivingApplication.ApplicantPersonID);
            _LoadLocalLicensesHistory(clsLicense.GetAllLocalLicensesForApplicant(_LocalDrivingApplication.ApplicantPersonID));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
