# GeneralToolkitLib

GeneralToolkitLib is primarily a .Net Framework Library combined with 3 test projects. 
* Strictly unit tests
* GUI and with manual algorithms testing used with a smartphone and for instance Google Authenticator or with a transparent OTP generator showing shared key. Including QR Code generator for OTP initialization. 
I have also written an implementation of RFC 4226 Counter-Based One-Time Password Algorithm.
* Managed.Lzma serialization and compression Testing using my own [class ObjectSerializer<T>]. However, the implemented version of LZMA2 data (encoder and decoder) using my own multithreaded implementation which can stream transform from a large set of objects. 1 million+ to raw binary transform which is extremely fast using Google Protobuf-net. From a binary stream you can then execute parallel async stream transform based on available threads into my LZMA2 parallel encoder and finally using a async non parallel AES256 crypto transform.
The compression rate is by default set to maximum and performance is the same or better then 7zipx64 written in C++. Both in terms of time and compression rate using binary data from a file stream or from the ObjectSerializer template class. Including encryption if you have hardware acceleration for AES instructions. My system performs about 12 Gigabyte/s mean encryption/decryption with HW acceleration, which is about 6 times faster than a similar encryption algorithm.

*Incomplete*
