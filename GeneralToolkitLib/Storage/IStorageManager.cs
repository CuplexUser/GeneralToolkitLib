using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeneralToolkitLib.Storage
{
    using Models;

    public interface IStorageManager
    {
        bool SerializeObjectToFile(object obj, string filename, IProgress<StorageManagerProgress> progress);
        Task<bool> SerializeObjectToFileAsync(object obj, string path, IProgress<StorageManagerProgress> progress);
        T DeserializeObjectFromFile<T>(string path, IProgress<StorageManagerProgress> progress);
        Task<T> DeserializeObjectFromFileAsync<T>(string path, IProgress<StorageManagerProgress> progress);
        bool CompressFile(List<string> filesToCompress, string outputFile, IProgress<StorageManagerProgress> progress);
        Task<bool> CompressFileAsync(List<string> filesToCompress, string outputFile, IProgress<StorageManagerProgress> progress);
    }
}