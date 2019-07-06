using System;
using GeneralToolkitLib.Encryption.License.DataConverters;
using GeneralToolkitLib.Encryption.License.DataModels;

namespace GeneralToolkitLib.Encryption.License
{
    public class RegistrationDataManager
    {
        private readonly RegistrationDataModel _registrationData;

        public RegistrationDataModel RegistrationData => _registrationData;

        private RegistrationDataManager(RegistrationDataModel registrationData)
        {
            _registrationData = registrationData;
        }

        public static RegistrationDataManager Create(RegistrationDataModel registrationData)
        {
            return new RegistrationDataManager(registrationData);
        }

        public string SerializeToString()
        {
            byte[] data = ObjectSerializer.SerializeDataContract(_registrationData);
            return Convert.ToBase64String(data);
        }
    }
}