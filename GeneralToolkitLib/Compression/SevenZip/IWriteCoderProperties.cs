using System.IO;

namespace GeneralToolkitLib.Compression.SevenZip
{
    public interface IWriteCoderProperties
    {
        void WriteCoderProperties(Stream outStream);
    }
}