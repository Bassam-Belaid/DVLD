using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD
{
    public partial class ctrlLicenseCard : UserControl
    {
        public ctrlLicenseCard()
        {
            InitializeComponent();
        }

        private void _SetDefaultImage(bool DefaultImage)
        {
            pbxPersonImage.Image = (DefaultImage) ? Properties.Resources.Male : Properties.Resources.Female;
        }

        private void _SetCustomImage(string ImagePath)
        {
            try
            {
                using (FileStream FS = new FileStream(ImagePath, FileMode.Open, FileAccess.Read))
                {
                    pbxPersonImage.Image = Image.FromStream(FS);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Image: " + ex.Message, "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPersonInfo(clsPerson Person)
        {
            lblName.Text = Person.GetFullName();
            lblN_N.Text = Person.NationalNo;
            lblGender.Text = Person.GetPersonGender();
            lblDateOfBirth.Text = Person.DateOfBirth.ToString("dd/MM/yyyy");
            pbxGender.Image = Person.Gender ? Properties.Resources.Male : pbxGender.Image = Properties.Resources.Female;

            if (string.IsNullOrEmpty(Person.ImagePath))
                _SetDefaultImage(Person.Gender);

            else
                _SetCustomImage(Person.ImagePath);
        }

        private void LoadLicenseInfo(clsLicense License)
        {
            lblLicenseClass.Text = clsLicenseClass.GetLicenseClassNameByLicenseClassID(License.LicenseClassID);
            lblLicennceID.Text = License.GetLicenseID().ToString();
            lblIssueDate.Text = License.IssueDate.ToString("dd/MM/yyyy");
            lblIssueReason.Text = License.GetIssueReason();
            lblNotes.Text = (License.Notes != null) ? License.Notes : "No Notes";
            lblIsActive.Text = (License.IsActive) ? "Yes" : "No";
            lblDriverID.Text = License.DriverID.ToString();
            lblExpirationDate.Text = License.ExpirationDate.ToString("dd/MM/yyyy");
        }

        public void LoadLicenseDetailsByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID) 
        {
            clsLocalDrivingApplication LocalDrivingApplication = clsLocalDrivingApplication.GetLocalDrivingLicenseApplicationInfoByLDLAppID(LocalDrivingLicenseApplicationID);
            clsPerson Person = clsPerson.GetPersonInfoByPersonID(LocalDrivingApplication.ApplicantPersonID);
            clsLicense License = clsLicense.GetLicenseByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            LoadPersonInfo(Person);
            LoadLicenseInfo(License);
        }
    }
}
