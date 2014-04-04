using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GSLib.IO.Compression
{
    public interface ICompress
    {
        void Compress(string[] filePaths, string outputPath, Encoding encoding);
        void Compress(FileData[] files, string outputPath);
        void Compress(FileInfo[] files, string outputPath, Encoding encoding);
        void Compress(byte[] bytes, string outputPath);
        byte[] Compress(string[] filePaths, Encoding encoding);
        byte[] Compress(FileData[] files);
        byte[] Compress(FileInfo[] files, Encoding encoding);
        byte[] Compress(byte[] bytes);
        void Decompress(string compressedFile, string outputDir, Encoding encoding);
        byte[] Decompress(byte[] compressedData);
    }
}
