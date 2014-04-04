using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSLib.Collections.Trees;

namespace GSLib.Setup
{
    /// <summary>
    /// Represents a feature and the files associated with it.
    /// </summary>
    public class Feature
    {
        string name;
        List<FileData> files;
        List<TreeNode<DirectoryData>> folders;
        bool required;
        bool selected;

        /// <summary>
        /// Gets or sets whether the feature is selected. If the feature is required, this value cannot be changed.
        /// </summary>
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if (!required)
                    selected = value;

                foreach (FileData file in Files)
                {
                    file.Install = selected;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating if the feature is required.
        /// </summary>
        public bool Required
        {
            get
            {
                return required;
            }
        }

        /// <summary>
        /// Gets or sets a value with the description of the feature.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        public Feature(string name, string description, bool startSelected, bool required = false)
        {
            files = new List<FileData>();
            folders = new List<TreeNode<DirectoryData>>();

            this.required = required;
            Description = description;

            // If the feature is required, then it must be selected. Otherwise, selection is optional.
            if (required) selected = true;
            else selected = startSelected;

            this.name = name;
        }

        public void AddFile(ref FileData file)
        {
            if (file == null) throw new NullReferenceException();
            files.Add(file);
        }

        public void AddFolder(ref TreeNode<DirectoryData> folder)
        {
            folders.Add(folder);
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public FileData[] Files
        {
            get
            {
                return files.ToArray();
            }
        }

        public TreeNode<DirectoryData>[] Folders
        {
            get
            {
                return folders.ToArray();
            }
        }
    }
}
