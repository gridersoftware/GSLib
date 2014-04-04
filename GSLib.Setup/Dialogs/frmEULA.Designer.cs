namespace GSLib.Setup.Dialogs
{
    partial class frmEULA
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLabel = new System.Windows.Forms.Label();
            this.txtEULA = new System.Windows.Forms.TextBox();
            this.chkAgree = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(14, 55);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(402, 13);
            this.lblMessage.TabIndex = 14;
            this.lblMessage.Text = "You must agree to the following End-User License Agreement to install this softwa" +
    "re.";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblLabel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 43);
            this.panel1.TabIndex = 13;
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel.Location = new System.Drawing.Point(12, 9);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(97, 25);
            this.lblLabel.TabIndex = 0;
            this.lblLabel.Text = "Installing";
            // 
            // txtEULA
            // 
            this.txtEULA.BackColor = System.Drawing.SystemColors.Window;
            this.txtEULA.Location = new System.Drawing.Point(24, 88);
            this.txtEULA.Multiline = true;
            this.txtEULA.Name = "txtEULA";
            this.txtEULA.ReadOnly = true;
            this.txtEULA.Size = new System.Drawing.Size(473, 179);
            this.txtEULA.TabIndex = 15;
            // 
            // chkAgree
            // 
            this.chkAgree.AutoSize = true;
            this.chkAgree.Location = new System.Drawing.Point(40, 273);
            this.chkAgree.Name = "chkAgree";
            this.chkAgree.Size = new System.Drawing.Size(324, 17);
            this.chkAgree.TabIndex = 16;
            this.chkAgree.Text = "I agree to the terms of the End-User License Agreement above.";
            this.chkAgree.UseVisualStyleBackColor = true;
            this.chkAgree.CheckedChanged += new System.EventHandler(this.chkAgree_CheckedChanged);
            // 
            // frmEULA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(523, 349);
            this.Controls.Add(this.chkAgree);
            this.Controls.Add(this.txtEULA);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.panel1);
            this.Name = "frmEULA";
            this.Load += new System.EventHandler(this.frmEULA_Load);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblMessage, 0);
            this.Controls.SetChildIndex(this.txtEULA, 0);
            this.Controls.SetChildIndex(this.chkAgree, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtEULA;
        private System.Windows.Forms.CheckBox chkAgree;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.Label lblMessage;
    }
}
