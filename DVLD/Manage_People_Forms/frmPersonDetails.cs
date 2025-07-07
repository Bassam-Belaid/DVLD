﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Manage_People_Forms
{
    public partial class frmPersonDetails : Form
    {

        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfoByPersonID(PersonID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
