using System;

namespace GeneralToolkitLib.Compression.SevenZip
{
    /// <summary>
    ///     The exception that is thrown when the value of an argument is outside the allowable range.
    /// </summary>
    [Serializable]
    internal class InvalidParamException : ApplicationException
    {
        public InvalidParamException()
            : base("Invalid Parameter")
        {
        }
    }
}