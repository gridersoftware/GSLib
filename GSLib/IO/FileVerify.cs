using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace GSLib.IO
{
    public class FileVerify
    {
        /// <summary>
        /// Verifies the integrity of a file based on a given MD5 hash.
        /// </summary>
        /// <param name="path">Path to a file to check.</param>
        /// <param name="hash">MD5 hash to compare to.</param>
        /// <returns>Returns true if the file is valid, otherwise returns false.</returns>
        public static bool VerifyFile(string path, byte[] hash)
        {
            if (File.Exists(path))
            {
                return (Collections.Helpers.CompareArrays<byte>(hash, GenerateHash(path)) == 0);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Verifies the integreity of a file based on a given SHA256 hash.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool FileVerifySha(string path, byte[] hash)
        {
            if (File.Exists(path))
            {
                return (Collections.Helpers.CompareArrays<byte>(hash, GenerateHashSha(path)) == 0);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Generates an MD5 hash from a given file.
        /// </summary>
        /// <param name="path">Path to file to hash.</param>
        /// <returns>Hash generated from file.</returns>
        public static byte[] GenerateHash(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    FileStream stream = new FileStream(path, FileMode.Open);

                    using (MD5 md5 = MD5.Create())
                    {
                        return md5.ComputeHash(stream);
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Generates an SHA256 Hash from a given file.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <returns>Hash generated from the file.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the file could not be found.</exception>
        public static byte[] GenerateHashSha(string path)
        {
            if (File.Exists(path))
            {
                SHA256Managed sha = new SHA256Managed();
                byte[] data = File.ReadAllBytes(path);
                return sha.ComputeHash(data);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
