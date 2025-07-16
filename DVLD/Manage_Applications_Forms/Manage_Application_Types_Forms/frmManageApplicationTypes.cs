using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD.Manage_Applications_Forms.Manage_Application_Types_Forms
{
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();

            _LoadApplicationsList(clsApplicationType.GetAllApplicationTypes());
        }

        private void _LoadApplicationsList(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
            lblNumberOfRecords.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "ID";
            dataTable.Columns[1].ColumnName = "Title";
            dataTable.Columns[2].ColumnName = "Fees";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eEditApplicationType))
            {
                int ApplicationTypeID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                frmUpdateApplicationType UpdateApplicationType = new frmUpdateApplicationType(ApplicationTypeID);
                UpdateApplicationType.ShowDialog();
                _LoadApplicationsList(clsApplicationType.GetAllApplicationTypes());
            }
        }
    }
}
