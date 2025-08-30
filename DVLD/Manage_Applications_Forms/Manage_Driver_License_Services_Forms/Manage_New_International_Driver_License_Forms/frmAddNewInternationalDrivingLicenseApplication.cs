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

namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms.Manage_New_International_Driver_License_Forms
{
    public partial class frmAddNewInternationalDrivingLicenseApplication : Form
    {
        private DateTime _ApplicationDate;
        private DateTime _IssueDate;
        private decimal _Fees;
        private DateTime _ExpirationDate;
        private string _CreatedByUser;
        private clsInternationalLicense _InternationalLicense;

        public frmAddNewInternationalDrivingLicenseApplication()
        {
            InitializeComponent();

            ctrlLicensesFilter1.Handler += SetUpLoadedLicence;

            _ApplicationDate = DateTime.Now;
            _IssueDate = DateTime.Now;
            _Fees = clsInternationalLicense.ApplicationFees;
            _ExpirationDate = DateTime.Now.AddYears(1);
            _CreatedByUser = clsUser.GetUserNameByUserID(clsGlobal.CurrentUser.GetUserID());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadInternationalDrivingLicenseApplicationInfo() 
        {
            lblApplicationDate.Text = _ApplicationDate.ToString("dd/MM/yyyy");
            lblIssueDate.Text = _IssueDate.ToString("dd/MM/yyyy");
            lblApplicationFees.Text = _Fees.ToString();
            lblExpirationDate.Text = _ExpirationDate.ToString("dd/MM/yyyy");
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

        private bool _IsLicenseExpired(clsLicense License)
        {
            if (License.IsExpired())
            {

                MessageBox.Show("This License Is Expired.",
                                            "Not Allowed",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);

                return true;
            }

            return false;
        }

        private bool _IsLicenseAnOrdinaryDrivingLicense(clsLicense License)
        {
            if (License.IsLicenseAnOrdinaryDrivingLicense())   
                return true;

            MessageBox.Show("This License Is Not Ordinary Driving License.",
                                           "Not Allowed",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);


            return false;
        }

        private bool _Save()
        {   
                _InternationalLicense = new clsInternationalLicense();

                _InternationalLicense.ApplicantPersonID = ctrlLicensesFilter1.GetLicenseCard().Person.GetPersonID();
                _InternationalLicense.DriverID = ctrlLicensesFilter1.GetLicenseCard().Driver.GetDriverID();
                _InternationalLicense.IssuedUsingLocalLicenseID = ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID();

                return _InternationalLicense.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = clsInternationalLicense.IsApplicantHasAnActiveInternationalLicense(ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID());

            if (InternationalLicenseID != -1)
            {
                MessageBox.Show("Person Already Has International License With License ID " + InternationalLicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_IsLicenseActive(ctrlLicensesFilter1.GetLicenseCard().License))
                return;

            if (_IsLicenseExpired(ctrlLicensesFilter1.GetLicenseCard().License))
                return;

            if (!_IsLicenseAnOrdinaryDrivingLicense(ctrlLicensesFilter1.GetLicenseCard().License))
                return;

            DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
                        "Confirm Add",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                if (_Save())
                {
                    MessageBox.Show("The License Has Been Added Successfully",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    lblApplicationID.Text = _InternationalLicense.GetApplicationID().ToString();
                    lblILLicenceID.Text = _InternationalLicense.GetInternationalLicenseID().ToString();
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

        private void frmAddNewInternationalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _LoadInternationalDrivingLicenseApplicationInfo();
        }

        private void SetUpLoadedLicence(object sender) 
        {
            if (ctrlLicensesFilter1.GetLicenseCard().License != null)
            {
                lblLocalLicenceID.Text = ctrlLicensesFilter1.GetLicenseCard().License.GetLicenseID().ToString();
                llblShowLicencesHistory.Enabled = true;
                btnSave.Enabled = true;
            }

            else
            {
                lblLocalLicenceID.Text = "???";
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
            frmInternationalLicenseDetails InternationalLicenseDetails = new frmInternationalLicenseDetails(_InternationalLicense.GetInternationalLicenseID());
            InternationalLicenseDetails.ShowDialog();
        }
    }
}
