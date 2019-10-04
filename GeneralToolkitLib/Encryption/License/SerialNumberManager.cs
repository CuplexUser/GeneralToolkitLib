using System;
using System.Security.Cryptography;
using GeneralToolkitLib.Converters;
using GeneralToolkitLib.Encryption.License.DataConverters;
using GeneralToolkitLib.Encryption.License.DataModels;
using GeneralToolkitLib.Encryption.License.StaticData;

namespace GeneralToolkitLib.Encryption.License
{
    public class SerialNumberManager
    {
        private const string KeyFormat = "XXXX-XXXX-XXXX-XXXX-XXXX-XXXX-XXXX"; //28 - 34
        private const int ValidationHashLength = 512;
        private readonly SerialNumbersSettings.ProtectedApp _app;
        private readonly RSAParameters _rsaPubKey;
        private LicenseDataModel _licenseData;

        public LicenseDataModel LicenseData
        {
            get { return _licenseData; }
            set { _licenseData = value; }
        }

        public SerialNumberManager(RSAParameters pubRsaKey, SerialNumbersSettings.ProtectedApp app)
        {
            _app = app;
            _rsaPubKey = pubRsaKey;
            _licenseData = new LicenseDataModel();
        }

        public bool ValidateRegistrationData()
        {
            if (string.IsNullOrEmpty(_licenseData?.RegistrationKey) || _licenseData.ValidationHash == null || _licenseData.ValidationHash.Length != ValidationHashLength)
                return false;

            byte[] licenseDataBytes = ObjectSerializer.SerializeDataContract(LicenseData.RegistrationData);
            return VerifySignedHash(licenseDataBytes, _licenseData.ValidationHash, _rsaPubKey);
        }

        public bool VerifyRegistrationKey(string registrationKey)
        {
            if (registrationKey == null || registrationKey.Length != KeyFormat.Length)
                return false;

            byte[] licenseDataBytes = ObjectSerializer.SerializeDataContract(LicenseData.RegistrationData);
            return registrationKey == CreateRegistrationKey(licenseDataBytes);
        }

        public string GenerateRegistrationKey(RegistrationDataModel registrationData)
        {
            byte[] licenseDataBytes = ObjectSerializer.SerializeDataContract(registrationData);
            return CreateRegistrationKey(licenseDataBytes);
        }

        #region Conversion Methods

        private string CreateRegistrationKey(byte[] licenseBytes)
        {
            string regKeyData = GeneralConverters.ByteArrayToBase64(licenseBytes);
            regKeyData = SerialNumbersSettings.ProtectedApplications.SaltData.GeneralToolkit + regKeyData + SerialNumbersSettings.ProtectedApplications.SaltData.GeneralToolkit + _app;
            byte[] buffer = null;
            EncryptionManager.EncodeString(ref buffer, regKeyData, "064lMPnLyjI6sqfXm5KhSE4R0FDU0AchClDyxpAWKkJgFyih59IkhX598sveO7vdbuEKgbEQjDRcLtx0FbcJtASEqHZE8bLX2CIq2LwYZC4OWZGWzx7dv0dxp1h6dcck");
            return ConvertToBase32SerialNumber(buffer);
        }

        private string ConvertToBase32SerialNumber(byte[] hashBytes)
        {
            byte[] halfHashBytes = SHA256.Create().ComputeHash(hashBytes);
            string base32Str = Base32.ToBase32String(halfHashBytes);

            string key = "";
            for (int i = 0; i < base32Str.Length / 2; i += 4)
                key += base32Str.Substring(i, 4) + "-";

            return key.TrimEnd('-');
        }

        #endregion

        #region RSA Hash Methods

        public byte[] HashAndSign(byte[] encrypted, RSAParameters rsaPrivateParams)
        {
            RSACryptoServiceProvider rsaCsp = new RSACryptoServiceProvider(4096);
            var hashAlgorithm = SHA256.Create();

            rsaCsp.ImportParameters(rsaPrivateParams);

            byte[] hashedData = hashAlgorithm.ComputeHash(encrypted);
            return rsaCsp.SignHash(hashedData, CryptoConfig.MapNameToOID("SHA256"));
        }

        public static byte[] HashAndSignBytes(byte[] dataToSign, RSAParameters key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the  
                // key from RSAParameters.  
                RSACryptoServiceProvider rsAalg = new RSACryptoServiceProvider(4096);
                rsAalg.ImportParameters(key);

                // Hash and sign the data. Pass a new instance of SHA1CryptoServiceProvider 
                // to specify the use of SHA1 for hashing. 
                return rsAalg.SignData(dataToSign, new SHA256CryptoServiceProvider());
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        private static bool VerifySignedHash(byte[] dataToVerify, byte[] signedData, RSAParameters key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the  
                // key from RSAParameters.
                RSACryptoServiceProvider rsAalg = new RSACryptoServiceProvider();

                rsAalg.ImportParameters(key);

                // Verify the data using the signature.  Pass a new instance of SHA1CryptoServiceProvider 
                // to specify the use of SHA1 for hashing. 
                return rsAalg.VerifyData(dataToVerify, new SHA256CryptoServiceProvider(), signedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        #endregion
    }
}