using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.Manage_People_Forms
{
    public partial class frmAddEditPerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int ID);
        public event DataBackEventHandler DataBack;

        public frmAddEditPerson(int PersonID = -1)
        {
            InitializeComponent();

            if (PersonID != -1)
            {
                lblTitle.Text = "Update Person";
                ctrlAddEditPersonForm1.FillPersonInfo(PersonID);
            }
        }
        private void _CloseForm(object sender, EventArgs e) 
        {
            DataBack?.Invoke(this, ctrlAddEditPersonForm1.GetNewPersonID());
            this.Close();
        }
    }
}
