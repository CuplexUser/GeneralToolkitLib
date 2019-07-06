using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GeneralToolkitLib.Converters;
using ProtoBuf;

namespace GeneralToolkitLib.Utility.DataConverters
{
    public class ObjectSerializer<T>
    {
        public T DeserializeObjectFromByteArray(byte[] byteData, bool useProtobuffer)
        {
            return useProtobuffer ? this.DeserializeProtoBufferObjectDataInternal(byteData) : this.DeserializeBinaryFormatterObjectDataInternal(byteData);
        }

        public T DeserializeObjectFromString(string data, StringSerializationFormat format, bool useProtobuffer)
        {
            byte[] byteData = null;
            switch (format)
            {
                case StringSerializationFormat.Base64:
                    byteData = Convert.FromBase64String(data);
                    break;
                case StringSerializationFormat.Hex:
                    byteData = GeneralConverters.HexStringToByteArray(data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("format");
            }

            return useProtobuffer ? this.DeserializeProtoBufferObjectDataInternal(byteData) : this.DeserializeBinaryFormatterObjectDataInternal(byteData);
        }

        public byte[] SerializeToByteArray(T obj)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(obj.GetType());
            bool protoBufferCompatible = attrs.OfType<DataContractAttribute>().Any();

            if (protoBufferCompatible)
                return SerializeUsingProtoBuffersInternal(obj);
            else
                return SerializeUsingBinaryFormatterInternal(obj);
        }

        #region Private Byte Array layer

        private byte[] SerializeUsingProtoBuffersInternal(T serializableObject)
        {
            MemoryStream ms = new MemoryStream();
            Serializer.NonGeneric.Serialize(ms, serializableObject);
            return ms.ToArray();
        }

        private byte[] SerializeUsingBinaryFormatterInternal(T serializableObject)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(ms, serializableObject);
            return ms.ToArray();
        }

        private T DeserializeProtoBufferObjectDataInternal(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            return Serializer.Deserialize<T>(ms);
        }

        private T DeserializeBinaryFormatterObjectDataInternal(byte[] data)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(data);

            return (T) binaryFormatter.Deserialize(ms);
        }

        #endregion

        public enum StringSerializationFormat
        {
            Base64,
            Hex,
        }
    }
}