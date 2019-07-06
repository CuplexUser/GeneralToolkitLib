namespace GeneralToolkitLib.Storage.Models
{
    public sealed class StorageManagerSettings
    {
        public int NumberOfThreads { get; set; }
        public bool UseMultithreading { get; set; }
        public bool UseEncryption { get; set; }
        public string Password { get; set; }

        public StorageManagerSettings()
        {
            NumberOfThreads = 1;
        }

        public StorageManagerSettings(bool useMultithreading, int numberOfThreads, bool useEncryption, string password)
        {
            UseMultithreading = useMultithreading;
            NumberOfThreads = numberOfThreads;
            UseEncryption = useEncryption;
            Password = password;
        }

        public static StorageManagerSettings GetDefaultSettings()
        {
            StorageManagerSettings settings = new StorageManagerSettings
            {
                NumberOfThreads = 1,
                UseEncryption = false,
                Password = null,
                UseMultithreading = false,
            };

            return settings;
        }
    }
}