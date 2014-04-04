using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Cryptography
{
    /// <summary>
    /// Represents an encrypted symmetric key and initialization vector
    /// </summary>
    public class EncryptedKeyIV
    {
        byte[] key;
        byte[] iv;

        private byte[] storeByte;
        public byte[] StoreBytes
        {
            get
            {
                return storeByte;
            }
        }

        string storeName;
        public string StoreName
        {
            get
            {
                return storeName;
            }
        }

        public byte[] EncryptedKey
        {
            get
            {
                return key;
            }
        }

        public byte[] EncryptedIV
        {
            get
            {
                return iv;
            }
        }

        public EncryptedKeyIV(KeyIV raw)
        {
            RSA enc = new RSA();
            key = enc.Encrypt(raw.Key);
            iv = enc.Encrypt(raw.IV);
            storeName = enc.StoreName;
            storeByte = enc.StoreBytes;
        }

        public EncryptedKeyIV(byte[] encryptedKey, byte[] encryptedIV, string store)
        {
            key = encryptedKey;
            iv = encryptedIV;
            storeName = store;
        }

        public KeyIV Decrypt()
        {
            RSA enc = new RSA(storeName);
            return new KeyIV(enc.Decrypt(EncryptedKey), enc.Decrypt(EncryptedIV));
        }

        public void DeleteRSAKey()
        {
            RSA.DeleteKey(storeName);
        }
    }
}
