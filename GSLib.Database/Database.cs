using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSLib.Collections.Trees;

namespace GSLib.Database.GDB
{
    /// <summary>
    /// Represents a collection of Tables arranged in a tree.
    /// </summary>
    class Database
    {
        string name;
        List<TreeNode<Table>> tables;

        public Database(string name)
        {
            this.name = name;
            tables = new List<TreeNode<Table>>();
        }

        public void AddTable(string name)
        {
            TreeNode<Table> table = new TreeNode<Table>(new Table(name));
            tables.Add(table);
        }

        public bool TableExists(string name)
        {
            foreach (TreeNode<Table> t in tables)
            {
                if (t.Value.Name == name) return true;
            }
            return false;
        }

        public TreeNode<Table> GetTableNode(string name)
        {
            foreach (TreeNode<Table> t in tables)
            {
                if (t.Value.Name == name) return t;
            }
            return null;
        }
    }
}
