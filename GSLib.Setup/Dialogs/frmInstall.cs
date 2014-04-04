using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GSLib.Setup.Dialogs
{
    public partial class frmInstall : GSLib.Setup.Dialogs.frmSmallDialogTemplate
    {
        private Setup setup;
        private DirectoryInfo dir;

        /// <summary>
        /// Creates a new instance of frmInstall.
        /// </summary>
        /// <param name="setup">Setup configuration instance to use.</param>
        /// <param name="message">Text to display.</param>
        /// <param name="installDirectory">Directory to install the files to.</param>
        public frmInstall(Setup setup, string message, DirectoryInfo installDirectory)
        {
            InitializeComponent();

            lblMessage.Text = message;
            pbProgress.Maximum = setup.DirectoryTree.GetCount();

            FeatureTree.InstallingEvent += new EventHandler<InstallingEventArgs>(Installing);
            FeatureTree.InstalledEvent += new EventHandler<InstalledEventArgs>(Installed);

            this.setup = setup;
            dir = installDirectory;

            Text = setup.Name + " Setup";
        }

        private void frmInstall_Load(object sender, EventArgs e)
        {
            
        }

        private void Installing(object sender, InstallingEventArgs e)
        {
            if (e.Directory != null)
            {
                lblStatus.Text = "Creating directory: " + e.Directory.FullName;
            }
            else
            {
                lblStatus.Text = "Copying file: " + e.File.Filename;
            }
        }

        private void Installed(object sender, InstalledEventArgs e)
        {
            if (e.Succeeded)
            {
                pbProgress.Value++;
                if (pbProgress.Value == pbProgress.Maximum)
                {
                    btnNext.Enabled = true;
                    btnCancel.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Install failed: " + e.Ex.Message);
            }
        }

        private void frmInstall_Shown(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnBack.Enabled = false;
            pbProgress.Maximum = setup.FeatureTree.GetInstallCount();
            setup.FeatureTree.Install(dir);
        }
    }
}
