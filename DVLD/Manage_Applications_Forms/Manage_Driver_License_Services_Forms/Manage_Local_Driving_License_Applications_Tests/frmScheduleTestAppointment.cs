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

            int labelWidth = lblTestAppointmentTitle.Width;
            int otherControlWidth = pbxTestIcon.Width;

            int newX = pbxTestIcon.Left + (otherControlWidth / 2) - (labelWidth / 2);

            lblTestAppointmentTitle.Location = new Point(newX, lblTestAppointmentTitle.Top);

            _LoadTestTypeIcon();
            ctrlLocalDrivingLicenseApplicationCard1.LoadLocalDrivingLicenseApplicationInfoByLDLAppID(LDLAppID);
            _AppointmentsListForLocalDrivingLicenseApplication(clsTestAppointment.GetAllTestAppointmentsForLocalDrivingLicenseApplication(_TestType, LDLAppID));
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
            if (!clsTestAppointment.IsHasPassedTestForLocalDrivingLicenseApplication(_TestType, ctrlLocalDrivingLicenseApplicationCard1.GetLoadedLocalDrivingLicenseApplicationID()))
            {
                if (!clsTestAppointment.IsHasATestAppointmentForLocalDrivingLicenseApplication(_TestType, ctrlLocalDrivingLicenseApplicationCard1.GetLoadedLocalDrivingLicenseApplicationID()))
                {
                    frmAddEditTest AddEditTest = new frmAddEditTest(frmAddEditTest.enMode.eAddNew, _TestType, ctrlLocalDrivingLicenseApplicationCard1.GetLoadedLocalDrivingLicenseApplicationID());
                    AddEditTest.ShowDialog();
                    _AppointmentsListForLocalDrivingLicenseApplication(clsTestAppointment.GetAllTestAppointmentsForLocalDrivingLicenseApplication(_TestType, ctrlLocalDrivingLicenseApplicationCard1.GetLoadedLocalDrivingLicenseApplicationID()));
                }

                else
                {
                    MessageBox.Show("Person Already Have An Active Appointment For This Test, You Can't Add New Appointment.",
                                        "Not Allowed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                }
            }

            else
            {
                MessageBox.Show("Person Already Have Passed The Test, You Can't Add New Appointment.",
                                    "Not Allowed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
        }

        private void _LoadTestTypeIcon()
        {
            switch (_TestType)
            {
                case clsTestType.enTestTypes.eVisionTest:
                    pbxTestIcon.Image = Properties.Resources.Vision_Test_2;
                    break;

                case clsTestType.enTestTypes.eWrittenTest:
                    pbxTestIcon.Image = Properties.Resources.Written_Test;
                    break;

                case clsTestType.enTestTypes.eStreetTest:
                    pbxTestIcon.Image = Properties.Resources.Street_Test;
                    break;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditTest AddEditTest = new frmAddEditTest(frmAddEditTest.enMode.eUpdate, _TestType, ctrlLocalDrivingLicenseApplicationCard1.GetLoadedLocalDrivingLicenseApplicationID());
            AddEditTest.ShowDialog();
            _AppointmentsListForLocalDrivingLicenseApplication(clsTestAppointment.GetAllTestAppointmentsForLocalDrivingLicenseApplication(_TestType, ctrlLocalDrivingLicenseApplicationCard1.GetLoadedLocalDrivingLicenseApplicationID()));
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            frmTakeTest TakeTest = new frmTakeTest(_TestType, ctrlLocalDrivingLicenseApplicationCard1.GetLoadedLocalDrivingLicenseApplicationID(), TestAppointmentID);
            TakeTest.ShowDialog();
            _AppointmentsListForLocalDrivingLicenseApplication(clsTestAppointment.GetAllTestAppointmentsForLocalDrivingLicenseApplication(_TestType, ctrlLocalDrivingLicenseApplicationCard1.GetLoadedLocalDrivingLicenseApplicationID()));
            ctrlLocalDrivingLicenseApplicationCard1.Refresh();
        }

        private void _EnableTestAppointmentsMenuConfig()
        {
            for (byte i = 0; i < cmsManageTestAppointments.Items.Count; i++)
            {
                cmsManageTestAppointments.Items[i].Enabled = true;
            }
        }

        private void _DisableTestAppointmentsMenuConfig()
        {
            for (byte i = 0; i < cmsManageTestAppointments.Items.Count; i++)
            {
                cmsManageTestAppointments.Items[i].Enabled = false;
            }
        }

        private void cmsManageTestAppointments_Opening(object sender, CancelEventArgs e)
        {
            int TestAppointmentID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            if (!clsTestAppointment.IsTestAppointmentForLocalDrivingLicenseApplicationIsActive(TestAppointmentID))
                _DisableTestAppointmentsMenuConfig();
            else
                _EnableTestAppointmentsMenuConfig();
        }
    }
}
