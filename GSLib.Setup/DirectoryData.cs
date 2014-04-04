using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Setup
{
    /// <summary>
    /// Represents a folder in which files may be installed.
    /// </summary>
    public class DirectoryData
    {
        List<FileData> files;
        string folderName;

        /// <summary>
        /// Adds a file to the file installation list.
        /// </summary>
        /// <param name="file">File to install in this directory.</param>
        public void AddFile(FileData file)
        {
            if (file == null) throw new ArgumentNullException();
            files.Add(file);
        }

        /// <summary>
        /// Creates a new DirectoryData instance.
        /// </summary>
        /// <param name="name">Name of the folder.</param>
        public DirectoryData(string name)
        {
            files = new List<FileData>();
            this.folderName = name;
        }

        /// <summary>
        /// Gets the name of the folder.
        /// </summary>
        public string Name
        {
            get
            {
                return folderName;
            }
        }

        /// <summary>
        /// Gets an array containing all of the files to be installed in this folder.
        /// </summary>
        public FileData[] Files
        {
            get
            {
                return files.ToArray();
            }
        }
    }
}
