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
    public partial class ctrlLicensesFilter : UserControl
    {
        public delegate void EventHandler(object sender);
        public event EventHandler Handler;

        public ctrlLicensesFilter()
        {
            InitializeComponent();
        }

        public ctrlLicenseCard GetLicenseCard() 
        {
            return ctrlLicenseCard2;
        }

        private void btnFindLicense_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearchInput.Text, out int LocalLicenseID))
            {
                ctrlLicenseCard2.LoadLicenseDetailsByLicenseID(LocalLicenseID);
                Handler.Invoke(this);
            }

            else
            {
                MessageBox.Show("Please Enter A Valid License ID (Numeric Value).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DisableLicenseFilter() 
        {
            gbxLicenseFilter.Enabled = false;
        }
    }
}
