using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD.Manage_Applications_Forms.Manage_Test_Types_Forms
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();

            _LoadTestTypesList(clsTestType.GetAllTestTypes());
        }

        private void _LoadTestTypesList(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
            lblNumberOfRecords.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "ID";
            dataTable.Columns[1].ColumnName = "Title";
            dataTable.Columns[2].ColumnName = "Description";
            dataTable.Columns[3].ColumnName = "Fees";
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.Permissions.eEditTestType))
            {
                int TestTypeID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                frmUpdateTestTypes UpdateTestTypes = new frmUpdateTestTypes(TestTypeID);
                UpdateTestTypes.ShowDialog();
                _LoadTestTypesList(clsTestType.GetAllTestTypes());
            }
        }
    }
}
