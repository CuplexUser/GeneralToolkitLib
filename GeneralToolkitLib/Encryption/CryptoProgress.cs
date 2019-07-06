using System;
using GeneralToolkitLib.Storage;

namespace GeneralToolkitLib.Encryption
{
    using GeneralToolkitLib.Storage.Models;

    public sealed class CryptoProgress : Progress<CryptoProgressHandler>
    {
        private readonly IProgress<StorageManagerProgress> _storageManagerAsyncExProgres;

        public CryptoProgress()
        {
        }

        public CryptoProgress(IProgress<StorageManagerProgress> storageManagerAsyncExProgress)
        {
            _storageManagerAsyncExProgres = storageManagerAsyncExProgress;
        }

        public void Report(CryptoProgressHandler cryptoProgressHandler)
        {
            if (_storageManagerAsyncExProgres != null)
            {
                StorageManagerProgress progress = new StorageManagerProgress();

                if (cryptoProgressHandler.TotalBytes > 0)
                    progress.ProgressPercentage = (int) ((cryptoProgressHandler.EncodedBytes * 100) / cryptoProgressHandler.TotalBytes);

                progress.Text = cryptoProgressHandler.Text;
                _storageManagerAsyncExProgres.Report(progress);
            }
            else
                OnReport(cryptoProgressHandler);
        }
    }

    public class CryptoProgressHandler
    {
        public long EncodedBytes { get; set; }
        public long TotalBytes { get; set; }
        public string Text { get; set; }
    }
}