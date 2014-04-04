namespace GSLib.Setup.Dialogs
{
    partial class frmFeatures
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tvFeatures = new GSLib.Controls.TreeView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(25, 56);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(49, 13);
            this.lblMessage.TabIndex = 14;
            this.lblMessage.Text = "message";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblLabel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 43);
            this.panel1.TabIndex = 13;
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel.Location = new System.Drawing.Point(12, 9);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(163, 25);
            this.lblLabel.TabIndex = 0;
            this.lblLabel.Text = "Select Features";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Location = new System.Drawing.Point(277, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 227);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Description";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(11, 19);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(216, 194);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "description";
            // 
            // tvFeatures
            // 
            this.tvFeatures.CheckBoxes = true;
            this.tvFeatures.Location = new System.Drawing.Point(12, 72);
            this.tvFeatures.Name = "tvFeatures";
            this.tvFeatures.Size = new System.Drawing.Size(259, 227);
            this.tvFeatures.TabIndex = 17;
            this.tvFeatures.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFeatures_BeforeCheck);
            this.tvFeatures.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvFeatures_AfterCheck);
            this.tvFeatures.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFeatures_AfterSelect);
            // 
            // frmFeatures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(523, 349);
            this.Controls.Add(this.tvFeatures);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.panel1);
            this.Name = "frmFeatures";
            this.Load += new System.EventHandler(this.frmFeatures_Load);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblMessage, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.tvFeatures, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblLabel;
        private Controls.TreeView tvFeatures;
    }
}
