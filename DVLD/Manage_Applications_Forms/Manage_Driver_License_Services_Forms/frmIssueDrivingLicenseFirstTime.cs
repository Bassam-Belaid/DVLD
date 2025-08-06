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
using static DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_Local_Driving_License_Applications_Tests.frmAddEditTest;

namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms
{
    public partial class frmIssueDrivingLicenseFirstTime : Form
    {
        private clsLicense _License;

        public frmIssueDrivingLicenseFirstTime(int LDLAppID)
        {
            InitializeComponent();

            _License = null;
            ctrlLocalDrivingLicenseApplicationCard1.LoadLocalDrivingLicenseApplicationInfoByLDLAppID(LDLAppID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _Save() 
        {
            _License = new clsLicense();

            _License.ApplicationID = ctrlLocalDrivingLicenseApplicationCard1.LocalDrivingApplication.GetApplicationID();
            _License.LicenseClassID = ctrlLocalDrivingLicenseApplicationCard1.LocalDrivingApplication.LicenseClassID;
            _License.Notes = txtNotes.Text;
            _License.CreatedByUserID = clsGlobal.CurrentUser.GetUserID();

            return _License.Save();   
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
                            "Confirm Issue",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                if (_Save())
                {
                    MessageBox.Show("The Driving License Has Been Issued Successfully With ID = " + _License.GetLicenseID().ToString(),
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The Operation Was Canceled. Please Check Your Information And Try Again.",
                                    "Operation Canceled",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
    }
}
