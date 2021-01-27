using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using GeneralToolkitLib.Compression.SevenZip;
using GeneralToolkitLib.Compression.SevenZip.Compress.LZMA;
using GeneralToolkitLib.Encryption;
using GeneralToolkitLib.Storage.Models;
using ProtoBuf;
using Serilog;

namespace GeneralToolkitLib.Storage
{
    public class StorageManager : StorageManagerBase, IStorageManager
    {
        private const int BlockSize = 0x200000;
        private readonly StorageManagerSettings _settings;
        private static readonly object FileLock = new object();

        public StorageManager(StorageManagerSettings settings)
        {
            _settings = settings;

            if (settings == null)
                throw new ArgumentException("StorageManagerSettings was null");
        }

        public bool SerializeObjectToFile(object obj, string path, IProgress<StorageManagerProgress> progress)
        {
            if (!VerifyObjectToSerialize(obj))
                throw new ArgumentException("serializableObject is not serializable");

            if (_settings.UseEncryption)
            {
                var encryptionManager = new EncryptionManager();
                var ms = SerializeAndCompressObjectToMemoryStream(obj, progress);

                lock (FileLock)
                {
                    return encryptionManager.EncryptAndSaveFile(path, ms, _settings.GetPassword(), null);
                }

            }

            return SerializeAndCompressObjectToFile(obj, path, progress);
        }

        public async Task<bool> SerializeObjectToFileAsync(object obj, string path, IProgress<StorageManagerProgress> progress)
        {
            if (!VerifyObjectToSerialize(obj))
                throw new ArgumentException("serializableObject is not serializable");

            return await Task.Factory.StartNew(() => SerializeObjectToFile(obj, path, progress));
        }

        public T DeserializeObjectFromFile<T>(string path, IProgress<StorageManagerProgress> progress)
        {
            if (_settings.UseEncryption)
                return DeSerializeAndDecompressObjectFromEncryptedFile<T>(path, progress);

            return DeSerializeAndDecompressObjectFromFile<T>(path, progress);
        }

        public async Task<T> DeserializeObjectFromFileAsync<T>(string path, IProgress<StorageManagerProgress> progress)
        {
            return await Task.Factory.StartNew(() => DeserializeObjectFromFile<T>(path, progress));
        }

        public bool CompressFile(List<string> filesToCompress, string outputFile, IProgress<StorageManagerProgress> progress)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CompressFileAsync(List<string> filesToCompress, string outputFile, IProgress<StorageManagerProgress> progress)
        {
            throw new NotImplementedException();
        }

        #region Serialize

        private MemoryStream SerializeAndCompressObjectToMemoryStream(object obj, IProgress<StorageManagerProgress> progress)
        {
            MemoryStream msInput = new MemoryStream();
            MemoryStream msOutput = new MemoryStream();

            var attrs = Attribute.GetCustomAttributes(obj.GetType());
            bool protoBufferCompatible = attrs.OfType<DataContractAttribute>().Any();

            progress?.Report(protoBufferCompatible ? new StorageManagerProgress { ProgressPercentage = 0, Text = "Serializing using Protobuffer" } : new StorageManagerProgress { ProgressPercentage = 0, Text = "Serializing using BinaryFormatter" });

            if (protoBufferCompatible)
                Serializer.NonGeneric.Serialize(msInput, obj);
            else
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(msInput, obj);
            }

            CodeProgressImplementation coderProgress = null;
            if (progress != null)
            {
                coderProgress = new CodeProgressImplementation(progress, CodeProgressImplementation.CodingOperations.Encoding, msInput.Length);
                progress.Report(new StorageManagerProgress { ProgressPercentage = 0, Text = "Starting LZMA encoding of file" });
            }

            msInput.Position = 0;

            if (_settings.UseMultithreading)
                CompressDataMultithreaded(msInput, msOutput, coderProgress);
            else
                CompressData(msInput, msOutput, msInput.Length, coderProgress);

            GC.Collect();
            return msOutput;
        }

        private bool SerializeAndCompressObjectToFile(object obj, string path, IProgress<StorageManagerProgress> progress)
        {
            CodeProgressImplementation coderProgress = null;
            Stream output = null;
            try
            {
                output = new FileStream(path, FileMode.Create);
                MemoryStream input = new MemoryStream();

                var attrs = Attribute.GetCustomAttributes(obj.GetType());
                bool protoBufferCompatible = attrs.OfType<DataContractAttribute>().Any();

                if (progress != null)
                    progress.Report(protoBufferCompatible ? new StorageManagerProgress { ProgressPercentage = 0, Text = "Serializing using Protobuffer" } : new StorageManagerProgress { ProgressPercentage = 0, Text = "Serializing using BinaryFormatter" });

                if (protoBufferCompatible)
                    Serializer.NonGeneric.Serialize(input, obj);
                else
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(input, obj);
                }

                input.Position = 0;

                // Encode the file.
                if (progress != null)
                {
                    coderProgress = new CodeProgressImplementation(progress, CodeProgressImplementation.CodingOperations.Encoding, input.Length);
                    progress.Report(new StorageManagerProgress { ProgressPercentage = 0, Text = "Starting LZMA encoding of file" });
                }

                if (_settings.UseMultithreading)
                    CompressDataMultithreaded(input, output, coderProgress);
                else
                    CompressData(input, output, input.Length, coderProgress);

                output.Flush();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in StorageManager.SerializeAndCompressObjectToFile()");
                return false;
            }
            finally
            {
                output?.Close();
            }
            return true;
        }

        #endregion

        #region Deserialize

        private T DeSerializeAndDecompressObjectFromEncryptedFile<T>(string path, IProgress<StorageManagerProgress> progress)
        {
            EncryptionManager encryptionManager = new EncryptionManager();
            Stream input = null;
            MemoryStream output = new MemoryStream();
            try
            {
                input = encryptionManager.DecryptFileToMemoryStream(path, _settings.GetPassword(), new CryptoProgress(progress));
                input.Position = 0;

                if (_settings.UseMultithreading && CompressionFileHeader.VerifyFileHeader(input))
                    DeflateDataMultithreded(input, output, progress);
                else
                {
                    CodeProgressImplementation coderProgress = new CodeProgressImplementation(progress, CodeProgressImplementation.CodingOperations.Decoding);
                    progress?.Report(new StorageManagerProgress { ProgressPercentage = 0, Text = "Starting LZMA multithreaded decoding of file" });
                    DeflateData(input, output, input.Length, coderProgress).RunSynchronously();
                }

                output.Flush();
                output.Position = 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in StorageManager.DeSerializeAndDecompressObjectFromEncryptedFile()");
            }
            finally
            {
                input?.Close();
            }

            var attrs = Attribute.GetCustomAttributes(typeof(T));
            bool protoBufferCompatible = attrs.OfType<DataContractAttribute>().Any();

            progress?.Report(protoBufferCompatible ? new StorageManagerProgress { ProgressPercentage = 0, Text = "Deserializing using Protobuffer" } : new StorageManagerProgress { ProgressPercentage = 0, Text = "Deserializing using BinaryFormatter" });

            if (protoBufferCompatible)
                return Serializer.Deserialize<T>(output);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            return (T)binaryFormatter.Deserialize(output);
        }

        private T DeSerializeAndDecompressObjectFromFile<T>(string path, IProgress<StorageManagerProgress> progress)
        {
            CodeProgressImplementation coderProgress = null;
            FileStream inputFileStream = null;
            MemoryStream output = new MemoryStream();
            try
            {
                inputFileStream = File.OpenRead(path);

                if (progress != null)
                {
                    coderProgress = new CodeProgressImplementation(progress, CodeProgressImplementation.CodingOperations.Decoding);
                    progress.Report(new StorageManagerProgress { ProgressPercentage = 0, Text = "Starting LZMA decoding of file" });
                }

                if (_settings.UseMultithreading && CompressionFileHeader.VerifyFileHeader(inputFileStream))
                    DeflateDataMultithreded(inputFileStream, output, progress);
                else
                    DeflateData(inputFileStream, output, inputFileStream.Length, coderProgress).RunSynchronously();

                output.Flush();
                output.Position = 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in StorageManager.DeSerializeAndDecompressObjectFromFile()");
            }
            finally
            {
                inputFileStream?.Close();
            }

            T deserializedObj;
            var attrs = Attribute.GetCustomAttributes(typeof(T));
            if (attrs.OfType<DataContractAttribute>().Any())
                deserializedObj = Serializer.Deserialize<T>(output);
            else
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                deserializedObj = (T)binaryFormatter.Deserialize(output);
            }

            output.Dispose();
            GC.Collect();
            return deserializedObj;
        }

        #endregion

        #region Private helper methods

        private void CompressDataMultithreaded(Stream input, Stream output, CodeProgressImplementation coderProgress)
        {
            //Write file header
            CompressionFileHeader compressionFileHeader = new CompressionFileHeader(input.Length, BlockSize);

            int sizeOfHeader = compressionFileHeader.FileHeaderSize;
            output.Position = sizeOfHeader;

            long totalEncodeSize = input.Length;
            long bytesLeft = totalEncodeSize;

            var tasks = new Task[_settings.NumberOfThreads];
            var outMemoryStreams = new MemoryStream[_settings.NumberOfThreads];
            var inputBlockSizeArray = new int[_settings.NumberOfThreads];

            input.Position = 0;
            while (bytesLeft > 0)
            {
                int taskCount = 0;
                for (int i = 0; i < tasks.Length; i++)
                {
                    int encodeSize = Math.Min(BlockSize, (int)bytesLeft);

                    if (encodeSize <= 0)
                        break;

                    taskCount++;
                    var buffer = new byte[encodeSize];
                    int bytesRead = input.Read(buffer, 0, buffer.Length);
                    bytesLeft -= bytesRead;

                    if (bytesRead == 0)
                        break;

                    MemoryStream inputStream = new MemoryStream(buffer);
                    MemoryStream outStream = new MemoryStream();
                    outMemoryStreams[i] = outStream;
                    inputBlockSizeArray[i] = bytesRead;
                    tasks[i] = new Task(() => { CompressData(inputStream, outStream, bytesRead, null); });
                    tasks[i].Start();
                }

                var activeTasks = tasks.Take(taskCount).ToArray();
                Task.WaitAll(activeTasks);

                coderProgress?.SetProgress(totalEncodeSize - bytesLeft, -1);

                for (int i = 0; i < taskCount; i++)
                {
                    CompressionBlock compressionBlock = new CompressionBlock();
                    var outBytes = outMemoryStreams[i].ToArray();
                    outMemoryStreams[i] = null;
                    compressionBlock.CompressedBlockSize = outBytes.Length;
                    compressionBlock.UncompressedBlockSize = inputBlockSizeArray[i];
                    compressionBlock.StartPosition = output.Position;
                    compressionBlock.EndPosition = output.Position + outBytes.Length;

                    output.Write(outBytes, 0, outBytes.Length);

                    compressionFileHeader.CompressedDataBlocks.Add(compressionBlock);
                }
            }

            // Write file header
            output.Position = 0;
            var headerBytes = compressionFileHeader.ToBytes();
            output.Write(headerBytes, 0, headerBytes.Length);
        }

        private void DeflateDataMultithreded(Stream inputDataStream, Stream outputStream, IProgress<StorageManagerProgress> progress)
        {
            CodeProgressImplementation coderProgress = null;

            if (!CompressionFileHeader.VerifyFileHeader(inputDataStream))
                throw new Exception("Invalid file header");

            if (progress != null)
            {
                coderProgress = new CodeProgressImplementation(progress, CodeProgressImplementation.CodingOperations.Decoding);
                progress.Report(new StorageManagerProgress { ProgressPercentage = 0, Text = "Starting LZMA multithreaded decoding of file" });
            }

            CompressionFileHeader compressionFileHeader = CompressionFileHeader.DecodeHeader(inputDataStream);
            inputDataStream.Position = compressionFileHeader.FileHeaderSize;

            int currentBlock = 0;
            var decoderTasks = new Task[_settings.NumberOfThreads];
            var outputMemoryStreams = new MemoryStream[_settings.NumberOfThreads];

            while (currentBlock < compressionFileHeader.NumberOfBlocks)
            {
                int taskCount = 0;
                for (int i = 0; i < _settings.NumberOfThreads; i++)
                {
                    CompressionBlock dataBlock = compressionFileHeader.CompressedDataBlocks[currentBlock];
                    outputMemoryStreams[i] = new MemoryStream();
                    var buffer = new byte[dataBlock.CompressedBlockSize];
                    inputDataStream.Read(buffer, 0, buffer.Length);
                    MemoryStream inputStream = new MemoryStream(buffer) { Position = 0 };
                    decoderTasks[i] = DeflateData(inputStream, outputMemoryStreams[i], dataBlock.CompressedBlockSize, coderProgress);
                    decoderTasks[i].Start();
                    currentBlock++;
                    taskCount++;

                    progress?.Report(new StorageManagerProgress { ProgressPercentage = currentBlock / compressionFileHeader.NumberOfBlocks, Text = "Decoding block " + currentBlock });

                    if (currentBlock == compressionFileHeader.NumberOfBlocks)
                        break;
                }

                Task.WaitAll(decoderTasks.Take(taskCount).ToArray());

                for (int i = 0; i < taskCount; i++)
                {
                    var buffer = outputMemoryStreams[i].ToArray();
                    outputStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        private void CompressData(Stream inputStream, Stream outStream, long inputSize, ICodeProgress progress)
        {
            Encoder coder = new Encoder();

            // Write the encoder properties
            coder.WriteCoderProperties(outStream);

            // Write the decompressed file size.
            outStream.Write(BitConverter.GetBytes(inputSize), 0, 8);

            coder.Code(inputStream, outStream, inputSize, -1, progress);
        }

        private Task DeflateData(Stream inputStream, Stream outStream, long compressedSize, ICodeProgress progress)
        {
            return new Task(() =>
            {
                Decoder decoder = new Decoder();

                // Read the decoder properties
                var properties = new byte[5];
                inputStream.Read(properties, 0, 5);

                // Read in the decompress block size.
                var fileLengthBytes = new byte[8];
                inputStream.Read(fileLengthBytes, 0, 8);
                long blockSize = BitConverter.ToInt64(fileLengthBytes, 0);

                decoder.SetDecoderProperties(properties);

                decoder.Code(inputStream, outStream, compressedSize, blockSize, progress);
            });
        }

        private bool VerifyObjectToSerialize(object obj)
        {
            return obj != null && obj.GetType().Attributes.HasFlag(TypeAttributes.Serializable & TypeAttributes.Public);
        }

        #endregion
    }
}