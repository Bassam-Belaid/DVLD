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

namespace DVLD
{
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _User;

        public ctrlUserCard()
        {
            InitializeComponent();

            _User = null;
        }

        public void LoadUserInfoByUserID(int UserID)
        {
            _User = clsUser.GetUserInfoByUserID(UserID);
            _LoadUserInfo();
        }

        private void _LoadUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfoByPersonID(_User.PersonID);

            lblUserID.Text = _User.GetUserID().ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = (_User.IsActive) ? "Yes" : "No";
        }
    }
}
