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
    public partial class ctrlListContent : UserControl
    {
        public Label Content;
        public delegate void CloseEventHandler();
        public event CloseEventHandler CloseHandlerRequest;

        public ctrlListContent()
        {
            InitializeComponent();
            Content = lblContent;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            CloseHandlerRequest?.Invoke();
        }
    }
}
