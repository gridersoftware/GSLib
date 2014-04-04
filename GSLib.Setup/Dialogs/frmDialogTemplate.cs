using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSLib.Setup.Dialogs
{
    public partial class frmDialogTemplate : Form
    {
        public frmDialogTemplate()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure you want to cancel setup?", "Cancel Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

    }
}
