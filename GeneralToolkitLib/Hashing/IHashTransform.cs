using System.IO;

namespace GeneralToolkitLib.Hashing
{
    public interface IHashTransform
    {
        //
        // Summary:
        //     Computes the hash value for the specified System.IO.Stream object.
        //
        // Parameters:
        //   inputStream:
        //     The input to compute the hash code for.
        //
        // Returns:
        //     The computed hash code.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     The object has already been disposed.
        byte[] ComputeHash(Stream inputStream);
        //
        // Summary:
        //     Gets the size, in bits, of the computed hash code.
        //
        // Returns:
        //     The size, in bits, of the computed hash code.
        int HashSize { get; }
    }
}