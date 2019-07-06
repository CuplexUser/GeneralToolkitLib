using System;
using GeneralToolkitLib.Encryption.Licence.DataConverters;
using GeneralToolkitLib.Encryption.Licence.DataModels;

namespace GeneralToolkitLib.Encryption.Licence
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