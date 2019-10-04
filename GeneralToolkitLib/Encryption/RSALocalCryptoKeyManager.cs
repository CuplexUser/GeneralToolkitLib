using System.Security.Cryptography;
using GeneralToolkitLib.Encryption.License.StaticData;

namespace GeneralToolkitLib.Encryption
{
    internal static class RsaLocalCryptoKeyManager
    {
        public static RSAParameters GetAssemblyRsaParameters()
        {
            RsaAsymmetricEncryption rsaAsymmetricEncryption = new RsaAsymmetricEncryption();
            RSAParameters rsaParameters = rsaAsymmetricEncryption.ParseRSAKeyInfo(GetLocalKeySetIdentity());

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