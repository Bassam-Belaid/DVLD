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
    public partial class frmReplacementForDamagedOrLostLicense : Form
    {
        private int _IssueReason;
        private DateTime _ApplicationDate;
        private decimal _ApplicationFees;
        private string _CreatedByUser;
        private clsLicense _License;

        public frmReplacementForDamagedOrLostLicense()
        {
            InitializeComponent();

            _IssueReason = 3;
            ctrlLicensesFilter1.Handler += _SetUpLoadedLicence;
            _ApplicationDate = DateTime.Now;
            _ApplicationFees = clsApplicationType.GetApplicationTypeFeesByApplicationTypeTitle("Replacement for a Damaged Driving License");
            _CreatedByUser = clsGlobal.CurrentUser.UserName;
            _License = null;

            LoadApplicationInfo();
        }

        private void LoadApplicationInfo()
        {
            lblApplicationDate.Text = _ApplicationDate.ToString("dd/MM/yyyy");
            lblApplicationFees.Text = _ApplicationFees.ToString();
            lblCreatedBy.Text = _CreatedByUser;
        }

        private bool _IsLicenseActive(clsLicense License)
        {
            if (License.IsActive)
                return true;

            MessageBox.Show("This License Is Not Active.",
                                        "Not Allowed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

            return false;

        }

        private void _SetUpLoadedLicence(object sender)
        {
            if (ctrlLicensesFilter1.GetLicenseCard().License != null)
            {
                lblOldLicenceID.Text = ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID().ToString();

                llblShowLicencesHistory.Enabled = true;

                if (_IsLicenseActive(ctrlLicensesFilter1.GetLicenseCard().License))
                    btnSave.Enabled = true;
                else
                    btnSave.Enabled = false;
            }

            else
            {
                llblShowLicencesHistory.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _Save()
        {
            _License = ctrlLicensesFilter1.GetLicenseCard().License;
            _License.CreatedByUserID = clsGlobal.CurrentUser.GetUserID();

            return _License.ReplacementForDamagedOrLostLicense(_IssueReason);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
                      "Confirm Add",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                if (_Save())
                {
                    MessageBox.Show("The License Has Been Replacement Successfully With ID : " + _License.GetLicenseID().ToString(),
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    lblApplicationID.Text = _License.ApplicationID.ToString();
                    lblRenewedLicenceID.Text = _License.GetLicenseID().ToString();
                    btnSave.Enabled = false;
                    llblShowLicenceInfo.Enabled = true;
                    ctrlLicensesFilter1.DisableLicenseFilter();
                    gpxRF.Enabled = false;

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
            frmLicenseDetails LicenseDetails = new frmLicenseDetails(_License.GetLicenseID());
            LicenseDetails.ShowDialog();
        }

        private void _SetApplicationFeesType(RadioButton radioButton) 
        {
            string ApplicationType = "";

            if (radioButton == rdbDamagedLicense)
            {
                ApplicationType = "Replacement for a Damaged Driving License";
                _IssueReason = 3;
            }

            else
            {
                ApplicationType = "Replacement for a Lost Driving License";
                _IssueReason = 2;
            }

            _ApplicationFees = clsApplicationType.GetApplicationTypeFeesByApplicationTypeTitle(ApplicationType);
            lblApplicationFees.Text = _ApplicationFees.ToString();
        }

        private void SetApplicationType(object sender, EventArgs e)
        {
            _SetApplicationFeesType((RadioButton)sender);
        }
    }
}
