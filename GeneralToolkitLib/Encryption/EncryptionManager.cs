using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using SHA256 = GeneralToolkitLib.Hashing.SHA256;

namespace GeneralToolkitLib.Encryption
{
    public class EncryptionManager
    {
        private const int MaxBufferSize = 33554432; //32 Mb

        private static readonly byte[] SALT =
        {
            0x16, 0x81, 0x38, 0x37, 0x1d, 0x3e, 0x97, 0x2, 0x4d, 0x72, 0x69, 0xaa, 0x99, 0x92, 0x64, 0x3d, 0xa0, 0x10, 0x65, 0x2, 0xef, 0x4c, 0x72, 0xb9, 0xbe, 0x75,
            0x9, 0xee, 0x6e, 0x9a, 0x9b, 0x12
        };

        private static readonly byte[] SALT2 =
        {
            0x65, 0x0, 0xa, 0x13, 0x63, 0xf1, 0x57, 0x96, 0xc2, 0x51, 0x4e, 0xef, 0xe2, 0x52, 0xa2, 0xb1, 0xe6, 0x7a, 0xbd, 0x26, 0x8b, 0x99, 0x9b, 0x6e, 0xc1, 0xdf,
            0x34, 0xb6, 0x33, 0x1c, 0x0, 0x76
        };
        
        public async Task<bool> EncryptAndSaveFileAsync(string filePath, MemoryStream ms, string passwordString, CryptoProgress progress)
        {
            bool result = await Task.Run(() => EncryptAndSaveFile(filePath, ms, passwordString, progress));
            return result;
        }

        public bool EncryptAndSaveFile(string filePath, MemoryStream ms, string passwordString, CryptoProgress progress)
        {
            FileStream fs = null;

            try
            {
                CryptoProgressHandler progressHandler = null;

                if (string.IsNullOrEmpty(passwordString))
                    throw new Exception("Password can not be null or empty");

                if (File.Exists(filePath))
                    File.Delete(filePath);

                fs = File.Create(filePath);

                if (progress != null)
                {
                    progressHandler = new CryptoProgressHandler { EncodedBytes = 0, TotalBytes = ms.Length, Text = "Starting ecryption" };
                    progress.Report(progressHandler);
                }

                using (Aes aesAlg = Aes.Create())
                {
                    var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordString, SALT, 1000);
                    Debug.Assert(aesAlg != null, nameof(aesAlg) + " != null");
                    aesAlg.BlockSize = 128;
                    aesAlg.KeySize = 256;
                    aesAlg.Padding = PaddingMode.PKCS7;
                    aesAlg.Mode = CipherMode.CBC;

                    aesAlg.Key = rfc2898DeriveBytes.GetBytes(32);
                    aesAlg.IV = rfc2898DeriveBytes.GetBytes(16);

                    // Create a encrypt transform
                    ICryptoTransform encrypt = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    int bufferSize = (int)Math.Min(MaxBufferSize, ms.Length);
                    var buffer = new byte[bufferSize];
                    ms.Position = 0;

                    using (var csEncrypt = new CryptoStream(fs, encrypt, CryptoStreamMode.Write))
                    {
                        int bytesRead;
                        while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            csEncrypt.Write(buffer, 0, bytesRead);
                            if (progressHandler == null) continue;
                            progressHandler.EncodedBytes += bytesRead;
                            progress.Report(progressHandler);
                        }

                        csEncrypt.FlushFinalBlock();
                        fs.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EncryptionManager.EncryptAndSaveFile");
                return false;
            }
            finally
            {
                fs?.Close();
                progress?.Report(new CryptoProgressHandler { EncodedBytes = ms.Length, TotalBytes = ms.Length, Text = "Encryption completed" });
            }

            return true;
        }

        public async Task<MemoryStream> DecryptFileToMemoryStreamAsync(string filePath, string passwordString, CryptoProgress progress)
        {
            MemoryStream result = await Task.Run(() => DecryptFileToMemoryStream(filePath, passwordString, progress));
            return result;
        }

        public MemoryStream DecryptFileToMemoryStream(string filePath, string passwordString, CryptoProgress progress)
        {
            var ms = new MemoryStream();
            FileStream fs = null;
            try
            {
                CryptoProgressHandler progressHandler = null;
                if (string.IsNullOrEmpty(passwordString))
                    throw new Exception("Password can not be null or empty");

                fs = File.OpenRead(filePath);
                fs.Position = 0;


                if (progress != null)
                {
                    progressHandler = new CryptoProgressHandler { EncodedBytes = 0, TotalBytes = fs.Length, Text = "Starting decryption" };
                    progress.Report(progressHandler);
                }

                // Create an AesCryptoServiceProvider object 
                using (Aes aesAlg = Aes.Create())
                {
                    var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordString, SALT, 1000);
                    aesAlg.BlockSize = 128;
                    aesAlg.KeySize = 256;
                    aesAlg.Padding = PaddingMode.PKCS7;
                    aesAlg.Mode = CipherMode.CBC;

                    aesAlg.Key = rfc2898DeriveBytes.GetBytes(32);
                    aesAlg.IV = rfc2898DeriveBytes.GetBytes(16);

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption.
                    int bufferSize = Math.Min(MaxBufferSize, (int)fs.Length);
                    var plainTextBytes = new byte[bufferSize];

                    using (var csDecrypt = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
                    {
                        int decryptedByteCount;
                        while ((decryptedByteCount = csDecrypt.Read(plainTextBytes, 0, plainTextBytes.Length)) > 0)
                        {
                            ms.Write(plainTextBytes, 0, decryptedByteCount);
                            if (progressHandler == null) continue;
                            progressHandler.EncodedBytes += decryptedByteCount;
                            progress.Report(progressHandler);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EncryptionManager.EncryptAndSaveFile");
                return null;
            }
            finally
            {
                fs?.Close();

                progress?.Report(new CryptoProgressHandler { EncodedBytes = ms.Length, TotalBytes = ms.Length, Text = "Decryption completed" });
            }

            return ms;
        }

        public static byte[] DecryptData(byte[] data, string password)
        {
            var msDecrypted = new MemoryStream();
            var msEncrypted = new MemoryStream(data);

            // Create an AesCryptoServiceProvider object 
            using (Aes aesAlg = Aes.Create())
            {
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SALT, 1000);
                if (aesAlg == null) return msDecrypted.ToArray();
                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 256;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Mode = CipherMode.CBC;

                aesAlg.Key = rfc2898DeriveBytes.GetBytes(32);
                aesAlg.IV = rfc2898DeriveBytes.GetBytes(16);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                int bufferSize = Math.Min(MaxBufferSize, (int)msEncrypted.Length);
                var plainTextBytes = new byte[bufferSize];

                using (var csDecrypt = new CryptoStream(msEncrypted, decryptor, CryptoStreamMode.Read))
                {
                    int decryptedByteCount;
                    while ((decryptedByteCount = csDecrypt.Read(plainTextBytes, 0, plainTextBytes.Length)) > 0)
                    {
                        msDecrypted.Write(plainTextBytes, 0, decryptedByteCount);
                    }
                }
            }

            return msDecrypted.ToArray();
        }

        public static byte[] EncryptData(byte[] data, string password)
        {
            var ms = new MemoryStream(data);
            var msEncodedData = new MemoryStream();

            using (Aes aesAlg = Aes.Create())
            {
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SALT, 1000);
                if (aesAlg == null) return msEncodedData.ToArray();
                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 256;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Mode = CipherMode.CBC;

                aesAlg.Key = rfc2898DeriveBytes.GetBytes(32);
                aesAlg.IV = rfc2898DeriveBytes.GetBytes(16);

                // Create AES Crypto Transform to be used in the CryptoStream transform function 
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                int bufferSize = (int)Math.Min(MaxBufferSize, ms.Length);
                var buffer = new byte[bufferSize];
                ms.Position = 0;

                using (var csEncrypt = new CryptoStream(msEncodedData, encryptor, CryptoStreamMode.Write))
                {
                    int bytesRead;
                    while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        csEncrypt.Write(buffer, 0, bytesRead);
                    }

                    csEncrypt.FlushFinalBlock();
                    msEncodedData.Flush();
                }
            }

            return msEncodedData.ToArray();
        }

        public static byte[] EncryptObject(object obj, string passwordString)
        {
            var binaryFormatter = new BinaryFormatter();
            var ms = new MemoryStream();
            var msEncodedData = new MemoryStream();
            binaryFormatter.Serialize(ms, obj);

            using (Aes aesAlg = Aes.Create())
            {
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordString, SALT, 1000);
                if (aesAlg == null) return msEncodedData.ToArray();
                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 256;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Mode = CipherMode.CBC;

                aesAlg.Key = rfc2898DeriveBytes.GetBytes(32);
                aesAlg.IV = rfc2898DeriveBytes.GetBytes(16);

                // Create AES Crypto Transform to be used in the CryptoStream transform function 
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                int bufferSize = (int)Math.Min(MaxBufferSize, ms.Length);
                var buffer = new byte[bufferSize];
                ms.Position = 0;

                using (var csEncrypt = new CryptoStream(msEncodedData, encryptor, CryptoStreamMode.Write))
                {
                    int bytesRead;
                    while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        csEncrypt.Write(buffer, 0, bytesRead);
                    }

                    csEncrypt.FlushFinalBlock();
                    msEncodedData.Flush();
                }
            }

            return msEncodedData.ToArray();
        }

        #region simple string encryption

        public static void EncodeString(ref byte[] buffer, string plaintext, string key)
        {
            //if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            using (Aes aesAlg = Aes.Create())
            {
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, SALT2, 1000);
                if (aesAlg == null) return;
                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 256;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Mode = CipherMode.CBC;

                aesAlg.Key = rfc2898DeriveBytes.GetBytes(32);
                aesAlg.IV = rfc2898DeriveBytes.GetBytes(16);

                // Create AES Crypto Transform to be used in the CryptoStream transform function 
                ICryptoTransform encrypt = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                var ms = new MemoryStream();
                using (var csEncrypt = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    var plainTextBytes = Encoding.UTF8.GetBytes(plaintext);

                    csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                    csEncrypt.FlushFinalBlock();
                    ms.Flush();
                }
                buffer = ms.ToArray();
            }
        }

        public static string GetDerivedPassword(string password1, string password2)
        {
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password1, SALT, 1000);
            var derivedBytes1 = rfc2898DeriveBytes.GetBytes(128);
            rfc2898DeriveBytes = new Rfc2898DeriveBytes(password2, SALT2, 1000);
            var derivedBytes2 = rfc2898DeriveBytes.GetBytes(128);

            return SHA256.GetSHA256HashAsHexString(derivedBytes1) + SHA256.GetSHA256HashAsHexString(derivedBytes2);
        }

        public static string DecodeString(ref byte[] buffer, string key)
        {
            try
            {
                // Create an AesCryptoServiceProvider object 
                using (Aes aesAlg = Aes.Create())
                {
                    var rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, SALT2, 1000);
                    aesAlg.BlockSize = 128;
                    aesAlg.KeySize = 256;
                    aesAlg.Padding = PaddingMode.PKCS7;
                    aesAlg.Mode = CipherMode.CBC;

                    aesAlg.Key = rfc2898DeriveBytes.GetBytes(32);
                    aesAlg.IV = rfc2898DeriveBytes.GetBytes(16);

                    // Create AES Crypto Transform to be used in the Decryption CryptoStream transform function 
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption.

                    int bufferSize = buffer.Length << 0x1;

                    var plainTextBytes = new byte[bufferSize];
                    var ms = new MemoryStream(buffer);
                    int decryptedByteCount;

                    using (var csDecrypt = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        decryptedByteCount = csDecrypt.Read(plainTextBytes, 0, plainTextBytes.Length);
                    }

                    IntPtr unmanagedPointer = Marshal.AllocHGlobal(plainTextBytes.Length);
                    Marshal.Copy(plainTextBytes, 0, unmanagedPointer, plainTextBytes.Length);
                    // Call unmanaged code
                    Marshal.FreeHGlobal(unmanagedPointer);


                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in EncryptionManager.DecodeString");
                return null;
            }
        }

        #endregion
    }
}