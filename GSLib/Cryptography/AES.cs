using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using GSLib.Collections;

namespace GSLib.Cryptography
{
    /// <summary>
    /// Represents a wrapper class for the RijndaelManaged class. Provides the
    /// ability to encrypt and decrypt byte arrays using a symmetric key, as 
    /// well as generate and encrypt/decrypt private keys and initialization vectors.
    /// </summary>
    /// VB: Public Class AES
    public class AES
    {
        /// <summary>
        /// Encrypts a byte array using AES.
        /// </summary>
        /// <param name="data">Data to encrypt.</param>
        /// <param name="key">Key to use.</param>
        /// <param name="IV">Initialization vector to use.</param>
        /// <param name="countAdded">Number of bytes added to pad the result.</param>
        /// <returns>Returns a byte array representing the encrypted data.</returns>
        /// VB: Public Function Encrypt(ByVal data() As Byte, ByVal IV() As Byte) As Byte()
        public byte[] Encrypt(byte[] data, byte[] key, byte[] IV, out int countAdded)
        {
            RijndaelManaged aes = new RijndaelManaged();    // AES encryption class
            aes.Padding = PaddingMode.None;
            MemoryStream outStream = new MemoryStream();    // This is our final output stream
            using (CryptoStream cryptoStream = new CryptoStream(outStream, aes.CreateEncryptor(key, IV), CryptoStreamMode.Write))  // This stream automatically encrypts all of the incoming data.
            {
                byte[] padded = PadData(data, out countAdded);
                cryptoStream.Write(padded, 0, padded.Length);
                cryptoStream.FlushFinalBlock();
            }  

            byte[] outArray = outStream.ToArray();
            
            outStream.Close();
            return outArray;
        }

        /// <summary>
        /// Decrypts a byte array using AES.
        /// </summary>
        /// <param name="data">Encrypted data to decrypt.</param>
        /// <param name="key">Key to use.</param>
        /// <param name="IV">Initialization vector to use.</param>
        /// <param name="bytesAdded">Number of bytes added to pad the result.</param>
        /// <returns>Returns a byte array representing the original, unencrypted data.</returns>
        /// VB: Public Function Decrypt(ByVal data() As Byte, ByVal key() As Byte, ByVal IV() As Byte) As Byte()
        public byte[] Decrypt(byte[] data, byte[] key, byte[] IV, out int bytesAdded)
        {
            RijndaelManaged aes = new RijndaelManaged();    // AES encryption class
            aes.Padding = PaddingMode.None;
            MemoryStream outStream = new MemoryStream();    // output stream
            CryptoStream cryptoStream = new CryptoStream(outStream, aes.CreateDecryptor(key, IV), CryptoStreamMode.Write);    // stream automatically decrypts all incoming data

            byte[] padded = PadData(data, out bytesAdded);
            cryptoStream.Write(padded, 0, padded.Length);

            byte[] outArray = outStream.ToArray();
            cryptoStream.Close();
            outStream.Close();
            return outArray;
        }

        /// <summary>
        /// Pads a byte array with zeros at the end using a modulus of 16.
        /// </summary>
        /// <param name="data">The data to pad.</param>
        /// <param name="countAdded">The number of bytes added.</param>
        /// <returns>Returns the padded array.</returns>
        private byte[] PadData(byte[] data, out int countAdded)
        {
            int x = data.Length % 16;
            if (x > 0) countAdded = 16 - x;
            else countAdded = 0;

            if (countAdded != 0)
            {
                List<byte> result = new List<byte>();
                result.AddRange(data);
                result.AddRange(new byte[countAdded]);
                return result.ToArray();
            }

            return data;
        }

        /// <summary>
        /// Removes the given number of bytes from the array and returns the result.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static byte[] UnpadData(byte[] data, int count)
        {
            Range<byte> range = new Range<byte>(0, data.Length - count, data);
            return range.Values;
        }

        /// <summary>
        /// Encrypts a byte array using AES.
        /// </summary>
        /// <param name="data">Data to encrypt.</param>
        /// <param name="kiv">KeyIV to use.</param>
        /// <param name="countAdded">Number of bytes added to pad the result.</param>
        /// <returns>Returns the encrypted data.</returns>
        /// VB: Public Function Encrypt(ByVal data() As Byte, ByVal kiv As KeyIV) As Byte()
        public byte[] Encrypt(byte[] data, KeyIV kiv, out int countAdded)
        {
            return Encrypt(data, kiv.Key, kiv.IV, out countAdded);
        }

        /// <summary>
        /// Decrypts a byte array using AES.
        /// </summary>
        /// <param name="data">Data to decrypt.</param>
        /// <param name="kiv">KeyIV to use.</param>
        /// <param name="countAdded">Number of bytes added to pad the result.</param>
        /// <returns>Returns the decrypted data.</returns>
        /// VB: Public Function Decrypt(ByVal data() As Byte, ByVal kiv As KeyIV)
        public byte[] Decrypt(byte[] data, KeyIV kiv, out int countAdded)
        {
            return Decrypt(data, kiv.Key, kiv.IV, out countAdded);
        }

        /// <summary>
        /// Generates a symmetric AES key.
        /// </summary>
        /// <returns>Returns a byte array representing the key.</returns>
        /// VB: Public Function GenerateKey() As Byte()
        public byte[] GenerateKey()
        {
            KeyIV kiv = new KeyIV();
            return kiv.Key;
        }

        /// <summary>
        /// Generates an AES initialization vector.
        /// </summary>
        /// <returns>Returns a byte array representing the initialization vector.</returns>
        /// VB: Public Function GenerateIV() As Byte()
        public byte[] GenerateIV()
        {
            KeyIV kiv = new KeyIV();
            return kiv.IV;
        }

        /// <summary>
        /// Generates a KeyIV object, containing a symmetric key and an initialization vector.
        /// </summary>
        /// <returns>Returns a new KeyIV object.</returns>
        /// VB: Public Function GenerateKeyIV() As Byte()
        public KeyIV GenerateKeyIV()
        {
            return new KeyIV();
        }
    }
}
