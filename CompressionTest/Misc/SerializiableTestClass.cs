using System;
using System.Runtime.Serialization;

namespace CompressionTest.Misc
{
    [Serializable]
    [DataContract(Name = "SerializiableTestClass")]
    public class SerializiableTestClass
    {
        [DataMember(Name = "DataBytes", Order = 1)]
        public byte[] DataBytes { get; set; }

        [DataMember(Name = "Id", Order =2)]
        public int Id { get; set; }

        [DataMember(Name = "Name", Order = 3)]
        public string Name { get; set; }

        [DataMember(Name = "Guid", Order = 4)]
        public string Guid { get; set; }
    }
}
