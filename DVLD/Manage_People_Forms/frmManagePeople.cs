using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD.Manage_People_Forms
{
    public partial class frmManagePeople : Form
    {

        private enum _enFilter
        {
            
            eNone = 0,        
            ePersonID = 1,  
            eNationalNo = 2, 
            eFirstName = 3,  
            eSecondName = 4, 
            eThirdName = 5,  
            eLastName = 6,   
            eNationality = 7,
            eGender = 8,     
            ePhone = 9,      
            eEmail = 10
                
        }

        private _enFilter _Filter;

        public frmManagePeople()
        {
            InitializeComponent();
            cbxManagePeopleFilter.SelectedItem = "None";
            _LoadPeopleList(clsPerson.GetAllPeople());
        }

        private void _LoadPeopleList(DataTable dataTable)
        {

            dataGridView1.DataSource = dataTable;
            lblNumberOfRecords.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "Person ID";
            dataTable.Columns[1].ColumnName = "National No";
            dataTable.Columns[2].ColumnName = "First Name";
            dataTable.Columns[3].ColumnName = "Second Name";
            dataTable.Columns[4].ColumnName = "Third Name";
            dataTable.Columns[5].ColumnName = "Last Name";
            dataTable.Columns[7].ColumnName = "Date Of Birth";
            dataTable.Columns[8].ColumnName = "Nationality";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private _enFilter _GetFilterType(string Filter)
        {

            txtSearchInput.Text = "";

            switch (Filter)
            {

                case "Person ID":
                    return _enFilter.ePersonID;
                case "National No":
                    return _enFilter.eNationalNo;
                case "First Name":
                    return _enFilter.eFirstName;
                case "Second Name":
                    return _enFilter.eSecondName;
                case "Third Name":
                    return _enFilter.eThirdName;
                case "Last Name":
                    return _enFilter.eLastName;
                case "Nationality":
                    return _enFilter.eNationality;
                case "Gender":
                    return _enFilter.eGender;
                case "Phone":
                    return _enFilter.ePhone;
                case "Email":
                    return _enFilter.eEmail;
                default:
                    return _enFilter.eNone;

            }
        }

        private void _FilterByPersonID()
        {
            if (int.TryParse(txtSearchInput.Text, out int PersonID))
            {
                _LoadPeopleList(clsPerson.GetPeopleFilteredByPersonID(PersonID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid Person ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FilterByNationalNo() 
        {

            _LoadPeopleList(clsPerson.GetPeopleFilteredByNationalNo(txtSearchInput.Text));
           
        }

        private void _FilterByFirstName()
        {

            _LoadPeopleList(clsPerson.GetPeopleFilteredByFirstName(txtSearchInput.Text));

        }

        private void _FilterBySecondName()
        {

            _LoadPeopleList(clsPerson.GetPeopleFilteredBySecondName(txtSearchInput.Text));

        }

        private void _FilterByThirdName()
        {

            _LoadPeopleList(clsPerson.GetPeopleFilteredByThirdName(txtSearchInput.Text));

        }

        private void _FilterByLastName()
        {

            _LoadPeopleList(clsPerson.GetPeopleFilteredByLastName(txtSearchInput.Text));

        }

        private void _FilterByNationality()
        {

            _LoadPeopleList(clsPerson.GetPeopleFilteredByNationality(txtSearchInput.Text));

        }

        private void _FilterByGender()
        {

            _LoadPeopleList(clsPerson.GetPeopleFilteredByGender(txtSearchInput.Text));

        }

        private void _FilterByPhone()
        {

            _LoadPeopleList(clsPerson.GetPeopleFilteredByPhone(txtSearchInput.Text));

        }

        private void _FilterByEmail()
        {

            _LoadPeopleList(clsPerson.GetPeopleFilteredByEmail(txtSearchInput.Text));

        }

        private void _FilterPeopleList(_enFilter Filter)
        {

            if (string.IsNullOrEmpty(txtSearchInput.Text))
            {
                _LoadPeopleList(clsPerson.GetAllPeople());
                return;
            }

            switch (Filter)
            {
                
                case _enFilter.ePersonID:
                    _FilterByPersonID(); 
                    break;

                case _enFilter.eNationalNo:
                    _FilterByNationalNo();
                    break;

                case _enFilter.eFirstName:
                    _FilterByFirstName();
                    break;

                case _enFilter.eSecondName:
                    _FilterBySecondName();
                    break;

                case _enFilter.eThirdName:
                    _FilterByThirdName();
                    break;

                case _enFilter.eLastName:
                    _FilterByLastName();
                    break;

                case _enFilter.eNationality:
                    _FilterByNationality();
                    break;

                case _enFilter.eGender:
                    _FilterByGender();
                    break;

                case _enFilter.ePhone:
                    _FilterByPhone();
                    break;

                case _enFilter.eEmail:
                    _FilterByEmail();
                    break;

            }
            
        }

        private void cbxManagePeopleFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            _Filter = _GetFilterType(cbxManagePeopleFilter.SelectedItem.ToString());
            txtSearchInput.Visible = (_Filter != _enFilter.eNone);
        }

        private void showDetilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.Permissions.eShowPersonDetails))
            {
                int PersonID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                frmPersonDetails frmPersonDetails = new frmPersonDetails(PersonID);
                frmPersonDetails.ShowDialog();
                _LoadPeopleList(clsPerson.GetAllPeople());
            }
        }

        private void _AddNewPerson()
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.Permissions.eAddNewPerson))
            {
                frmAddEditPerson AddEditPerson = new frmAddEditPerson();
                AddEditPerson.ShowDialog();
                _LoadPeopleList(clsPerson.GetAllPeople());
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
          _AddNewPerson();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _AddNewPerson();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            if (clsGlobal.CurrentUser.PersonID == PersonID || ctrlUserPermission.CheckUserPermissions(clsUserPermission.Permissions.eEditPerson))
            {
                frmAddEditPerson AddEditPerson = new frmAddEditPerson(PersonID);
                AddEditPerson.ShowDialog();
                _LoadPeopleList(clsPerson.GetAllPeople());
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.Permissions.eDeletePerson))
            {
                int PersonID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                DialogResult Result = MessageBox.Show("Are You Sure You Want To Delete Person [" + PersonID.ToString() + "]",
                                "Confirm Delete",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                if (Result == DialogResult.Yes)
                {
                    if (clsPerson.DeletePerson(PersonID))
                    {
                        MessageBox.Show("The Person Has Been Deleted Successfully",
                                            "Success",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed To Delete The Person [" + PersonID.ToString() + "] Because They Have Linked Information.",
                                        "Failed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }

                _LoadPeopleList(clsPerson.GetAllPeople());
            }
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterPeopleList(_Filter);
        }
    }
}
