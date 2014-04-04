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
    public partial class frmInstallPath : GSLib.Setup.Dialogs.frmSmallDialogTemplate
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

        public string Message
        {
            get
            {
                return lblMessage.Text;
            }
            set
            {
                if (value == null) throw new ArgumentNullException();
                lblMessage.Text = value;
            }
        }

        public string InstallPath
        {
            get
            {
                return txtPath.Text;
            }
        }

        public DirectoryInfo InstallDirectory
        {
            get
            {
                return new DirectoryInfo(InstallPath);
            }
        }

        public frmInstallPath(Setup setup, string defaultPath, string topLabel = "Installation Path", string message = "Choose a path to install the program.")
        {
            InitializeComponent();

            if ((topLabel == null) | (message == null) | (defaultPath == null)) throw new ArgumentNullException();
            if (!Directory.Exists(defaultPath)) throw new DirectoryNotFoundException();

            lblLabel.Text = topLabel;
            lblMessage.Text = message;
            txtPath.Text = defaultPath;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgFolderBrowser.ShowNewFolderButton = true;
            dlgFolderBrowser.SelectedPath = txtPath.Text;
            DialogResult result = dlgFolderBrowser.ShowDialog();

            // If the user clicks OK, set the new value
            if (result == System.Windows.Forms.DialogResult.OK)
                txtPath.Text = dlgFolderBrowser.SelectedPath;
        }
    }
}
