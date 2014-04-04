using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Security;

namespace GSLib.Setup
{
    /// <summary>
    /// Represents a file and its associated data.
    /// </summary>
    public class FileData
    {
        public enum FileSizeUnit
        {
            Bytes,
            Kilobytes,
            Megabytes
        }

        /***********************************************************************
         * Public Fields
         **********************************************************************/ 
        /// <summary>
        /// Contains the name of the file.
        /// </summary>
        protected string fileName;

        /// <summary>
        /// Contains the bytes that make up the file.
        /// </summary>
        protected byte[] filebytes;

        /***********************************************************************
         * Public Properties
         **********************************************************************/
        /// <summary>
        /// Gets a value containing the name of the file.
        /// </summary>
        public string Filename
        {
            get
            {
                return fileName;
            }
        }

        /// <summary>
        /// Gets or sets a value determining if the file will be installed.
        /// </summary>
        public bool Install { get; set; }

        public long FileSizeBytes
        {
            get
            {
                return filebytes.LongCount();
            }
        }

        public long FileSizeKBytes
        {
            get
            {
                return filebytes.LongCount() / 1024;
            }
        }

        public long FileSizeMBytes
        {
            get
            {
                return filebytes.LongCount() / (1024 * 1024);
            }
        }
        /***********************************************************************
         * Constructors
         **********************************************************************/
        /// <summary>
        /// Creates a new FileData object.
        /// </summary>
        /// <param name="filename">Filename to create.</param>
        /// <param name="installPath">Installation path relative to the base install path.</param>
        /// <param name="file">File bytes.</param>
        /// <exception cref="ArgumentNullException">Thrown if one or more arguments are null.</exception>
        /// <exception cref="ArgumentException">Thrown if filename, installPath, or both are empty.</exception>
        public FileData(string filename, byte[] file)
        {
            if ((filename == null) | (file == null)) throw new ArgumentNullException();
            if (filename == "") throw new ArgumentException();

            this.fileName = filename;
            filebytes = file;
        }

        /***********************************************************************
         * Public Methods
         **********************************************************************/
        public long GetFileSize(FileSizeUnit unit)
        {
            switch (unit)
            {
                case FileSizeUnit.Bytes:
                    return FileSizeBytes;

                case FileSizeUnit.Kilobytes:
                    return FileSizeKBytes;

                case FileSizeUnit.Megabytes:
                    return FileSizeMBytes;

                default:
                    return FileSizeBytes;
            }
        }

        internal void InstallFile(DirectoryInfo dir)
        {
            try
            {
                if (Install) File.WriteAllBytes(dir.GetPath() + fileName, filebytes);
            }
            catch
            {
                throw;
            }

            //catch (ArgumentException)
            //{
            //    MessageBox.Show("Someone made a big mistake!\n\nThe installation path is empty. Please notify the distributor.");
            //    // TODO: Unrecoverable error. Exit the setup.
            //}
            //catch (PathTooLongException)
            //{
            //    MessageBox.Show("File could not be installed at \"" + rootPath + installPath + fileName + "\"\nbecause the path is too long.");
            //    // TODO: Unrecoverable error. Exit the setup.
            //}
            //catch (DirectoryNotFoundException)
            //{
            //    MessageBox.Show("The specified path is invalid. Make sure your destination disk is connected or inserted then click Ok.");
            //    return InstallFile(rootPath);
            //}
            //catch (IOException)
            //{
            //    switch (MessageBox.Show("An I/O error occured while opening the file. Click Retry to try again, Abort to cancel the installation, or Ignore to skip the file.", "Setup", MessageBoxButtons.AbortRetryIgnore))
            //    {
            //        case DialogResult.Abort:
            //        // TODO: Exit the setup.
            //        case DialogResult.Retry:
            //            return InstallFile(rootPath);
            //    }
            //}
            //catch (UnauthorizedAccessException)
            //{
            //    switch (MessageBox.Show("Setup does not have permission to write. Click Retry to try again, Abort to cancel the installation, or Ignore to skip the file.", "Setup", MessageBoxButtons.AbortRetryIgnore))
            //    {
            //        case DialogResult.Abort:
            //        // TODO: Exit the setup.
            //        case DialogResult.Retry:
            //            return InstallFile(rootPath);
            //    }
            //}
            //catch (NotSupportedException)
            //{
            //    MessageBox.Show("Someone made a big mistake!\n\nThe specified path is in an invalid format. Please notify the distributor.");
            //    // TODO: Unrecoverable error. Exit the setup.
            //}
            //catch (SecurityException)
            //{
            //    switch (MessageBox.Show("Setup does not have permission to write. Click Retry to try again, Abort to cancel the installation, or Ignore to skip the file.", "Setup", MessageBoxButtons.AbortRetryIgnore))
            //    {
            //        case DialogResult.Abort:
            //        // TODO: Exit the setup.
            //        case DialogResult.Retry:
            //            return InstallFile(rootPath);
            //    }
            //}
        }
    }
}
