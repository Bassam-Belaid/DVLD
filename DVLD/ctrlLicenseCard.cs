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
        public clsPerson Person;
        public clsDriver Driver;
        public clsLicense License;

        public ctrlLicenseCard()
        {
            InitializeComponent();
            
            Person = null;
            License = null;
            Driver = null;
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

        private void _LoadPersonInfo(clsPerson Person)
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

        private void _LoadLicenseInfo(clsLicense License)
        {
            lblLicenseClass.Text = clsLicenseClass.GetLicenseClassNameByLicenseClassID(License.LicenseClassID);
            lblLicennceID.Text = License.GetLicenseID().ToString();
            lblIssueDate.Text = License.IssueDate.ToString("dd/MM/yyyy");
            lblIssueReason.Text = License.GetIssueReason();
            lblNotes.Text = (License.Notes != null) ? License.Notes : "No Notes";
            lblIsActive.Text = (License.IsActive) ? "Yes" : "No";
            lblIsDetained.Text = (License.IsDetained) ? "Yes" : "No";
            lblDriverID.Text = License.DriverID.ToString();
            lblExpirationDate.Text = License.ExpirationDate.ToString("dd/MM/yyyy");
        }

        private void _Clear() 
        {
            lblName.Text = "???";
            lblN_N.Text = "???";
            lblGender.Text = "???";
            lblDateOfBirth.Text = "???";
            pbxGender.Image = Properties.Resources.Male;
            _SetDefaultImage(true);

            lblLicenseClass.Text = "???";
            lblLicennceID.Text = "???";
            lblIssueDate.Text = "???";
            lblIssueReason.Text = "???";
            lblNotes.Text = "???";
            lblIsActive.Text = "???";
            lblDriverID.Text = "???";
            lblExpirationDate.Text = "???";

            Person = null;
            License = null;
            Driver = null;

        }

        public void LoadLicenseDetailsByLicenseID(int LicenseID)
        {
            License = clsLicense.GetLicenseByLicenseID(LicenseID);

            if (License != null)
            {
                Driver = clsDriver.GetDriverInfoByDriverID(License.DriverID);
                Person = clsPerson.GetPersonInfoByPersonID(Driver.GetPersonID());

                _LoadPersonInfo(Person);
                _LoadLicenseInfo(License);
            }

            else 
            {
                MessageBox.Show("No License Found With ID " + LicenseID.ToString() + ".", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _Clear();
            }
        }
    }
}
