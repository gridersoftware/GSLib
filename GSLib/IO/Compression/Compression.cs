using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GSLib.IO.Compression
{
    /// <summary>
    /// Provides a generic way of compressing and decompressing data.
    /// </summary>
    public abstract class Compression : ICompress
    {
        /// <summary>
        /// Initializes a new Compression class.
        /// </summary>
        public Compression()
        {
        }

        /// <summary>
        /// When overridden in a derived class, compresses the given byte array.
        /// </summary>
        /// <param name="bytes">Byte array to be compressed.</param>
        /// <returns>Returns a byte array of compressed data.</returns>
        public abstract byte[] Compress(byte[] bytes);

        /// <summary>
        /// Compresses the files given and writes the output to the given path.
        /// </summary>
        /// <param name="filePaths">Files to compress.</param>
        /// <param name="outputPath">Where to write files.</param>
        /// <param name="encoding">String encoding to use.</param>
        public virtual void Compress(string[] filePaths, string outputPath, Encoding encoding)
        {
            if ((filePaths == null) | (outputPath == null)) throw new ArgumentNullException();
            if (filePaths.Length == 0) throw new ArgumentException();
            if (outputPath == "") throw new ArgumentException();
            
            FileInfo f = new FileInfo(outputPath);
            if (f.Exists & f.IsReadOnly) throw new UnauthorizedAccessException();

            foreach (string file in filePaths)
            {
                if (!File.Exists(file)) throw new FileNotFoundException();
            }

            try
            {
                File.WriteAllBytes(outputPath, Compress(filePaths, encoding));
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Compresses the files given and writes the output to the given path.
        /// </summary>
        /// <param name="files">Files to compress.</param>
        /// <param name="outputPath">Output path.</param>
        public virtual void Compress(FileData[] files, string outputPath)
        {
            try
            {
                File.WriteAllBytes(outputPath, Compress(files));
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Compresses the byte array and writes the output to the given path.
        /// </summary>
        /// <param name="bytes">Data to compress.</param>
        /// <param name="outputPath">Output path.</param>
        public virtual void Compress(byte[] bytes, string outputPath)
        {
            try
            {
                File.WriteAllBytes(outputPath, Compress(bytes));
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Compresses the given files and returns a byte array representing those files.
        /// </summary>
        /// <param name="filePaths">Paths of files to compress.</param>
        /// <param name="encoding">Method for encoding file name strings.</param>
        /// <returns>Returns a byte array containing the compressed data.</returns>
        public virtual byte[] Compress(string[] filePaths, Encoding encoding)
        {
            FileData[] files = new FileData[filePaths.Length];
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    files[i] = new FileData(new FileInfo(filePaths[i]), encoding);
                }
                catch
                {
                    throw;
                }
            }
            return Compress(files);
        }

        /// <summary>
        /// Compresses the given files and returns the compressed data as a byte array.
        /// </summary>
        /// <param name="files">Files to compress.</param>
        /// <returns>Compressed data as a byte array.</returns>
        public virtual byte[] Compress(FileData[] files)
        {
            List<byte> uncompressed = new List<byte>();

            // add file count
            uncompressed.AddRange(BitConverter.GetBytes(files.Length));
            foreach (FileData file in files)
            {
                // add filename length
                uncompressed.AddRange(BitConverter.GetBytes(file.fileName.Length));

                // add filename
                uncompressed.AddRange(file.fileName);

                // add file data length
                uncompressed.AddRange(BitConverter.GetBytes(file.bytes.Length));

                // add file bytes
                uncompressed.AddRange(file.bytes);
            }

            return Compress(uncompressed.ToArray());
        }

        /// <summary>
        /// Compresses the given files and returns the compressed data as a byte array.
        /// </summary>
        /// <param name="files">Files to compress.</param>
        /// <param name="encoding">Encoding scheme for file names.</param>
        /// <returns>Compressed data as a byte array.</returns>
        public virtual byte[] Compress(FileInfo[] files, Encoding encoding)
        {
            FileData[] f = new FileData[files.Length];

            for (int i = 0; i < f.Length; i++)
            {
                f[i] = new FileData(files[i], encoding);
            }

            return Compress(f);
        }

        /// <summary>
        /// Decompresses the given byte array and returns the decompressed data as a byte array.
        /// </summary>
        /// <param name="compressedData">Byte array to decompress.</param>
        /// <returns>Decompressed data as a byte array.</returns>
        public abstract byte[] Decompress(byte[] compressedData);
        
        /// <summary>
        /// Decompresses the given file and writes its contents to the given directory.
        /// </summary>
        /// <param name="compressedFile">Path to compressed file.</param>
        /// <param name="outputDir">Output directory.</param>
        /// <param name="encoding">Encoding scheme of for file names.</param>
        /// <remarks>This method operates under the assumption that the data was compressed using one of the included Compress methods.</remarks>
        public virtual void Decompress(string compressedFile, string outputDir, Encoding encoding)
        {
            MemoryStream stream;


            if (!File.Exists(compressedFile)) throw new FileNotFoundException();

            DirectoryInfo dir = new DirectoryInfo(outputDir);
            if (!dir.Exists)
            {
                try
                {
                    dir.Create();
                }
                catch
                {
                    throw;
                }
            }

            stream = new MemoryStream(Decompress(File.ReadAllBytes(compressedFile)));

            byte[] buffer = new byte[sizeof(int)];
            int fileCount;

            // get file count
            stream.Read(buffer, 0, sizeof(int));
            fileCount = BitConverter.ToInt32(buffer, 0);

            for (int i = 0; i < fileCount; i++)
            {
                // get filename length
                stream.Read(buffer, 0, sizeof(int));
                int len = BitConverter.ToInt32(buffer, 0);

                // get filename
                buffer = new byte[len];
                stream.Read(buffer, 0, len);
                string fileName = encoding.GetString(buffer);

                // get file size
                buffer = new byte[sizeof(int)];
                stream.Read(buffer, 0, sizeof(int));
                len = BitConverter.ToInt32(buffer, 0);

                // get file data
                buffer = new byte[len];
                stream.Read(buffer, 0, len);

                File.WriteAllBytes(dir.FullName + "\\" + fileName, buffer);
            }
        }

        /// <summary>
        /// Compresses the given files and writes the output to the given path.
        /// </summary>
        /// <param name="files">Files to compress.</param>
        /// <param name="outputPath">Output path.</param>
        /// <param name="encoding">Encoding scheme for file names.</param>
        public virtual void Compress(FileInfo[] files, string outputPath, Encoding encoding)
        {
            try
            {
                File.WriteAllBytes(outputPath, Compress(files, encoding));
            }
            catch
            {
                throw;
            }
        }
    }
}
