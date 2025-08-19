using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Manage_Applications_Forms.Manage_Driver_License_Services_Forms
{
    public partial class frmLicenseDetails : Form
    {
        public frmLicenseDetails(int LocalDrivingApplicationID)
        {
            InitializeComponent();

            ctrlLicenseCard1.LoadLicenseDetailsByLocalDrivingLicenseApplicationID(LocalDrivingApplicationID);
        }
    }
}
