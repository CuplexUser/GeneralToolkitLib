using System;

namespace GeneralToolkitLib.Compression.SevenZip
{
    /// <summary>
    ///     The exception that is thrown when an error in input stream occurs during decoding.
    /// </summary>
    [Serializable]
    internal class DataErrorException : ApplicationException
    {
        public DataErrorException()
            : base("Data Error")
        {
        }
    }
}