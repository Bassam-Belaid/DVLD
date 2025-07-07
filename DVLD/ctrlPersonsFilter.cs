using System;
using DVLD.Manage_People_Forms;
using DVLDBusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlPersonsFilter : UserControl
    {
        private enum _enFilter
        {
            eNationalNo = 0,
            ePersonID = 1,
            ePhone = 2,
            eEmail = 3
        }

        private _enFilter _Filter;

        public ctrlPersonsFilter()
        {
            InitializeComponent();
            cbxPersonsFilter.SelectedItem = "National No";
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (clsUserPermission.CheckUserPermissions(clsUserPermission.Permissions.eAddNewPerson))
            {
                frmAddEditPerson AddEditPerson = new frmAddEditPerson();
                AddEditPerson.DataBack += ShowNewPersonInfo;
                AddEditPerson.ShowDialog();
            }
        }

        private _enFilter _GetFilterType(string Filter)
        {
            txtSearchInput.Text = "";

            switch (Filter)
            {
                case "Person ID":
                    return _enFilter.ePersonID;
                case "Phone":
                    return _enFilter.ePhone;
                case "Email":
                    return _enFilter.eEmail;
                default: 
                    return _enFilter.eNationalNo;
            }
        }

        private void _FindByPersonID()
        {
            if (int.TryParse(txtSearchInput.Text, out int PersonID))
            {

                if (clsPerson.IsPersonExistsByPersonID(PersonID))
                {
                    ctrlPersonCard1.LoadPersonInfoByPersonID(PersonID);
                }
                else
                {
                    MessageBox.Show("No Person Exists With The Provided Person ID [" + PersonID.ToString() + "] Please Check And Try Again.",
                                    "Invalid Input",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    ctrlPersonCard1.LoadDefaultPersonInfo();
                }
            }

            else
            {
                MessageBox.Show("Please Enter A Valid Person ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FindByNationalNo()
        {

           if(clsPerson.IsPersonExistsByNationalNo(txtSearchInput.Text))
            {
                ctrlPersonCard1.LoadPersonInfoByNationalNo(txtSearchInput.Text);
            }
            else
            {
                MessageBox.Show("No Person Exists With The Provided National Number [" + txtSearchInput.Text + "] Please Check And Try Again.",
                                "Invalid Input",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                ctrlPersonCard1.LoadDefaultPersonInfo();
            }
        }

        private void _FindByEmail()
        {

            if (clsPerson.IsPersonExistsByEmail(txtSearchInput.Text))
            {
                ctrlPersonCard1.LoadPersonInfoByEmail(txtSearchInput.Text);
            }
            else
            {
                MessageBox.Show("No Person Exists With The Provided Email [" + txtSearchInput.Text + "] Please Check And Try Again.",
                                "Invalid Input",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                ctrlPersonCard1.LoadDefaultPersonInfo();
            }

        }

        private void _FindByPhone()
        {
            if (clsPerson.IsPersonExistsByPhone(txtSearchInput.Text))
            {
                ctrlPersonCard1.LoadPersonInfoByPhone(txtSearchInput.Text);
            }
            else
            {
                MessageBox.Show("No Person Exists With The Provided Phone [" + txtSearchInput.Text + "] Please Check And Try Again.",
                                "Invalid Input",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                ctrlPersonCard1.LoadDefaultPersonInfo();
            }
        }

        private void _FindPerson(_enFilter Filter)
        {
            switch (Filter)
            {
                case _enFilter.eNationalNo:
                    _FindByNationalNo();
                    break;

                case _enFilter.ePersonID:
                    _FindByPersonID();
                    break;

                case _enFilter.ePhone:
                    _FindByPhone();
                    break;

                case _enFilter.eEmail:
                    _FindByEmail();
                    break;
            }
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchInput.Text))
            {
                MessageBox.Show("Please Enter a Search Term.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindPerson(_Filter);
        }

        private void cbxPersonsFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            _Filter = _GetFilterType(cbxPersonsFilter.SelectedItem.ToString());
        }
        
        public int GetLoadedPersonID()
        {
            return ctrlPersonCard1.GetLoadedPersonID();
        }

        public void LoadPersonInfo(int PersonID)
        {
            cbxPersonsFilter.SelectedItem = "Person ID";
            txtSearchInput.Text = PersonID.ToString();
            gbxPersonsFilter.Enabled = false;

            ctrlPersonCard1.LoadPersonInfoByPersonID(PersonID);
        }

        public void ShowNewPersonInfo(object sender, int PersonID)
        {
            if(PersonID != -1)
            ctrlPersonCard1.LoadPersonInfoByPersonID(PersonID);
        }
    }
}
