using System;
using System.Security.Cryptography;
using System.Text;

namespace GeneralToolkitLib.Encryption
{
    public class RsaAsymmetricEncryption
    {
        public enum RSAKeySize
        {
            b1024 = 1024,
            b2048 = 2048,
            b4096 = 4096,
            b8192 = 8192,
            b16384 = 16384
        }

        private const string BEGIN_RSA_PUBLIC_KEY = "-----BEGIN RSA PUBLIC KEY-----";
        private const string END_RSA_PUBLIC_KEY = "-----END RSA PUBLIC KEY-----";

        private const string BEGIN_RSA_PRIVATE_KEY = "-----BEGIN RSA PRIVATE KEY-----";
        private const string END_RSA_PRIVATE_KEY = "-----END RSA PRIVATE KEY-----";

        public RSAKeySetIdentity GenerateRsaKeyPair(RSAKeySize keySize)
        {
            RSAKeySetIdentity keySet;
            using (var rsa = new RSACryptoServiceProvider((int) keySize))
            {
                byte[] rsaPrivateKeyData = rsa.ExportCspBlob(true);
                byte[] rsaPublicKeyData = rsa.ExportCspBlob(false);


                StringBuilder tmp = new StringBuilder(Convert.ToBase64String(rsaPrivateKeyData));
                for (int i = 64; i < tmp.Length; i += 66)
                {
                    tmp.Insert(i, "\r\n");
                }

                string privKey = BEGIN_RSA_PRIVATE_KEY + "\r\n" + tmp + "\r\n" + END_RSA_PRIVATE_KEY;

                tmp = new StringBuilder(Convert.ToBase64String(rsaPublicKeyData));
                for (int i = 64; i < tmp.Length; i += 66)
                {
                    tmp.Insert(i, "\r\n");
                }

                string pubKey = BEGIN_RSA_PUBLIC_KEY + "\r\n" + tmp + "\r\n" + END_RSA_PUBLIC_KEY;

                keySet = new RSAKeySetIdentity(privKey, pubKey);
            }

            return keySet;
        }

        public RSAParameters ParseRSAPublicKeyOnlyInfo(RSAKeySetIdentity rsaKeySet)
        {
            RSAParameters rsaParams;

            int startIndex = rsaKeySet.RSA_PublicKey.IndexOf(BEGIN_RSA_PUBLIC_KEY, StringComparison.Ordinal);
            int endIndex = rsaKeySet.RSA_PublicKey.IndexOf(END_RSA_PUBLIC_KEY, StringComparison.Ordinal);

            if (startIndex < 0 || endIndex < 0)
                throw new ArgumentException("Invalid key data");

            string keyData = rsaKeySet.RSA_PublicKey.Substring(startIndex + BEGIN_RSA_PUBLIC_KEY.Length, endIndex - startIndex - BEGIN_RSA_PUBLIC_KEY.Length).Replace("\r\n", "");
            byte[] keyBlobBytes = Convert.FromBase64String(keyData);
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportCspBlob(keyBlobBytes);
                rsaParams = rsa.ExportParameters(false);
            }

            return rsaParams;
        }

        public RSAParameters ParseRSAKeyInfo(RSAKeySetIdentity rsaKeySet)
        {
            RSAParameters rsaParams;

            int startIndex = rsaKeySet.RSA_PrivateKey.IndexOf(BEGIN_RSA_PRIVATE_KEY, StringComparison.Ordinal);
            int endIndex = rsaKeySet.RSA_PrivateKey.IndexOf(END_RSA_PRIVATE_KEY, StringComparison.Ordinal);

            if (startIndex < 0 || endIndex < 0)
                throw new ArgumentException("Invalid key data");

            string keyData = rsaKeySet.RSA_PrivateKey.Substring(startIndex + BEGIN_RSA_PRIVATE_KEY.Length, endIndex - startIndex - BEGIN_RSA_PRIVATE_KEY.Length).Replace("\r\n", "");
            byte[] keyBlobBytes = Convert.FromBase64String(keyData);
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportCspBlob(keyBlobBytes);
                rsaParams = rsa.ExportParameters(true);
            }


            return rsaParams;
        }

        public string EncryptObjectUsingRSA(string plaintextString, RSAParameters rsaParameters)
        {
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.ImportParameters(rsaParameters);
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintextString);
                byte[] encodedData = rsa.Encrypt(plaintextBytes, true);

                return Convert.ToBase64String(encodedData);
            }
        }

        public string DecryptObjectUsingRSA(string b64EncodedData, RSAParameters rsaParameters)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096))
            {
                byte[] encodedData = Convert.FromBase64String(b64EncodedData);
                rsa.ImportParameters(rsaParameters);
                byte[] plaintextBytes = rsa.Decrypt(encodedData, true);

                return Encoding.UTF8.GetString(plaintextBytes);
            }
        }
    }
}