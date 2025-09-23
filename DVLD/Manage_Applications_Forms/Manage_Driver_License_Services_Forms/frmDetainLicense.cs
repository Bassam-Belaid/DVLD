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
    public partial class frmDetainLicense : Form
    {
        private DateTime _DetainDate;
        private decimal _FineFees;
        private string _CreatedByUser;
        private clsLicense _License;

        public frmDetainLicense()
        {
            InitializeComponent();

            ctrlLicensesFilter1.Handler += _SetUpLoadedLicence;
            _DetainDate = DateTime.Now;
            _CreatedByUser = clsGlobal.CurrentUser.UserName;
            _License = null;

            LoadApplicationInfo();
        }

        private void LoadApplicationInfo()
        {
            lblDetainDate.Text = _DetainDate.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = _CreatedByUser;
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

                if (!_IsLicenseDetained(ctrlLicensesFilter1.GetLicenseCard().License))
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

        private void btnSave_Click(object sender, EventArgs e)
        {

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

        private void txtFineFees_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtFineFees.Text))
                return;

            if (decimal.TryParse(txtFineFees.Text, out decimal FineFees))
                _FineFees = FineFees;

            else
            {
                MessageBox.Show("Please Enter A Valid Decimal Number For The Fine Fees.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFineFees.Text = txtFineFees.Text.Substring(0, txtFineFees.Text.Length - 1);
            }
        }
    }
}
