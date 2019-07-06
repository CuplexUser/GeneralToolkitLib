namespace GeneralToolkitLib.Storage.Models
{
    public class StorageManagerProgress
    {
        private int _progressPercentage;

        public int ProgressPercentage
        {
            get { return this._progressPercentage; }
            set
            {
                if (value >= 0 && value <= 100)
                    this._progressPercentage = value;
            }
        }

        public string Text { get; set; }
    }
}