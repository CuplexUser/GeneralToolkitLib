using System;
using System.Security;

namespace GeneralToolkitLib.Storage.Models
{
    [SecurityCritical]
    public sealed class StorageManagerSettings
    {
        public int NumberOfThreads { get; set; }

        public bool UseMultithreading { get; set; }

        public bool UseEncryption { get; set; }

        [SecuritySafeCritical] 
        private string _password;

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

        public void SetPassword(string password)
        {
            _password = password;
        }

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

        public static StorageManagerSettings GetDefaultSettings()
        {
            var storageManager= new StorageManagerSettings {
                NumberOfThreads = Environment.ProcessorCount,
                UseEncryption = true,
                UseMultithreading = true,
            };
            storageManager.SetPassword("64AC8DF1-7CBD-4424-BD32-1735A0869F06");

            return storageManager;
        }
    }
}