using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GSLib.Setup.Dialogs
{
    public partial class frmEULA : GSLib.Setup.Dialogs.frmDialogTemplate
    {
        public string TopLabel
        {
            get
            {
                return lblLabel.Text;
            }
            set
            {
                if (value == null) throw new ArgumentNullException();
                lblLabel.Text = value;
            }
        }

        public string MessageText
        {
            get
            {
                return lblMessage.Text;
            }
            set
            {
                if (value == null) throw new ArgumentNullException();
                lblLabel.Text = value;
            }
        }

        public frmEULA(Setup setup)
        {
            InitializeComponent();

            txtEULA.Text = setup.EULA;
            TopLabel = "End-User License Agreement";
        }

        private void frmEULA_Load(object sender, EventArgs e)
        {
            chkAgree.Checked = false;
            btnNext.Enabled = false;
        }

        private void chkAgree_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = chkAgree.Checked;
        }
    }
}
