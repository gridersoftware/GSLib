using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSLib.Collections.Trees;
using System.IO;

namespace GSLib.Setup
{
    public static class FeatureTree
    {
        public static EventHandler<InstallingEventArgs> InstallingEvent;
        public static EventHandler<InstalledEventArgs> InstalledEvent;

        public static void Install(this TreeNode<Feature> tree, DirectoryInfo root)
        {
            Feature feature = tree.Value;

            if (feature.Selected)
            {
                foreach (TreeNode<DirectoryData> dir in tree.Value.Folders)
                {
                    DirectoryInfo d = dir.GetDirectoryInfo(root);
                    InstallingEvent(null, new InstallingEventArgs(d));
                    try
                    {
                        d = dir.MakeDirectory(root);
                        InstalledEvent(null, new InstalledEventArgs(d, true, null));
                    }
                    catch (Exception ex)
                    {
                        InstalledEvent(null, new InstalledEventArgs(d, false, ex));
                    }

                    foreach (FileData file in dir.Value.Files)
                    {
                        try
                        {
                            if (file.Install)
                            {
                                InstallingEvent(null, new InstallingEventArgs(file));
                                file.InstallFile(d);
                                InstalledEvent(null, new InstalledEventArgs(file, true, null));
                            }
                        }
                        catch (Exception ex)
                        {
                            InstalledEvent(null, new InstalledEventArgs(file, false, ex));
                        }
                    }
                }

                foreach (TreeNode<Feature> f in tree)
                {
                    f.Install(root);
                }
            }
        }

        public static int GetInstallCount(this TreeNode<Feature> tree)
        {
            int count = 0;

            if (tree.Value.Selected == true)
            {
                foreach (TreeNode<DirectoryData> dir in tree.Value.Folders)
                {
                    count++;
                    foreach (FileData file in dir.Value.Files)
                    {
                        if (file.Install) count++;
                    }
                }
                foreach (TreeNode<Feature> f in tree)
                {
                    count = count + f.GetInstallCount();
                }
            }

            return count;
        }

        public static long GetDiskSpaceRequirement(this TreeNode<Feature> tree, FileData.FileSizeUnit unit)
        {
            long count = 0;

            if (tree.Value.Selected == true)
            {
                foreach (TreeNode<DirectoryData> dir in tree)
                {
                    foreach (FileData file in dir.Value.Files)
                    {
                        if (file.Install) count = count + file.GetFileSize(unit);
                    }
                }
                foreach (TreeNode<Feature> f in tree)
                {
                    count = count + f.GetDiskSpaceRequirement(unit);
                }
            }

            return count;
        }
    }
}
