using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSLib.Forms
{
    public partial class InputBox : Form
    {
        public string Response
        {
            get
            {
                return txtInput.Text;
            }
        }

        public InputBox()
        {
            InitializeComponent();
        }

        public InputBox(string prompt, string title = "", string defaultResponse = "", char passwordChar = '\0', bool useSystemPasswordChar = false)
        {
            InitializeComponent();
            if ((prompt == null) | (title == null) | (defaultResponse == null)) throw new ArgumentNullException();

            lblPrompt.Text = prompt;
            txtInput.Text = defaultResponse;

            if (title == "")
                this.Text = Application.ProductName;
            else
                this.Text = title;

            this.CenterToScreen();
            this.TopMost = true;

            txtInput.PasswordChar = passwordChar;
            txtInput.UseSystemPasswordChar = useSystemPasswordChar;
        }

        public InputBox(string prompt, string title = "", string defaultResponse = "", int xpos = -1, int ypos = -1, char passwordChar = '\0', bool useSystemPasswordChar = false)
        {
            InitializeComponent();
            if ((prompt == null) | (title == null) | (defaultResponse == null)) throw new ArgumentNullException();

            lblPrompt.Text = prompt;
            txtInput.Text = defaultResponse;

            if (title == "")
                this.Text = Application.ProductName;
            else
                this.Text = title;
                
            this.CenterToScreen();
            if (xpos >= 0)
                this.Left = xpos;

            if (ypos >= 0)
                this.Top = ypos;

            txtInput.PasswordChar = passwordChar;
            txtInput.UseSystemPasswordChar = useSystemPasswordChar;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {

        }

        private void InputBox_Shown(object sender, EventArgs e)
        {
            txtInput.Top = lblPrompt.Bottom + 3;
            btnOk.Top = txtInput.Bottom + 5;
            btnCancel.Top = btnOk.Top;
            this.Height = lblPrompt.Top + btnOk.Bottom + 20;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }


    }
}
