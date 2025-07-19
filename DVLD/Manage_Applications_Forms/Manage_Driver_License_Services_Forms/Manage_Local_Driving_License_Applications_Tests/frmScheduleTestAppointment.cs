using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_Local_Driving_License_Applications_Tests
{
    public partial class frmScheduleTestAppointment : Form
    {
        private clsTestType.enTestTypes _TestType;
        private string _TestTypeTitle;

        public frmScheduleTestAppointment(clsTestType.enTestTypes TestType, int LDLAppID)
        {
            InitializeComponent();
            _TestType = TestType;
            _TestTypeTitle = clsTestType.GetTestTypeTitleByTestTypeID(_TestType);
            _TestTypeTitle = "Schedule " + _TestTypeTitle + " Appointments";
            this.Text = _TestTypeTitle;
            lblTestAppointmentTitle.Text = _TestTypeTitle;
            _SetTestTypeDetails();
            ctrlLocalDrivingLicenseApplicationCard1.LoadLocalDrivingLicenseApplicationInfoByLDLAppID(LDLAppID);
            _AppointmentsListForLocalDrivingLicenseApplication(clsTestAppointment.GetAllTestAppointmentsForLocalDrivingLicenseApplication(LDLAppID, _TestType));
        }

        private void _AppointmentsListForLocalDrivingLicenseApplication(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
            lblNumberOfRecords.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "Appointment ID";
            dataTable.Columns[1].ColumnName = "Appointment Date";
            dataTable.Columns[2].ColumnName = "Paid Fees";
            dataTable.Columns[3].ColumnName = "Is Locked";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (!clsTestAppointment.IsHasATestAppointmentForLocalDrivingLicenseApplication(ctrlLocalDrivingLicenseApplicationCard1.GetLoadedLocalDrivingLicenseApplicationID(), clsTestType.enTestTypes.eVisionTest))
            {
                frmAddEditTest AddEditTest = new frmAddEditTest(clsTestType.enTestTypes.eVisionTest, ctrlLocalDrivingLicenseApplicationCard1.GetLoadedLocalDrivingLicenseApplicationID());
                AddEditTest.ShowDialog();
            }

            else 
            {
                MessageBox.Show("Person Already Have An Active Appointment For This Test, You Can't Add New Appointment .",
                                    "Not Allowed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
        }

        private void _LoadVisionTest()
        {
            pbxTestIcon.Image = Properties.Resources.Vision_Test_2;
        }

        private void _SetTestTypeDetails()
        {
            switch (_TestType)
            {
                case clsTestType.enTestTypes.eVisionTest:
                    _LoadVisionTest();
                    break;
            }
        }
    }
}
