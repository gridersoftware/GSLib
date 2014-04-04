using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GSLib.IO
{
    public struct FileData
    {
        internal FileInfo file;
        internal byte[] bytes;
        internal byte[] fileName;
        internal string fileNameStr;

        /// <summary>
        /// Gets the FileInfo object representing the file.
        /// </summary>
        public FileInfo File
        {
            get
            {
                return file;
            }
        }

        /// <summary>
        /// Gets the bytes contained in the file.
        /// </summary>
        public byte[] Bytes
        {
            get
            {
                return bytes;
            }
        }

        /// <summary>
        /// Gets the file name in byte format.
        /// </summary>
        public byte[] FileNameBytes
        {
            get
            {
                return fileName;
            }
        }

        /// <summary>
        /// Gets the filename string.
        /// </summary>
        public string FileName
        {
            get
            {
                return fileNameStr;
            }
        }

        /// <summary>
        /// Initializes a new FileData object with the given FileInfo object.
        /// </summary>
        /// <param name="file">FileInfo from which to open the file.</param>
        /// <param name="encoding">Encoding method of the file name string.</param>
        public FileData(FileInfo file, Encoding encoding)
        {
            this.file = file;
            if (file.Exists)
            {
                bytes = System.IO.File.ReadAllBytes(file.FullName);
                fileName = encoding.GetBytes(file.Name);
                fileNameStr = file.Name;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Initializes a new FileData object with the given file name, and containing the given data.
        /// </summary>
        /// <param name="filename">Name of the file.</param>
        /// <param name="data">Data contained in the file.</param>
        /// <param name="encoding">Encoding method of the file name string.</param>
        /// <remarks>This constructor does not look to determine if the file already exists.</remarks>
        public FileData(string filename, byte[] data, Encoding encoding)
        {
            if ((filename != null) & (data != null))
            {
                if (filename == "") throw new ArgumentException();

                file = new FileInfo(filename);
                bytes = data;
                fileName = encoding.GetBytes(file.Name);
                fileNameStr = file.Name;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
