using System.IO;

namespace GeneralToolkitLib.Compression.SevenZip.Compress.LZ
{
    internal interface IInWindowStream
    {
        void SetStream(Stream inStream);
        void Init();
        void ReleaseStream();
        byte GetIndexByte(int index);
        uint GetMatchLen(int index, uint distance, uint limit);
        uint GetNumAvailableBytes();
    }
}