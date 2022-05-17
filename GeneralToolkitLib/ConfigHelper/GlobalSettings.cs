using System;
using System.IO;
using System.Reflection;

namespace GeneralToolkitLib.ConfigHelper
{
    /// <summary>
    /// The Application Global Settings class has to be initializes before it can be used since it is always the calling assembly who has all the necessary data.
    /// </summary>
    public class GlobalSettings
    {
        public bool Initialized { get; private set; }
        public static GlobalSettings Settings
        {
            get
            {
                return Instance;
            }
        }

        public static readonly GlobalSettings Instance= new GlobalSettings();
        private const string UserDbFileName = "ComputedHashData.bin";
        private string _logFileName;
        private string _userDataPath;

        public GlobalSettings()
        {
            Initialized = false;
        }

        public void UnitTestInitialize(string testDataPath)
        {
            if (Initialized)
                return;

            Initialized = true;

            _logFileName = "UnitTest.log";
            _userDataPath = testDataPath;
        }

        public void Initialize(string executableAssemblyName, bool useApplicationDataFolder)
        {
            if (Initialized)
                return;

            Initialized = true;

            _logFileName = executableAssemblyName + ".log";
            if (useApplicationDataFolder)
                _userDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + executableAssemblyName + "\\";
            else
                _userDataPath = GetAssemblyPath(Assembly.GetExecutingAssembly().Location);

            if (!Directory.Exists(_userDataPath))
                Directory.CreateDirectory(_userDataPath);
        }

        public string GetApplicationLogFilePath()
        {
            if (!Initialized)
                throw new InvalidOperationException("GetApplicationLogDirectory() can only be called after initialization");

            return Path.Combine(_userDataPath, _logFileName);
        }

        private string GetAssemblyPath(string fullAssemblyPath)
        {
            if (fullAssemblyPath != null)
            {
                int lastSlash = fullAssemblyPath.LastIndexOf('\\');
                if (lastSlash > 0)
                    return fullAssemblyPath.Substring(0, lastSlash + 1);
            }
            return null;
        }

        public string GetHashtableFilename()
        {
            if (!Initialized)
                throw new InvalidOperationException("GetHashtableFilename() can only be called after initialization");

            return Path.Combine(_userDataPath, UserDbFileName);
        }

        public string GetUserDataDirectoryPath()
        {
            if (!Initialized)
                throw new InvalidOperationException("Default Config is not initialized!");

            return _userDataPath;
        }
    }
}