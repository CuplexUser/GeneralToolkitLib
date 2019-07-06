using System;
using GeneralToolkitLib.Storage.Models;

namespace GeneralToolkitLib.Compression.SevenZip
{
    public class CodeProgressImplementation : ICodeProgress
    {
        private readonly CodingOperations _codingOperation;
        private readonly long _originalSize;
        private readonly IProgress<StorageManagerProgress> _progress;

        public enum CodingOperations
        {
            Encoding,
            Decoding
        }

        public CodeProgressImplementation(IProgress<StorageManagerProgress> progress, CodingOperations codingOperation)
        {
            _progress = progress;
            _codingOperation = codingOperation;
            _originalSize = 0;
        }

        public CodeProgressImplementation(IProgress<StorageManagerProgress> progress, CodingOperations codingOperation, long originalSize)
        {
            _progress = progress;
            _codingOperation = codingOperation;
            _originalSize = originalSize;
        }

        public void SetProgress(long inSize, long outSize)
        {
            if (_progress != null)
            {
                int progressState = 0;
                string text = _codingOperation == CodingOperations.Encoding ? "Encoding file" : "Decoding file";

                if (_codingOperation == CodingOperations.Decoding)
                {
                    if (inSize > 0)
                        progressState = (int) ((outSize * 100) / inSize);
                }
                else if (_originalSize > 0)
                    progressState = (int) ((inSize * 100) / _originalSize);

                _progress.Report(new StorageManagerProgress
                {
                    Text = text,
                    ProgressPercentage = progressState
                });
            }
        }
    }
}