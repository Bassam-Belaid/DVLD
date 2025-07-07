using DVLDBusinessLayer;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlUserPermission : UserControl
    {
        public ctrlUserPermission()
        {
            InitializeComponent();
        }

        private static void _ShowNoPermissionAccessMessage()
        {
            MessageBox.Show(
                "You Do Not Have Access To This Feature. Please Contact Your Admin",
                "Access Denied",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        public static bool CheckUserPermissions(clsUserPermission.Permissions Permission)
        {
            if (clsUserPermission.CheckUserPermissions(Permission))
                return true;

            else
            {
                _ShowNoPermissionAccessMessage();
                return false;
            }
        }
    }
}
