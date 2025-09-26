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
    public partial class frmReleaseDetainedLicense : Form
    {
        private decimal _ApplicationFees;
        private decimal _TotalFees;
        private clsDetainLicense _DetainLicense;

        public frmReleaseDetainedLicense(int LicenseID = -1)
        {
            InitializeComponent();

            if (LicenseID == -1)
            {

                ctrlLicensesFilter1.Handler += _SetUpLoadedLicence;
                _ApplicationFees = 0;
                _TotalFees = 0;
                _DetainLicense = null;
            }

            else 
            {

                ctrlLicensesFilter1.GetLicenseCard().LoadLicenseDetailsByLicenseID(LicenseID);

                lblLicenceID.Text = ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID().ToString();
                llblShowLicencesHistory.Enabled = true;

                _DetainLicense = clsDetainLicense.GetDetainedLicenseByLicenseID(ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID());
                lblDetainID.Text = _DetainLicense.GetDetainID().ToString();
                lblDetainDate.Text = _DetainLicense.DetainDate.ToString("dd/MM/yyyy");
                _ApplicationFees = clsApplicationType.GetApplicationTypeFeesByApplicationTypeTitle("Release Detained Driving License");
                lblApplicationFees.Text = _ApplicationFees.ToString();
                lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
                lblFineFees.Text = _DetainLicense.FineFees.ToString();
                _TotalFees = _ApplicationFees + _DetainLicense.FineFees;
                lblTotalFees.Text = _TotalFees.ToString();
                btnSave.Enabled = true;
                ctrlLicensesFilter1.DisableLicenseFilter(LicenseID);
            }
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
            if (!License.IsDetained)
            {

                MessageBox.Show("This License Is Not Detained",
                                            "Not Allowed",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void _SetUpLoadedLicence(object sender)
        {
            if (ctrlLicensesFilter1.GetLicenseCard().License != null)
            {

                lblLicenceID.Text = ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID().ToString();

                llblShowLicencesHistory.Enabled = true;

                if (_IsLicenseDetained(ctrlLicensesFilter1.GetLicenseCard().License) && _IsLicenseActive(ctrlLicensesFilter1.GetLicenseCard().License))
                {
                    _DetainLicense = clsDetainLicense.GetDetainedLicenseByLicenseID(ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID());
                    lblDetainID.Text = _DetainLicense.GetDetainID().ToString();
                    lblDetainDate.Text = _DetainLicense.DetainDate.ToString("dd/MM/yyyy");
                    _ApplicationFees = clsApplicationType.GetApplicationTypeFeesByApplicationTypeTitle("Release Detained Driving License");
                    lblApplicationFees.Text = _ApplicationFees.ToString();
                    lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
                    lblFineFees.Text = _DetainLicense.FineFees.ToString();
                    _TotalFees = _ApplicationFees + _DetainLicense.FineFees;
                    lblTotalFees.Text = _TotalFees.ToString();

                    btnSave.Enabled = true;
                }
                else
                    btnSave.Enabled = false;
            }

            else
            {
                lblDetainID.Text = "???";
                lblDetainDate.Text = "???";
                lblApplicationFees.Text = "???";
                lblTotalFees.Text = "???";
                lblLicenceID.Text = "???";
                lblCreatedBy.Text = "???";
                lblFineFees.Text = "???";

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
           _DetainLicense.IsReleased = true;
           _DetainLicense.ReleaseDate = DateTime.Now;
           _DetainLicense.ReleasedByUserID = clsGlobal.CurrentUser.GetUserID();

            return _DetainLicense.ReleaseDetain();
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
                    MessageBox.Show("The License Has Been Released Successfully",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    lblApplicationID.Text = _DetainLicense.ReleaseApplicationID.ToString();
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
