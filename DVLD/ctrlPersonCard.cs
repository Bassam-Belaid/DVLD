using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using DVLD.Manage_People_Forms;
using DVLDBusinessLayer;

namespace DVLD
{
    public partial class ctrlPersonCard : UserControl
    {
        clsPerson _Person;

        public ctrlPersonCard()
        {
            InitializeComponent();
            _Person = null;
        }

        public void SetDefaultImage(bool DefaultImage)
        {
            pbxPersonImage.Image = (DefaultImage) ? Properties.Resources.Male : Properties.Resources.Female;
        }

        public void SetCustomImage(string ImagePath)
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

        public void LoadPersonInfoByPersonID(int PersonID)
        {
            _Person = clsPerson.GetPersonInfoByPersonID(PersonID);
            _LoadPersonInfo();
        }

        public void LoadPersonInfoByNationalNo(string NationalNo)
        {
            _Person = clsPerson.GetPersonInfoByNationalNo(NationalNo);
            _LoadPersonInfo();
        }

        public void LoadPersonInfoByEmail(string Email)
        {
            _Person = clsPerson.GetPersonInfoByEmail(Email);
            _LoadPersonInfo();
        }

        public void LoadPersonInfoByPhone(string Phone)
        {
            _Person = clsPerson.GetPersonInfoByPhone(Phone);
            _LoadPersonInfo();
        }

        public void LoadDefaultPersonInfo()
        {
            _Person = null;
            lblPersonID.Text = "???";
            lblName.Text = "???";
            lblN_N.Text = "???";
            lblGender.Text = "???";
            lblEmail.Text = "???";
            lblAddress.Text = "???";
            lblDateOfBirth.Text = "???";
            lblPhone.Text = "???";
            lblCountry.Text = "???";
            pbxGender.Image = Properties.Resources.Male;
            llblEditPersonalInfo.Enabled = false;
            SetDefaultImage(true);
        }

        private void _LoadPersonInfo()
        {
            lblPersonID.Text = _Person.GetPersonID().ToString();
            lblName.Text = _Person.GetFullName();
            lblN_N.Text = _Person.NationalNo;
            lblGender.Text = _Person.GetPersonGender();
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.Date.ToString("dd/MM/yyyy");
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = clsCountry.GetCountryNameByCountryID(_Person.NationalityCountryID);
            pbxGender.Image = _Person.Gender ? Properties.Resources.Male : pbxGender.Image = Properties.Resources.Female;


            if (string.IsNullOrEmpty(_Person.ImagePath))
                SetDefaultImage(_Person.Gender);

            else
                SetCustomImage(_Person.ImagePath);

            llblEditPersonalInfo.Enabled = true;
        }

        private void llblEditPersonalInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (clsGlobal.CurrentUser.PersonID == _Person.GetPersonID() || ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eEditPerson))
            {
                frmAddEditPerson AddEditPerson = new frmAddEditPerson(_Person.GetPersonID());
                AddEditPerson.ShowDialog();
                LoadPersonInfoByPersonID(_Person.GetPersonID());
            }
        }

        public int GetLoadedPersonID()
        {
            if (_Person != null)
                return _Person.GetPersonID();
            
            else
                return -1;
        }
    }

}
