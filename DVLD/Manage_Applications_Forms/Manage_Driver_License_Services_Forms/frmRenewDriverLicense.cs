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
    public partial class frmRenewDriverLicense : Form
    {
        private DateTime _ApplicationDate;
        private DateTime _IssueDate;
        private decimal _ApplicationFees;
        private decimal _LicenseFees;
        private decimal _TotalFees;
        private DateTime _ExpirationDate;
        private string _CreatedByUser;
        private clsLicense _License;

        public frmRenewDriverLicense()
        {
            InitializeComponent();

            ctrlLicensesFilter1.Handler += _SetUpLoadedLicence;
            _ApplicationDate = DateTime.Now;
            _IssueDate = DateTime.Now;
            _ApplicationFees = clsApplicationType.GetApplicationTypeFeesByApplicationTypeTitle("Renew Driving License Service");
            _CreatedByUser = clsGlobal.CurrentUser.UserName;
            _License = null;

            LoadApplicationInfo();
        }

        private void LoadApplicationInfo() 
        {
            lblApplicationDate.Text = _ApplicationDate.ToString("dd/MM/yyyy");
            lblIssueDate.Text = _IssueDate.ToString("dd/MM/yyyy");
            lblApplicationFees.Text = _ApplicationFees.ToString();
            lblCreatedBy.Text = _CreatedByUser;
        }

        private bool _IsLicenseExpired(clsLicense License)
        {
            if (!License.IsExpired())
            {

                MessageBox.Show("This License Is Not Expired Yet, It Will Expired On : " + ctrlLicensesFilter1.GetLicenseCard().License.ExpirationDate.ToString("dd/MM/yyyy"),
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
                _LicenseFees = clsLicenseClass.GetLicenseClassFeesByLicenseClassName(clsLicenseClass.GetLicenseClassNameByLicenseClassID(ctrlLicensesFilter1.GetLicenseCard().License.LicenseClassID));
                lblLicenceFees.Text = _LicenseFees.ToString();

                _TotalFees = (_ApplicationFees + _LicenseFees);
                lblTotalFees.Text = _TotalFees.ToString();

                _ExpirationDate = DateTime.Now.AddYears(ctrlLicensesFilter1.GetLicenseCard().License.ExpirationDate.Year - ctrlLicensesFilter1.GetLicenseCard().License.IssueDate.Year);
                lblExpirationDate.Text = _ExpirationDate.ToString("dd/MM/yyyy");
                
                lblOldLicenceID.Text = ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID().ToString();

                llblShowLicencesHistory.Enabled = true;

                if (!_IsLicenseExpired(ctrlLicensesFilter1.GetLicenseCard().License))
                    btnSave.Enabled = true;
                else
                    btnSave.Enabled = false;
            }

            else
            {
                lblLicenceFees.Text = "???";
                lblTotalFees.Text = "???";
                lblExpirationDate.Text = "???";
                lblOldLicenceID.Text = "???";
                llblShowLicencesHistory.Enabled = false;
                btnSave.Enabled = false;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _Save()
        {
            _License = ctrlLicensesFilter1.GetLicenseCard().License;

            _License.CreatedByUserID = clsGlobal.CurrentUser.GetUserID();
            _License.ExpirationDate = _ExpirationDate;
            _License.Notes = txtNotes.Text;

            return _License.RenewLicense();
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
                    MessageBox.Show("The License Has Been Renewed Successfully With ID : " + _License.GetLicenseID().ToString(),
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    lblApplicationID.Text = _License.ApplicationID.ToString();
                    lblRenewedLicenceID.Text = _License.GetLicenseID().ToString();
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
    }
}
