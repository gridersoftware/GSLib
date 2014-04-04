using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GSLib.Collections.Trees;

namespace GSLib.Setup.Dialogs
{
    public partial class frmFeatures : GSLib.Setup.Dialogs.frmDialogTemplate
    {
        bool init = false;

        public frmFeatures(Setup setup)
        {
            InitializeComponent();

            foreach (TreeNode<Feature> t in setup.FeatureTree.Children)
            {
                TreeNode node = new TreeNode(t.Value.Name);
                node.Checked = t.Value.Selected;
                node.Tag = t.Value;
                tvFeatures.Nodes.Add(node);
                AddNode(node, t);
            }
        }

        public void AddNode(TreeNode tvNode, TreeNode<Feature> feature)
        {
            foreach (TreeNode<Feature> t in feature.Children)
            {
                TreeNode node = new TreeNode(t.Value.Name);
                node.Checked = t.Value.Selected;
                node.Tag = t.Value;
                tvNode.Nodes.Add(node);
                AddNode(node, t);   
            }
        }

        private void frmFeatures_Load(object sender, EventArgs e)
        {
            if (tvFeatures.Nodes.Count > 0)
                tvFeatures.Select();

            init = true;
        }

        private void tvFeatures_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Feature f = (Feature)tvFeatures.SelectedNode.Tag;
            lblDescription.Text = f.Description;
        }

        private void tvFeatures_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            Feature f = (Feature)e.Node.Tag;
            if (init & f.Required) e.Cancel = true;
        }

        private void tvFeatures_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Feature f = (Feature)e.Node.Tag;
            f.Selected = e.Node.Checked;
        }

        private void tvFeatures_DoubleClick(object sender, EventArgs e)
        {
            Feature f = (Feature)tvFeatures.SelectedNode.Tag;
            if (f.Required) tvFeatures.SelectedNode.Checked = true;
        }
    }
}
