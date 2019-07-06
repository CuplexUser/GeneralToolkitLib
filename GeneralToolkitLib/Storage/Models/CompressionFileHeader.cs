using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeneralToolkitLib.Storage.Models
{
    public class CompressionFileHeader
    {
        private static readonly byte[] HeaderIdentifierBytes = {0xE7, 0x0F, 0x5E, 0x0D, 0x4B, 0xD9, 0x55, 0x34, 0x52, 0xFD, 0x5E, 0xC4, 0x40, 0x26, 0x9E, 0xBE, 0xC5, 0x43, 0xB9, 0xD1, 0x39, 0x5C, 0x08, 0x43, 0x96, 0xE5, 0x8B, 0x0E, 0x98, 0xF5, 0x0F, 0x3E};
        private static readonly byte[] HeaderEndBytes = {0x2E, 0xA9, 0x9C, 0x59, 0x80, 0x67, 0xEB, 0x33, 0x8C, 0xDD, 0x7C, 0xF1, 0x0E, 0xF8, 0xB0, 0x67, 0x6C, 0xFA, 0xB7, 0x7F, 0x9B, 0x90, 0xF4, 0xFC, 0x02, 0x7E, 0x5E, 0xCD, 0x87, 0xCF, 0xFA, 0x80};
        private const int HeaderPropertiesAllocSize = 0x14;

        public long DecompressedFileSize { get; private set; }
        public int NumberOfBlocks { get; private set; }
        public int FileHeaderSize { get; private set; }
        public int BlockSize { get; private set; }

        public List<CompressionBlock> CompressedDataBlocks { get; private set; }

        public CompressionFileHeader(long fileSize, int blockSize)
        {
            CompressedDataBlocks = new List<CompressionBlock>();

            NumberOfBlocks = (int) Math.Ceiling(Convert.ToDouble(fileSize) / Convert.ToDouble(blockSize));
            BlockSize = blockSize;
            DecompressedFileSize = fileSize;
            FileHeaderSize = (NumberOfBlocks * CompressionBlock.BlockAllocSize) + HeaderIdentifierBytes.Length + HeaderEndBytes.Length + HeaderPropertiesAllocSize;
        }

        public byte[] ToBytes()
        {
            MemoryStream msStream = new MemoryStream();

            // Write file header identifier
            msStream.Write(HeaderIdentifierBytes, 0, HeaderIdentifierBytes.Length);

            // Write file header size
            byte[] bufferBytes = BitConverter.GetBytes(FileHeaderSize);
            msStream.Write(bufferBytes, 0, bufferBytes.Length);

            // Write decompressed file size
            bufferBytes = BitConverter.GetBytes(DecompressedFileSize);
            msStream.Write(bufferBytes, 0, bufferBytes.Length);

            // Write block size
            bufferBytes = BitConverter.GetBytes(BlockSize);
            msStream.Write(bufferBytes, 0, bufferBytes.Length);

            // Write block count
            bufferBytes = BitConverter.GetBytes(NumberOfBlocks);
            msStream.Write(bufferBytes, 0, bufferBytes.Length);

            foreach (CompressionBlock compressionBlock in CompressedDataBlocks)
            {
                // Write start position
                bufferBytes = BitConverter.GetBytes(compressionBlock.StartPosition);
                msStream.Write(bufferBytes, 0, bufferBytes.Length);

                // Write end position
                bufferBytes = BitConverter.GetBytes(compressionBlock.EndPosition);
                msStream.Write(bufferBytes, 0, bufferBytes.Length);

                // Write decompressed block size
                bufferBytes = BitConverter.GetBytes(compressionBlock.UncompressedBlockSize);
                msStream.Write(bufferBytes, 0, bufferBytes.Length);

                // Write compressed block size
                bufferBytes = BitConverter.GetBytes(compressionBlock.CompressedBlockSize);
                msStream.Write(bufferBytes, 0, bufferBytes.Length);
            }

            // Write end of file header
            msStream.Write(HeaderEndBytes, 0, HeaderEndBytes.Length);

            return msStream.ToArray();
        }

        public static bool VerifyFileHeader(Stream inputStream)
        {
            if (inputStream.Length < HeaderIdentifierBytes.LongLength)
                return false;

            byte[] inputHeaderBytes = new byte[HeaderIdentifierBytes.Length];
            inputStream.Read(inputHeaderBytes, 0, inputHeaderBytes.Length);
            inputStream.Position = 0;

            return HeaderIdentifierBytes.SequenceEqual(inputHeaderBytes);
        }

        public static CompressionFileHeader DecodeHeader(Stream inputStream)
        {
            inputStream.Position = 0;
            if (!VerifyFileHeader(inputStream))
                throw new Exception("Incorrect file header");

            inputStream.Position = HeaderIdentifierBytes.Length;
            byte[] buffer = new byte[8];

            // Read file header size
            inputStream.Read(buffer, 0, 4);
            int fileHeaderSize = BitConverter.ToInt32(buffer, 0);

            // Read decompressed file size
            inputStream.Read(buffer, 0, 8);
            long decompressedFileSize = BitConverter.ToInt64(buffer, 0);

            // Read block size
            inputStream.Read(buffer, 0, 4);
            int blockSize = BitConverter.ToInt32(buffer, 0);

            // Read block count
            inputStream.Read(buffer, 0, 4);
            int numberOfBlocks = BitConverter.ToInt32(buffer, 0);

            CompressionFileHeader compressionFileHeader = new CompressionFileHeader(decompressedFileSize, blockSize) {NumberOfBlocks = numberOfBlocks};

            if (fileHeaderSize != compressionFileHeader.FileHeaderSize)
                throw new Exception("Incorrect file header size");

            for (int i = 0; i < compressionFileHeader.NumberOfBlocks; i++)
            {
                CompressionBlock compressionBlock = new CompressionBlock();

                // Read start position
                inputStream.Read(buffer, 0, 8);
                compressionBlock.StartPosition = BitConverter.ToInt64(buffer, 0);

                // Read end position
                inputStream.Read(buffer, 0, 8);
                compressionBlock.EndPosition = BitConverter.ToInt64(buffer, 0);

                // Read decompressed block size
                inputStream.Read(buffer, 0, 4);
                compressionBlock.UncompressedBlockSize = BitConverter.ToInt32(buffer, 0);

                // Read compressed block size
                inputStream.Read(buffer, 0, 4);
                compressionBlock.CompressedBlockSize = BitConverter.ToInt32(buffer, 0);

                compressionFileHeader.CompressedDataBlocks.Add(compressionBlock);
            }

            return compressionFileHeader;
        }
    }

    public class CompressionBlock
    {
        public const int BlockAllocSize = 0x18;

        public long StartPosition { get; set; }
        public long EndPosition { get; set; }
        public int UncompressedBlockSize { get; set; }
        public int CompressedBlockSize { get; set; }
    }
}