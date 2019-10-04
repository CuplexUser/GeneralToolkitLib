using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using GeneralToolkitLib.Encryption.License.DataConverters;
using GeneralToolkitLib.Encryption.License.DataModels;
using GeneralToolkitLib.Encryption.License.StaticData;
using Serilog;

namespace GeneralToolkitLib.Encryption.License
{
    public class LicenseService
    {
        private static LicenseService _instance;
        private SerialNumberManager _serialNumberManager;
        private const int MaxFileSize = 4096;
        private LicenseDataModel _licenseData;
        private readonly LicenseServiceState _serviceState;
        private bool _initializing;
        public event EventHandler OnInitComplete;

        public bool Initialized
        {
            get { return !_initializing; }
        }

        public bool ValidLicense
        {
            get
            {
                if (!_serviceState.Validated)
                    this.ValidateLicense();

                return _serviceState.Valid;
            }
        }

        public string RegistrationKey
        {
            get
            {
                if (!_initializing && _licenseData != null)
                    return _licenseData.RegistrationKey;
                return null;
            }
        }

        public LicenseDataModel LicenseData => _licenseData;

        private LicenseService()
        {
            _serviceState = new LicenseServiceState();
            LoadSystemInfo();
        }

        public void Init(SerialNumbersSettings.ProtectedApp app)
        {
            string pubKey;
            switch (app)
            {
                case SerialNumbersSettings.ProtectedApp.SecureMemo:
                    pubKey = SerialNumbersSettings.ProtectedApplications.PublicKeys.SecureMemo;
                    break;
                case SerialNumbersSettings.ProtectedApp.SearchForDuplicates:
                    pubKey = SerialNumbersSettings.ProtectedApplications.PublicKeys.SearchForDuplicates;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("app");
            }


            RsaAsymmetricEncryption rsaAsymmetricEncryption = new RsaAsymmetricEncryption();
            RSAKeySetIdentity rsaPublicKeySetIdentity = new RSAKeySetIdentity("", pubKey);
            RSAParameters rsaPublicKey = rsaAsymmetricEncryption.ParseRSAPublicKeyOnlyInfo(rsaPublicKeySetIdentity);
            _serialNumberManager = new SerialNumberManager(rsaPublicKey, app);
        }

        private void LoadSystemInfo()
        {
            if (_initializing || _serviceState.SystemInfo != null)
                return;

            _initializing = true;
            var t = new Task(() => _serviceState.SystemInfo = SysInfoManager.GetComputerId());
            t.GetAwaiter().OnCompleted(InitCompleted);
            t.Start();
        }

        private void InitCompleted()
        {
            _initializing = false;
            OnInitComplete?.Invoke(this, new EventArgs());
        }

        public void ValidateLicense()
        {
            if (_serialNumberManager == null)
                return;

            _serialNumberManager.LicenseData = _licenseData;
            _serviceState.Validated = true;
            _serviceState.Valid = _serialNumberManager.ValidateRegistrationData();
        }

        public SerialNumberManager GetSerialNumberManager()
        {
            return _serialNumberManager;
        }

        public bool LoadLicenseFromFile(string filename)
        {
            FileStream fs = null;
            try
            {
                if (!File.Exists(filename))
                    return false;

                fs = File.OpenRead(filename);
                if (fs.Length > MaxFileSize)
                    throw new Exception("Invalid length of license file");

                TextReader tr = new StreamReader(fs);
                string licenseBase64 = tr.ReadToEnd();
                fs.Close();

                _licenseData = ObjectSerializer.DeserializeLicenseDataFromString(licenseBase64);

                _serviceState.Valid = false;
                _serviceState.Validated = false;

                return _licenseData != null;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "LoadLicenseFromFile");
            }
            finally
            {
                fs?.Close();
            }

            return false;
        }

        public static LicenseService Instance => _instance ?? (_instance = new LicenseService());

        internal class LicenseServiceState
        {
            public bool Validated;
            public bool Valid;
            public SysInfo SystemInfo;
        }
    }
}