using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using GeneralToolkitLib.Encryption.License.StaticData;
using GeneralToolkitLib.Utility;

namespace GeneralToolkitLib.Encryption.License
{
    public static class SysInfoManager
    {
        public static SysInfo GetComputerId()
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            SystemInfo.UseProcessorID = true;
            SystemInfo.UseBaseBoardProduct = true;
            SystemInfo.UseBaseBoardManufacturer = true;
            SystemInfo.UseDiskDriveSignature = true;
            SystemInfo.UseVideoControllerCaption = true;
            SystemInfo.UsePhysicalMediaSerialNumber = true;
            SystemInfo.UseBiosVersion = true;
            SystemInfo.UseBiosManufacturer = true;
            SystemInfo.UseWindowsSerialNumber = true;

            string sysInfoTmp = SystemInfo.GetSystemInfo(assemblyName);
            sysInfoTmp = SerialNumbersSettings.ProtectedApplications.SaltData.GeneralToolkit + sysInfoTmp + SerialNumbersSettings.ProtectedApplications.SaltData.GeneralToolkit;
            byte[] sysInfoBytes = Encoding.ASCII.GetBytes(sysInfoTmp);
            byte[] cumputerIdBytes;

            using (var hashAlg = SHA512.Create())
            {
                cumputerIdBytes = hashAlg.ComputeHash(sysInfoBytes);
            }

            byte[] rsaSignedHashBytes = SerialNumberManager.HashAndSignBytes(cumputerIdBytes, RsaLocalCryptoKeyManager.GetAssemblyRsaParameters());

            return new SysInfo(Converters.GeneralConverters.ByteArrayToBase64(cumputerIdBytes), Converters.GeneralConverters.ByteArrayToBase64(rsaSignedHashBytes));
        }
    }

    [Serializable]
    public class SysInfo
    {
        private string _computerId;
        private string _signedHash;

        protected SysInfo()
        {
        }

        public SysInfo(string computerId, string signedHash)
        {
            this.ComputerId = computerId;
            this.SignedHash = signedHash;
        }

        public string ComputerId
        {
            get { return this._computerId; }
            private set { this._computerId = value; }
        }

        public string SignedHash
        {
            get { return this._signedHash; }
            private set { this._signedHash = value; }
        }
    }
}