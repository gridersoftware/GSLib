using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace GSLib.IO.Compression
{
    /// <summary>
    /// Provides wrapper for compressing and decompressing data using Deflate.
    /// </summary>
    public class Deflate : Compression
    {
        public Deflate()
            : base() { }

        public override byte[] Compress(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream();
            DeflateStream ds = new DeflateStream(ms, CompressionMode.Compress);

            ds.Write(bytes, 0, bytes.Length);
            ds.Close();

            byte[] buffer = ms.ToArray();
            ms.Close();
            return buffer;
        }

        public override byte[] Decompress(byte[] compressedData)
        {
            MemoryStream compressed = new MemoryStream();
            MemoryStream decompressed = new MemoryStream();
            DeflateStream deflate = new DeflateStream(compressed, CompressionMode.Decompress);

            compressed.Write(compressedData, 0, compressedData.Length);
            byte[] buffer = new byte[1024];
            int countRead = 1;

            while (countRead > 0)
            {
                countRead = deflate.Read(buffer, 0, 1024);
                if (countRead > 0) decompressed.Write(buffer, 0, countRead);
            }

            return decompressed.ToArray();
        }

        public override void Decompress(string compressedFile, string outputDir, Encoding encoding)
        {
            FileStream fs;

            if (File.Exists(compressedFile))
                fs = new FileStream(compressedFile, FileMode.Open);
            else
                throw new FileNotFoundException();

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


            DeflateStream ds = new DeflateStream(fs, CompressionMode.Decompress);

            byte[] buffer = new byte[sizeof(int)];
            int fileCount;

            // get file count
            ds.Read(buffer, 0, sizeof(int));
            fileCount = BitConverter.ToInt32(buffer, 0);

            for (int i = 0; i < fileCount; i++)
            {
                // get filename length
                ds.Read(buffer, 0, sizeof(int));
                int len = BitConverter.ToInt32(buffer, 0);

                // get filename
                buffer = new byte[len];
                ds.Read(buffer, 0, len);
                string fileName = encoding.GetString(buffer);

                // get file size
                buffer = new byte[sizeof(int)];
                ds.Read(buffer, 0, sizeof(int));
                len = BitConverter.ToInt32(buffer, 0);

                // get file data
                buffer = new byte[len];
                ds.Read(buffer, 0, len);

                File.WriteAllBytes(dir.FullName + "\\" + fileName, buffer);
            }
        }

        
    }
}
