using DVLD.Manage_People_Forms;
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
using Util;

namespace DVLD.Manage_Users_Forms
{
    public partial class frmManageUsers : Form
    {
        private enum _enFilter
        {
            eNone = 0,
            ePersonID = 1,
            eUserID = 2,
            eFullName = 3,
            eUserName = 4,
            eIsActive = 5
        }
        private enum _enActivityStatusFilter
        {
            eAll = 0,
            eActive = 1,
            eNotActive = 2,
        }

        private _enFilter _Filter;

        public frmManageUsers()
        {
            InitializeComponent();
            cbxManageUsersFilter.SelectedItem = "None";
            _LoadUsersList(clsUser.GetAllUsers());
        }

        private void _LoadUsersList(DataTable dataTable)
        {

            dataGridView1.DataSource = dataTable;
            lblNumberOfRecords.Text = (dataTable == null) ? "0" : (" " + dataTable.Rows.Count.ToString());

            if (dataTable == null)
                return;

            dataTable.Columns[0].ColumnName = "User ID";
            dataTable.Columns[1].ColumnName = "Person ID";
            dataTable.Columns[2].ColumnName = "Full Name";
            dataTable.Columns[3].ColumnName = "UserName";
            dataTable.Columns[4].ColumnName = "Is Active";
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
                case "User ID":
                    return _enFilter.eUserID;
                case "Full Name":
                    return _enFilter.eFullName;
                case "UserName":
                    return _enFilter.eUserName;
                case "Is Active":
                    return _enFilter.eIsActive;
                default:
                    return _enFilter.eNone;
            }
        }

        private _enActivityStatusFilter _GetActivityStatusFilterType(string ActivityStatusFilter)
        {

            switch (ActivityStatusFilter)
            {
                case "Active":
                    return _enActivityStatusFilter.eActive;
                case "Not Active":
                    return _enActivityStatusFilter.eNotActive;
                default:
                    return _enActivityStatusFilter.eAll;
            }
        }

        private void _FilterByPersonID()
        {
            if (int.TryParse(txtSearchInput.Text, out int PersonID))
            {
                _LoadUsersList(clsUser.GetUsersFilteredByPersonID(PersonID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid Person ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FilterByUserID()
        {
            if (int.TryParse(txtSearchInput.Text, out int UserID))
            {
                _LoadUsersList(clsUser.GetUsersFilteredByUserID(UserID));
            }

            else
            {
                txtSearchInput.Text = "";
                MessageBox.Show("Please Enter A Valid User ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void _FilterByFullName()
        {
            _LoadUsersList(clsUser.GetUsersFilteredByFullName(txtSearchInput.Text));
        }

        private void _FilterByUserName()
        {
            _LoadUsersList(clsUser.GetUsersFilteredByUserName(txtSearchInput.Text));
        }

        private void _FilterByActivityStatus(_enActivityStatusFilter ActivityStatusFilter)
        {
            switch (ActivityStatusFilter)
            {
                case _enActivityStatusFilter.eActive:
                    _LoadUsersList(clsUser.GetUsersFilteredByActivationStatus(true));
                    break;

                case _enActivityStatusFilter.eNotActive:
                    _LoadUsersList(clsUser.GetUsersFilteredByActivationStatus(false));
                    break;

                default:
                    _LoadUsersList(clsUser.GetAllUsers());
                    break;
            }
        }

        private void _FilterUsersList(_enFilter Filter)
        {
            if (string.IsNullOrEmpty(txtSearchInput.Text))
            {
                _LoadUsersList(clsUser.GetAllUsers());
                return;
            }
            switch (Filter)
            {
                case _enFilter.ePersonID:
                    _FilterByPersonID();
                    break;

                case _enFilter.eUserID:
                    _FilterByUserID();
                    break;

                case _enFilter.eFullName:
                    _FilterByFullName();
                    break;

                case _enFilter.eUserName:
                    _FilterByUserName();
                    break;
            }
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterUsersList(_Filter);
        }

        private void cbxManageUsersFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Filter = _GetFilterType(cbxManageUsersFilter.SelectedItem.ToString());
            txtSearchInput.Visible = (_Filter != _enFilter.eNone && _Filter != _enFilter.eIsActive);
            cbxActivityStatus.Visible = (_Filter == _enFilter.eIsActive);

            if(_Filter == _enFilter.eNone)
                _LoadUsersList(clsUser.GetAllUsers());

            if (cbxActivityStatus.Visible)
                cbxActivityStatus.SelectedItem = "All";
        }

        private void cbxActivityStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            _FilterByActivityStatus(_GetActivityStatusFilterType(cbxActivityStatus.SelectedItem.ToString()));
        }

        private void _AddNewUser()
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eAddNewUser))
            {
                frmAddEditUser AddEditUser = new frmAddEditUser();
                AddEditUser.ShowDialog();
                _LoadUsersList(clsUser.GetAllUsers());
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            _AddNewUser();
        }

        private void showDetilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eShowUserDetails))
            {
                int UserID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                frmUserDetails UserDetails = new frmUserDetails(UserID);
                UserDetails.ShowDialog();
                _LoadUsersList(clsUser.GetAllUsers());
            }
        }
        
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _AddNewUser();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            if (clsGlobal.CurrentUser.GetUserID() == UserID || ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eEditUser))
            {

                frmAddEditUser AddEditUser = new frmAddEditUser(UserID);
                AddEditUser.ShowDialog();
                _LoadUsersList(clsUser.GetAllUsers());
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eDeleteUser))
            {
                int UserID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                DialogResult Result = MessageBox.Show("Are You Sure You Want To Delete User [" + UserID.ToString() + "]",
                                "Confirm Delete",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                if (Result == DialogResult.Yes)
                {
                    if (clsUser.DeleteUser(UserID))
                    {
                        MessageBox.Show("The User Has Been Deleted Successfully",
                                            "Success",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed To Delete The User [" + UserID.ToString() + "] Because They Have Linked Information.",
                                        "Failed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }

                _LoadUsersList(clsUser.GetAllUsers());
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            if (clsGlobal.CurrentUser.GetUserID() == UserID || ctrlUserPermission.CheckUserPermissions(clsUserPermission.enPermissions.eChangeUserPassword))
            {
                string UserPassword = clsUser.GetUserPasswordByUserID(UserID);

                frmChangePassword ChangePassword = new frmChangePassword(UserID, UserPassword);
                ChangePassword.ShowDialog();
            }
        }

    }
}
