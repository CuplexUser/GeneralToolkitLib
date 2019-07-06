using System;
using System.IO;
using CompressionTest.Misc;
using GeneralToolkitLib.Utility.DataConverters;
using ManagedLzma.LZMA;
using ManagedLzma.LZMA.Master;
using ManagedLzma.Testing;

namespace CompressionTest
{
    [Serializable]
    public class TestSettings : SharedSettings
    {
        public int Seed;
        public int DatLen;
        public int RunLen;

        public TestSettings() { }
        public TestSettings(TestSettings other)
            : base(other)
        {
            this.Seed = other.Seed;
            this.DatLen = other.DatLen;
            this.RunLen = other.RunLen;
        }
    }

    public class ManagedIncludeDllCompression
    {
        public void TestManagedIncludeDllCompression(string testDataFile, SerializiableTestClass testClass)
        {
            try
            {
                using (FileStream fs = File.Create(testDataFile))
                {
                    var encoder = new LZMA.CLzma2Enc(LZMA.ISzAlloc.SmallAlloc, LZMA.ISzAlloc.BigAlloc);

                    ObjectSerializer<SerializiableTestClass> testClassObjectSerializer = new ObjectSerializer<SerializiableTestClass>();
                    MemoryStream ms = new MemoryStream(testClassObjectSerializer.SerializeToByteArray(testClass));
                    byte[] byteArrayToEncode = ms.ToArray();

                    ms = null;
                    GC.Collect();

                    SharedSettings settings = new SharedSettings
                    {
                        Algo = 2,
                        UseV2 = true,
                        DictSize = 0x2000000,
                        Level = 5,
                        BTMode = 1,
                        NumHashBytes = 4,
                        WriteEndMark = 1,
                        NumThreads = 8,
                        NumTotalThreads = 8,
                        LC = 3,
                        LP = 0,
                        MC = 32,
                        FB = 32,
                        PB = 4,
                        NumBlockThreads = 8,
                        Variant = 1,
                    };

                    settings.Src = new PZ(byteArrayToEncode);
                    settings.Dst = new PZ(new byte[byteArrayToEncode.Length]);
                    LZMA_Compress(settings);

                    fs.Write(settings.Dst.Buffer, 0, settings.WrittenSize);
                    fs.Flush();
                    fs.Close();

                    //LZMA.ISeqOutStream encoderOutputStream= new LZMA.CSeqOutStream();
                    //encoder.Lzma2Enc_Encode(fs, ms, null);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LZMA_Compress(SharedSettings s)
        {
            long s_WrittenSize = s.Dst.Length;

            var props = LZMA.CLzmaEncProps.LzmaEncProps_Init();
            props.mLevel = s.ActualLevel;
            props.mDictSize = (uint)s.ActualDictSize;
            props.mLC = s.ActualLC;
            props.mLP = s.ActualLP;
            props.mPB = s.ActualPB;
            props.mAlgo = s.ActualAlgo;
            props.mFB = s.ActualFB;
            props.mBtMode = s.ActualBTMode;
            props.mNumHashBytes = s.ActualNumHashBytes;
            props.mMC = s.ActualMC;
            props.mWriteEndMark = s.ActualWriteEndMark;
            props.mNumThreads = s.ActualNumThreads;

            var enc = LZMA.LzmaEnc_Create(LZMA.ISzAlloc.BigAlloc);
            var res = enc.LzmaEnc_SetProps(props);
            if (res != LZMA.SZ_OK)
                throw new Exception("SetProps failed: " + res);
            res = enc.LzmaEnc_MemEncode(P.From(s.Dst), ref s_WrittenSize, P.From(s.Src), s.Src.Length,
                s.ActualWriteEndMark != 0, null, LZMA.ISzAlloc.SmallAlloc, LZMA.ISzAlloc.BigAlloc);
            if (res != LZMA.SZ_OK)
                throw new Exception("MemEncode failed: " + res);

            s.Enc = new PZ(new byte[LZMA.LZMA_PROPS_SIZE]);
            long s_Enc_Length = s.Enc.Length;
            res = enc.LzmaEnc_WriteProperties(P.From(s.Enc), ref s_Enc_Length);
            if (res != LZMA.SZ_OK)
                throw new Exception("WriteProperties failed: " + res);
            if (s.Enc.Length != s.Enc.Buffer.Length)
                throw new NotSupportedException();

            enc.LzmaEnc_Destroy(LZMA.ISzAlloc.SmallAlloc, LZMA.ISzAlloc.BigAlloc);
            s.WrittenSize = (int)s_WrittenSize;
            s.Enc.Length = (int)s_Enc_Length;
        }
    }
}
