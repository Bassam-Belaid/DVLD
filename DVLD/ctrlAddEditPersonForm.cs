using System;
using System.Data;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Util;
using System.Collections.Generic;

namespace DVLD
{
    public partial class ctrlAddEditPersonForm : UserControl
    {

        private enum _enMode { eAddNew = 0, eUpdate = 1 };

        private _enMode _Mode;

        private static string _DefaultSelectedCountry = "Libya";
        private string _ImagePath;

        private clsPerson _Person;

        private Dictionary<string, bool> _Fields = new Dictionary<string, bool>()
        {
            { "FirstName", false },
            { "SecondName", false },
            { "ThirdName", true },
            { "LastName", false },
            { "NationalNo", false },
            { "Email", true },
            { "Address", false },
            { "Phone", false },
        };

        public ctrlAddEditPersonForm()
        {
            InitializeComponent();

            _LoadAllCountries();
            txtFirstName.Focus();
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            _Person = null;
            _ImagePath = null;
            _Mode = _enMode.eAddNew;
            _SetDefaultImage(_GetSelectedGender());
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
        public void FillPersonInfo(int PersonID)
        {
            _Person = clsPerson.GetPersonInfoByPersonID(PersonID);
            _Mode = _enMode.eUpdate;

            lblPersonID.Text = PersonID.ToString();

            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;

            if (!string.IsNullOrWhiteSpace(_Person.ThirdName))
                txtThirdName.Text = _Person.ThirdName;

            txtLastName.Text = _Person.LastName;

            txtN_N.Text = _Person.NationalNo;

            if (_Person.Gender)
                rdbMale.Checked = true;
            else
                rdbFemale.Checked = true;

            if (!string.IsNullOrWhiteSpace(_Person.Email))
                txtEmail.Text = _Person.Email;

            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtPhone.Text = _Person.Phone;
            cbxCountries.SelectedItem = clsCountry.GetCountryNameByCountryID(_Person.NationalityCountryID);

            if (string.IsNullOrEmpty(_Person.ImagePath))
                _SetDefaultImage(_Person.Gender);

            else
            {
                _SetCustomImage(_Person.ImagePath);
                llblRemoveImageLink.Visible = true;
            }
        }
        private void _LoadAllCountries()
        {
            DataTable dataTable = clsCountry.GetAllCountries();
            foreach (DataRow row in dataTable.Rows)
            {
                cbxCountries.Items.Add(row["CountryName"].ToString());
            }

            cbxCountries.SelectedItem = _DefaultSelectedCountry;
        }
        private bool _GetSelectedGender()
        {
            return rdbMale.Checked;
        }
        private void OnGenderSelection(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_ImagePath))
                _SetDefaultImage(_GetSelectedGender());
        }
        private bool _IsValidName(TextBox NameField)
        {
            if (NameField != txtThirdName && string.IsNullOrWhiteSpace(NameField.Text))
            {
                errorProvider1.SetError(NameField, "This Field Should Have A Value!");
                return false;
            }
            else if (!clsValidation.IsValidName(NameField.Text))
            {
                errorProvider1.SetError(NameField, "Please Enter A Valid Name.");
                return false;
            }

            errorProvider1.SetError(NameField, "");
            return true;
        }
        private bool _IsValidNationalNumber()
        {
            if (string.IsNullOrWhiteSpace(txtN_N.Text))
            {
                errorProvider1.SetError(txtN_N, "This Field Should Have A Value!");
                return false;
            }

            else if (clsPerson.IsPersonExistsByNationalNo(txtN_N.Text))
            {

                if (_Mode == _enMode.eAddNew || (_Mode == _enMode.eUpdate && _Person.NationalNo != txtN_N.Text))
                {
                    errorProvider1.SetError(txtN_N, "This National Number Is Already Taken.");
                    return false;
                }
            }

            errorProvider1.SetError(txtN_N, "");
            return true;
        }
        private bool _IsValidEmail()
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !clsValidation.IsValidEmail(txtEmail.Text))
            {

                errorProvider1.SetError(txtEmail, "Please Enter A Valid Email.");
                return false;
            }
            else if (clsPerson.IsPersonExistsByEmail(txtEmail.Text))
            {
                if (_Mode == _enMode.eAddNew || (_Mode == _enMode.eUpdate && _Person.Email != txtEmail.Text))
                {
                    errorProvider1.SetError(txtEmail, "This Email Is Already Taken.");
                    return false;
                }
            }

            errorProvider1.SetError(txtEmail, "");
            return true;
        }
        private bool _IsValidPhone()
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                errorProvider1.SetError(txtPhone, "This Field Should Have A Value!");
                return false;
            }
            else if (!clsValidation.IsValidPhone(txtPhone.Text))
            {
                errorProvider1.SetError(txtPhone, "Please Enter A Valid Phone.");
                return false;
            }
            else if (clsPerson.IsPersonExistsByPhone(txtPhone.Text))
            {
                if (_Mode == _enMode.eAddNew || (_Mode == _enMode.eUpdate && _Person.Phone != txtPhone.Text))
                {
                    errorProvider1.SetError(txtPhone, "This Phone Is Already Taken.");
                    return false;
                }
            }

            errorProvider1.SetError(txtPhone, "");
            return true;
        }
        private bool _IsValidAddress()
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                errorProvider1.SetError(txtAddress, "This Field Should Have A Value!");
                return false;
            }

            errorProvider1.SetError(txtAddress, "");
            return true;
        }
        private bool _IsAllFieldsAreValid()
        {
            _Fields["FirstName"] = _IsValidName(txtFirstName);
            _Fields["SecondName"] = _IsValidName(txtSecondName);
            _Fields["ThirdName"] = _IsValidName(txtThirdName);
            _Fields["LastName"] = _IsValidName(txtLastName);

            _Fields["NationalNo"] = _IsValidNationalNumber();

            _Fields["Email"] = _IsValidEmail();

            _Fields["Address"] = _IsValidAddress();

            _Fields["Phone"] = _IsValidPhone();

            foreach (bool FieldValue in _Fields.Values) 
            {
                if (!FieldValue)
                    return false;
            }

            return true;
        }
        private bool _Save()
        {
            if (_Mode == _enMode.eAddNew)
                _Person = new clsPerson();

            if (!string.IsNullOrWhiteSpace(txtFirstName.Text))
                _Person.FirstName = txtFirstName.Text;


            if (!string.IsNullOrWhiteSpace(txtSecondName.Text))
                _Person.SecondName = txtSecondName.Text;

            if (!string.IsNullOrWhiteSpace(txtThirdName.Text))
                _Person.ThirdName = txtThirdName.Text;
            else
                _Person.ThirdName = null;

            if (!string.IsNullOrWhiteSpace(txtLastName.Text))
                _Person.LastName = txtLastName.Text;

            if (!string.IsNullOrWhiteSpace(txtN_N.Text))
                _Person.NationalNo = txtN_N.Text;

            _Person.Gender = _GetSelectedGender();

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                _Person.Email = txtEmail.Text;
            else
                _Person.Email = null;

            if (!string.IsNullOrWhiteSpace(txtAddress.Text))
                _Person.Address = txtAddress.Text;

            _Person.DateOfBirth = DateTime.Parse(dtpDateOfBirth.Text);

            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                _Person.Phone = txtPhone.Text;

            _Person.NationalityCountryID = clsCountry.GetCountryIDByCountryName(cbxCountries.SelectedItem.ToString());

            if (!string.IsNullOrEmpty(_ImagePath))
                _Person.ImagePath = _ImagePath;

            return _Person.Save();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string Message = (_Mode == _enMode.eAddNew) ? "The Person Has Been Added Successfully" : "The Person Has Been Updated Successfully";
            string Title = (_Mode == _enMode.eAddNew) ? "Confirm Add" : "Confirm Update";

            DialogResult Result = MessageBox.Show("Are You Sure About All The Information ?",
                            Title,
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                if (_IsAllFieldsAreValid() && _Save())
                {
                    if (_Mode == _enMode.eAddNew)
                        lblPersonID.Text = _Person.GetPersonID().ToString();

                    MessageBox.Show(Message,
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

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
        private void llblSetImageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _ImagePath = openFileDialog1.FileName;
                _SetCustomImage(_ImagePath);
            }
            llblRemoveImageLink.Visible = true;
        }
        private void llblRemoveImageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_Mode == _enMode.eUpdate)
                _Person.ImagePath = null;

            _ImagePath = null;
            _SetDefaultImage(_GetSelectedGender());
            llblRemoveImageLink.Visible = false;
        }
        public int GetNewPersonID() 
        {
            if(_Person != null)
                return _Person.GetPersonID();

            else return -1;
        }
    }
}