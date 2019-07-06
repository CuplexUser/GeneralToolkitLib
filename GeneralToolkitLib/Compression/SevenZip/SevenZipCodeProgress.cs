using System;
using GeneralToolkitLib.Storage;

namespace GeneralToolkitLib.Compression.SevenZip
{
    public class SevenZipCodeProgress : ICodeProgress
    {
        private readonly IProgress<OpenAndSaveFileTaskAsyncExProgress> _progress;
        private readonly CodingOperations _codingOperation;

        public enum CodingOperations
        {
            Encoding,
            Decoding
        }

        public SevenZipCodeProgress(IProgress<OpenAndSaveFileTaskAsyncExProgress> progress, CodingOperations codingOperation)
        {
            _progress = progress;
            _codingOperation = codingOperation;
        }

        public void SetProgress(long inSize, long outSize)
        {
            if(_progress != null)
            {
                int progressPercent = 0;

                if(outSize > 0)
                    progressPercent = Convert.ToInt32((((double)inSize/(double)outSize)*100));

                 string text = this._codingOperation == CodingOperations.Encoding ? "Encoding data" : "Decoding data";
                _progress.Report(new OpenAndSaveFileTaskAsyncExProgress { ProgressPercentage = progressPercent, Text = text });
            }
        }
    }
}
