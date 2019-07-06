using System;
using System.Runtime.Serialization;

namespace GeneralToolkitLib.Encryption.License.DataModels
{
    [Serializable]
    [DataContract(Name = "LicenceDataModel")]
    public sealed class LicenceDataModel
    {
        [DataMember(Name = "RegistrationData", Order = 1)]
        public RegistrationDataModel RegistrationData { get; set; }

        [DataMember(Name = "ValidationHash", Order = 2)]
        public byte[] ValidationHash { get; set; }

        [DataMember(Name = "RegistrationKey", Order = 3)]
        public string RegistrationKey { get; set; }
    }
}