using System;
using System.IO;

namespace GeneralToolkitLib.Utility.Serialization
{
    public class GenericObjectSerializer<T>
    {
        public T DeserializeObject(byte[] byteData, string stringData, SerializationFormat serializationFormat)
        {
            byte[] dataBuffer = null;
            switch (serializationFormat)
            {
                case SerializationFormat.Binary:
                    return this.DeserializeObjectDataInternal(byteData);
                case SerializationFormat.Base64:
                    dataBuffer = Convert.FromBase64String(stringData);
                    break;
                case SerializationFormat.Hex:
                    dataBuffer = Converters.GeneralConverters.HexStringToByteArray(stringData);
                    break;
            }

            return this.DeserializeObjectDataInternal(dataBuffer);
        }

        #region Private Byte Array layer

        private byte[] SerializeObjectInternal(T serializableObject)
        {
            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.NonGeneric.Serialize(ms, serializableObject);
            return ms.ToArray();
        }

        private T DeserializeObjectDataInternal(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            return ProtoBuf.Serializer.Deserialize<T>(ms);
        }

        #endregion

        public enum SerializationFormat
        {
            Binary,
            Base64,
            Hex,
        }
    }
}
