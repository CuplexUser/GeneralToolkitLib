using System;
using System.IO;
using GeneralToolkitLib.Encryption.License.DataModels;
using Serilog;

namespace GeneralToolkitLib.Encryption.License.DataConverters
{
    public static class ObjectSerializer
    {
        public static LicenseDataModel DeserializeLicenseDataFromString(string licenseDataBase64)
        {
            if (licenseDataBase64 == null)
                return null;
            try
            {
                byte[] data = Convert.FromBase64String(licenseDataBase64);
                return DeserializeLicenseData(data);
            }
            catch (Exception ex)
            {
                Log.Error(ex,"DeserializeLicenseDataFromString()");
            }
            return null;
        }

        public static RegistrationDataModel DeserializeRegistrationDataFromString(string registrationDataBase64)
        {
            if (registrationDataBase64 == null)
                return null;
            try
            {
                byte[] data = Convert.FromBase64String(registrationDataBase64);
                return DeserializeRegistrationData(data);
            }
            catch (Exception ex)
            {
                Log.Error(ex,"DeserializeRegistrationDataFromString()");
            }
            return null;
        }

        public static byte[] SerializeDataContract(object obj)
        {
            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.NonGeneric.Serialize(ms, obj);
            return ms.ToArray();
        }

        public static RegistrationDataModel DeserializeRegistrationData(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            return ProtoBuf.Serializer.Deserialize<RegistrationDataModel>(ms);
        }

        private static LicenseDataModel DeserializeLicenseData(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            return ProtoBuf.Serializer.Deserialize<LicenseDataModel>(ms);
        }
    }
}