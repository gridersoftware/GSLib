using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace GSLib.Cryptography
{
    /// <summary>
    /// Represents a symmetric key and initialization vector for an AES object.
    /// </summary>
    public class KeyIV
    {
        byte[] key;
        byte[] iv;

        public byte[] Key
        {
            get
            {
                return key;
            }
            set
            {
                if ((key.Length == 128) || (key.Length == 192) || (key.Length == 256))
                    key = value;
                else
                    throw new ArgumentException("Key must be 128, 192, or 256 bytes long.");
            }
        }

        public byte[] IV
        {
            get
            {
                return iv;
            }
            set
            {
                iv = value;
            }
        }

        /// <summary>
        /// Constructor. Generates a new key and initialization vector automatically.
        /// </summary>
        public KeyIV()
        {
            RijndaelManaged aes = new RijndaelManaged();
            key = aes.Key;
            iv = aes.IV;
        }

        /// <summary>
        /// Constructor. Uses key and initialization vector provided.
        /// </summary>
        /// <param name="symkey">Symmetric key</param>
        /// <param name="IV">Initialization vector</param>
        public KeyIV(byte[] symkey, byte[] IV)
        {
            key = symkey;
            iv = IV;
        }

        public EncryptedKeyIV Encrypt()
        {
            return new EncryptedKeyIV(this);
        }
    }
}
