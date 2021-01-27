using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace GeneralToolkitLib.Storage.Models
{
    [SecurityCritical]
    public sealed class StorageManagerSettings
    {
        public int NumberOfThreads { get; set; }

        public bool UseMultithreading { get; set; }

        public bool UseEncryption { get; set; }

        [SecuritySafeCritical] private readonly string _password;

        public StorageManagerSettings()
        {
            NumberOfThreads = 1;
        }

        public StorageManagerSettings(bool useMultithreading, int numberOfThreads, bool useEncryption, string password)
        {
            UseMultithreading = useMultithreading;
            NumberOfThreads = numberOfThreads;
            UseEncryption = useEncryption;
            _password = password;
            //SetProtectedPassword(password);
        }

        private StorageManagerSettings(int numberOfThreads, string password)
        {
            NumberOfThreads = numberOfThreads;
            UseMultithreading = numberOfThreads > 1;
            UseEncryption = true;
            _password = password;
            //SetProtectedPassword(password);
        }

        //[SecuritySafeCritical]
        //private void SetProtectedPassword(string password)
        //{
        //    _password = Enc
        //}

        [SecuritySafeCritical]
        public string GetPassword()
        {
            //ProtectedMemory.Unprotect(_passwordBytes, MemoryProtectionScope.SameProcess);
            //string password = Encoding.Default.GetString(_passwordBytes);
            //ProtectedMemory.Protect(_passwordBytes, MemoryProtectionScope.SameProcess);

            return _password;
        }

        public static StorageManagerSettings CreateSettingsWithSecureString(int numberOfThreads, string password)
        {
            var settings = new StorageManagerSettings(numberOfThreads, password);
            return settings;
        }
    }
}