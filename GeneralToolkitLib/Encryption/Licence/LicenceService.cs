using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using GeneralToolkitLib.Encryption.Licence.DataConverters;
using GeneralToolkitLib.Encryption.Licence.DataModels;
using GeneralToolkitLib.Encryption.Licence.StaticData;
using Serilog;

namespace GeneralToolkitLib.Encryption.Licence
{
    public class LicenceService
    {
        private static LicenceService _instance;
        private SerialNumberManager _serialNumberManager;
        private const int MAX_FILE_SIZE = 4096;
        private LicenceDataModel _licenceData;
        private readonly LicenceServiceState _serviceState;
        private bool _initializing;
        public event EventHandler OnInitComplete;

        public bool Initialized
        {
            get { return !_initializing; }
        }

        public bool ValidLicence
        {
            get
            {
                if (!_serviceState.Validated)
                    this.ValidateLicence();

                return _serviceState.Valid;
            }
        }

        public string RegistrationKey
        {
            get
            {
                if (!_initializing && _licenceData != null)
                    return _licenceData.RegistrationKey;
                return null;
            }
        }

        public LicenceDataModel LicenceData => _licenceData;

        private LicenceService()
        {
            _serviceState = new LicenceServiceState();
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


            RSA_AsymetricEncryption rsaAsymetricEncryption = new RSA_AsymetricEncryption();
            RSAKeySetIdentity rsaPublicKeySetIdentity = new RSAKeySetIdentity("", pubKey);
            RSAParameters rsaPublicKey = rsaAsymetricEncryption.ParseRSAPublicKeyOnlyInfo(rsaPublicKeySetIdentity);
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

        public void ValidateLicence()
        {
            if (_serialNumberManager == null)
                return;

            _serialNumberManager.LicenceData = _licenceData;
            _serviceState.Validated = true;
            _serviceState.Valid = _serialNumberManager.ValidateRegistrationData();
        }

        public bool LoadLicenceFromFile(string filename)
        {
            FileStream fs = null;
            try
            {
                if (!File.Exists(filename))
                    return false;

                fs = File.OpenRead(filename);
                if (fs.Length > MAX_FILE_SIZE)
                    throw new Exception("Invalid length of licence file");

                TextReader tr = new StreamReader(fs);
                string licenceBase64 = tr.ReadToEnd();
                fs.Close();

                _licenceData = ObjectSerializer.DeserializeLicenceDataFromString(licenceBase64);

                _serviceState.Valid = false;
                _serviceState.Validated = false;

                return _licenceData != null;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "LoadLicenceFromFile");
            }
            finally
            {
                fs?.Close();
            }

            return false;
        }

        public static LicenceService Instance => _instance ?? (_instance = new LicenceService());

        internal class LicenceServiceState
        {
            public bool Validated;
            public bool Valid;
            public SysInfo SystemInfo;
        }
    }
}