using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace GSLib.Cryptography
{
    public class RSA
    {
        /**********************************************************************/
        /* Fields                                                         
        /**********************************************************************/
        RSACryptoServiceProvider rsa;

        /**********************************************************************/
        /* Properties                                                         
        /**********************************************************************/
        private RSAParameters key;
        public RSAParameters PublicKey
        {
            get
            {
                return key;
            }
        }

        private byte[] storeBytes;
        public byte[] StoreBytes
        {
            get
            {
                return storeBytes;
            }
        }

        private string storeName;
        public string StoreName
        {
            get
            {
                return storeName;
            }
        }

        

        /**********************************************************************/
        /* Constructors                                                         
        /**********************************************************************/
        public RSA()
        {
            GenerateKey(out storeName);
        }

        public RSA(string store)
        {
            storeName = store;
            RetrieveKey(store);
        }

        /**********************************************************************/
        /* Methods                                                         
        /**********************************************************************/
        public RSAParameters GenerateKey(out string storeName)
        {
            CspParameters cp = new CspParameters();
            Guid guid = new Guid();
            storeName = guid.ToString();
            storeBytes = guid.ToByteArray();
            cp.KeyContainerName = storeName;

            rsa = new RSACryptoServiceProvider(cp);
            key = rsa.ExportParameters(false);
            return PublicKey;
        }

        public RSAParameters RetrieveKey(string storeName)
        {
            CspParameters cp = new CspParameters();

            cp.KeyContainerName = storeName;
            rsa = new RSACryptoServiceProvider(cp);
            key = rsa.ExportParameters(false);
            return PublicKey;
        }

        public byte[] Encrypt(byte[] data)
        {
            return rsa.Encrypt(data, false);
        }

        public byte[] Decrypt(byte[] data)
        {
            return rsa.Decrypt(data, false);
        }

        public static void DeleteKey(string storeName)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = storeName;

            // Create a new instance of RSACryptoServiceProvider that accesses
            // the key container.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            // Delete the key entry in the container.
            rsa.PersistKeyInCsp = false;

            rsa.Clear();
        }
    }
}
