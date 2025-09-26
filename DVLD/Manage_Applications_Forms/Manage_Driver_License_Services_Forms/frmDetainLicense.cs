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
using static DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_Local_Driving_License_Applications_Tests.frmAddEditTest;

namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms
{
    public partial class frmDetainLicense : Form
    {
        private DateTime _DetainDate;
        private decimal _FineFees;
        private string _CreatedByUser;
        private clsDetainLicense _DetainLicense;

        public frmDetainLicense()
        {
            InitializeComponent();

            ctrlLicensesFilter1.Handler += _SetUpLoadedLicence;
            _DetainDate = DateTime.Now;
            _FineFees = 0;
            _CreatedByUser = clsGlobal.CurrentUser.UserName;
            _DetainLicense = null;

            LoadApplicationInfo();
        }

        private void LoadApplicationInfo()
        {
            lblDetainDate.Text = _DetainDate.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = _CreatedByUser;
        }

        private bool _IsLicenseActive(clsLicense License)
        {
            if (License.IsActive)
                return true;
            
            MessageBox.Show("This License Is Not Active",
                                          "Not Allowed",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);

            return false;
        }

        private bool _IsLicenseDetained(clsLicense License)
        {
            if (License.IsDetained)
            {

                MessageBox.Show("This License Is Detained",
                                            "Not Allowed",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        private void _SetUpLoadedLicence(object sender)
        {
            if (ctrlLicensesFilter1.GetLicenseCard().License != null)
            {
               
                lblLicenceID.Text = ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID().ToString();

                llblShowLicencesHistory.Enabled = true;

                if (!_IsLicenseDetained(ctrlLicensesFilter1.GetLicenseCard().License) && _IsLicenseActive(ctrlLicensesFilter1.GetLicenseCard().License))
                    btnSave.Enabled = true;
                else
                    btnSave.Enabled = false;
            }

            else
            {
                lblLicenceID.Text = "???";
                llblShowLicencesHistory.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _IsValidFineFees()
        {
            if (string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                errorProvider1.SetError(txtFineFees, "This Field Should Have A Value!");
                return false;
            }
            else if (decimal.TryParse(txtFineFees.Text, out decimal FineFees) && FineFees > 0)
            {
                _FineFees = FineFees;
            }
            else 
            {
                errorProvider1.SetError(txtFineFees, "Please Enter A Valid Decimal Number For The Fine Fees.");
                return false;
            }

            errorProvider1.SetError(txtFineFees, "");
            return true;
        }

        private bool _Save()
        {
            _DetainLicense = new clsDetainLicense();

            _DetainLicense.LicenseID = ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID();
            _DetainLicense.FineFees = _FineFees;
            _DetainLicense.CreatedByUserID = clsGlobal.CurrentUser.GetUserID();

            return _DetainLicense.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_IsValidFineFees())
                return;

            DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
            "Confirm Add",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                if (_Save())
                {
                    MessageBox.Show("The License Has Been Detained Successfully With ID : " + _DetainLicense.GetDetainID().ToString(),
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    lblDetainID.Text = _DetainLicense.GetDetainID().ToString();
                    btnSave.Enabled = false;
                    llblShowLicenceInfo.Enabled = true;
                    ctrlLicensesFilter1.DisableLicenseFilter();
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

        private void llblShowLicencesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonLicenseHistory PersonLicenseHistory = new frmPersonLicenseHistory(ctrlLicensesFilter1.GetLicenseCard().Person.GetPersonID());
            PersonLicenseHistory.ShowDialog();
        }

        private void llblShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseDetails LicenseDetails = new frmLicenseDetails(ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID());
            LicenseDetails.ShowDialog();
        }
    }
}
