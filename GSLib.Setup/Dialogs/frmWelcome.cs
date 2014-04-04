using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GSLib.Setup.Dialogs
{
    public partial class frmWelcome : GSLib.Setup.Dialogs.frmSmallDialogTemplate
    {
        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        public string MessageText 
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

        /// <summary>
        /// Gets or sets the top label text.
        /// </summary>
        public string TopLabel
        {
            get
            {
                return lblLabel.Text;
            }
            set
            {
                if (value == null) throw new ArgumentException();
                lblLabel.Text = value;
            }
        }

        /// <summary>
        /// Creates a new Welcome dialog with the given message.
        /// </summary>
        /// <param name="message">Message text</param>
        public frmWelcome(string message, Setup setup)
        {
            InitializeComponent();
            if (message == null) throw new ArgumentNullException();
            MessageText = message;
            TopLabel = "Installing " + setup.Name;
        }
    }
}
