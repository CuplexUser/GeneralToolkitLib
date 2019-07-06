using System;
using System.Runtime.Serialization;

namespace GeneralToolkitLib.Encryption.Licence.DataModels
{
    [Serializable]
    [DataContract(Name = "RegistrationDataModel")]
    public sealed class RegistrationDataModel
    {
        [DataMember(Name = "FirstName", Order = 1)]
        public string FirstName { get; set; }

        [DataMember(Name = "LastName", Order = 2)]
        public string LastName { get; set; }

        [DataMember(Name = "Company", Order = 3)]
        public string Company { get; set; }

        [DataMember(Name = "VersionName", Order = 4)]
        public string VersionName { get; set; }

        [DataMember(Name = "ComputerId", Order = 5)]
        public SysInfo ComputerId { get; set; }

        [DataMember(Name = "ValidTo", Order = 6)]
        public DateTime ValidTo { get; set; }

        [DataMember(Name = "Salt", Order = 7)]
        public string Salt { get; set; }
    }
}