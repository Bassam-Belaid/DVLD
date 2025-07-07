using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Manage_Users_Forms
{
    public partial class frmUserDetails : Form
    {
        public frmUserDetails(int UserID)
        {
            InitializeComponent();

            ctrlUserCard1.LoadUserInfoByUserID(UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
