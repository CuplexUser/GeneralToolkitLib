using System;

namespace GeneralToolkitLib.Hashing
{
    [Serializable]
    public class ChecksumProgress
    {
        public int TotalProgress { get; set; }
        public int FilesTotal { get; set; }
        public int FilesCompleted { get; set; }
        public long DataRead { get; set; }
        public string Text { get; set; }
        public bool Completed { get; set; }
    }
}