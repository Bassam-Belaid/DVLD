using System;
using System.Windows.Forms;
using Util;

namespace DVLD
{
    public partial class ctrlPasswordGenerator : UserControl
    {
        public static string Password;

        public ctrlPasswordGenerator()
        {
            InitializeComponent();
            nudPasswordLength.Minimum = clsPasswordGenerator.GetMinimumPasswordLength();
            nudPasswordLength.Maximum = clsPasswordGenerator.GetMaximumPasswordLength();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            clsPasswordGenerator.SetPasswordLength(int.Parse(nudPasswordLength.Value.ToString()));
            lblPassword.Text = clsPasswordGenerator.GeneratePassword();
            Password = lblPassword.Text;
        }
    }
}
