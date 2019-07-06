using System.Security.Cryptography;
using GeneralToolkitLib.Encryption.Licence.StaticData;

namespace GeneralToolkitLib.Encryption
{
    internal static class RSALocalCryptoKeyManager
    {
        public static RSAParameters GetAssemblyRsaParameters()
        {
            RSA_AsymetricEncryption rsaAsymetricEncryption = new RSA_AsymetricEncryption();
            RSAParameters rsaParameters = rsaAsymetricEncryption.ParseRSAKeyInfo(GetLocalKeySetIdentity());

            return rsaParameters;
        }

        private static RSAKeySetIdentity GetLocalKeySetIdentity()
        {
            RSAKeySetIdentity rsaKeySetIdentity = new RSAKeySetIdentity(SerialNumbersSettings.ProtectedApplications.PrivateKeys.GeneralToolkitLib,
                SerialNumbersSettings.ProtectedApplications.PublicKeys.GeneralToolkitLib);
            return rsaKeySetIdentity;
        }
    }
}