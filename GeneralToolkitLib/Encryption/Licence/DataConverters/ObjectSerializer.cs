using System;
using System.IO;
using GeneralToolkitLib.Encryption.Licence.DataModels;
using Serilog;

namespace GeneralToolkitLib.Encryption.Licence.DataConverters
{
    public static class ObjectSerializer
    {
        public static LicenceDataModel DeserializeLicenceDataFromString(string licenceDataBase64)
        {
            if (licenceDataBase64 == null)
                return null;
            try
            {
                byte[] data = Convert.FromBase64String(licenceDataBase64);
                return DeserializeLicenceData(data);
            }
            catch (Exception ex)
            {
               Log.Error(ex,"DeserializeLicenceDataFromString()");
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

        private static LicenceDataModel DeserializeLicenceData(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            return ProtoBuf.Serializer.Deserialize<LicenceDataModel>(ms);
        }
    }
}