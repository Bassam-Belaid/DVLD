using DVLDBusinessLayer;
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

namespace DVLD
{
    public partial class ctrlInternationalLicenseCard : UserControl
    {
        public clsPerson Person;
        public clsDriver Driver;
        public clsInternationalLicense InternationalLicense;

        public ctrlInternationalLicenseCard()
        {
            InitializeComponent();
       
            Person = null;
            InternationalLicense = null;
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

        private void _LoadLicenseInfo(clsInternationalLicense InternationalLicense)
        {
            lblAppID.Text = InternationalLicense.GetApplicationID().ToString();
            lblLicennceID.Text = InternationalLicense.GetInternationalLicenseID().ToString();
            lblIssueDate.Text = InternationalLicense.IssueDate.ToString("dd/MM/yyyy");
            lblIsActive.Text = (InternationalLicense.IsActive) ? "Yes" : "No";
            lblDriverID.Text = InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = InternationalLicense.ExpirationDate.ToString("dd/MM/yyyy");
        }

        private void _Clear()
        {
            lblName.Text = "???";
            lblN_N.Text = "???";
            lblGender.Text = "???";
            lblDateOfBirth.Text = "???";
            pbxGender.Image = Properties.Resources.Male;
            _SetDefaultImage(true);

            lblLicennceID.Text = "???";
            lblIssueDate.Text = "???";
            lblIsActive.Text = "???";
            lblDriverID.Text = "???";
            lblExpirationDate.Text = "???";

            Person = null;
            InternationalLicense = null;
            Driver = null;

        }

        public void LoadLicenseDetailsByLicenseID(int InternationalLicenseID)
        {
            InternationalLicense = clsInternationalLicense.GetInternationalLicenseByInternationalLicenseID(InternationalLicenseID);

            if (InternationalLicense != null)
            {
                Driver = clsDriver.GetDriverInfoByDriverID(InternationalLicense.DriverID);
                Person = clsPerson.GetPersonInfoByPersonID(Driver.GetPersonID());

                _LoadPersonInfo(Person);
                _LoadLicenseInfo(InternationalLicense);
            }

            else
            {
                MessageBox.Show("No License Found With ID " + InternationalLicenseID.ToString() + ".", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _Clear();
            }
        }
    }
}
