using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using GSLib.Collections.Trees;

namespace GSLib.Setup
{
    /// <summary>
    /// Contains installation events and extensions for handling DirectoryData trees.
    /// </summary>
    public static class DirectoryTree
    {
        public static int GetCount(this TreeNode<DirectoryData> tree)
        {
            int count = 0;

            foreach (TreeNode<DirectoryData> item in tree)
            {
                count = count + 1;
                count = count + item.Value.Files.Length;
                count = count + item.GetCount();
            }

            return count;
        }

        public static string GetPath(this DirectoryInfo directory)
        {
            string path = directory.FullName;

            if (!path.EndsWith("\\")) path = path + "\\";
            return path;
        }

        /// <summary>
        /// Creates the folder and returns a DirectoryInfo object for the new folder.
        /// </summary>
        /// <returns>Returns the DirectoryInfo of the new folder.</returns>
        public static DirectoryInfo MakeDirectory(this TreeNode<DirectoryData> tree, DirectoryInfo root)
        {
            if (root == null) throw new ArgumentNullException();
            if (tree == null) throw new NullReferenceException();

            return root.CreateSubdirectory(tree.GetPath(new DirectoryDataNameGetter()));
        }

        public static DirectoryInfo GetDirectoryInfo(this TreeNode<DirectoryData> tree, DirectoryInfo root)
        {
            DirectoryInfo result = new DirectoryInfo(root.GetPath() + tree.GetPath(new DirectoryDataNameGetter()));
            return result;
        }

        //public static int GetInstallCount(this TreeNode<DirectoryData> directory)
        //{
        //    int total = 0;

        //    foreach (FileData file in directory.Value.Files)
        //    {
        //        if (file.Install) total++;
        //    }

        //    foreach (TreeNode<DirectoryData> dir in directory)
        //    {
        //        total = total + dir.GetInstallCount();
        //    }

        //    return total;
        //}
    }
}
