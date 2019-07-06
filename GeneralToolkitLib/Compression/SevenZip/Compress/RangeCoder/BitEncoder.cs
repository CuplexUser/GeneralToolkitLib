using System.Diagnostics.CodeAnalysis;

namespace GeneralToolkitLib.Compression.SevenZip.Compress.RangeCoder
{
    internal struct BitEncoder
    {
        public const int kNumBitModelTotalBits = 11;
        public const uint kBitModelTotal = (1 << kNumBitModelTotalBits);
        private const int kNumMoveBits = 5;
        private const int kNumMoveReducingBits = 2;
        public const int kNumBitPriceShiftBits = 6;
        private static readonly uint[] ProbPrices = new uint[kBitModelTotal >> kNumMoveReducingBits];
        private uint Prob;

        [SuppressMessage("Microsoft.Usage", "CA2207:InitializeValueTypeStaticFieldsInline")]
        static BitEncoder()
        {
            const int kNumBits = (kNumBitModelTotalBits - kNumMoveReducingBits);
            for (int i = kNumBits - 1; i >= 0; i--)
            {
                uint start = (uint) 1 << (kNumBits - i - 1);
                uint end = (uint) 1 << (kNumBits - i);
                for (uint j = start; j < end; j++)
                {
                    ProbPrices[j] = ((uint) i << kNumBitPriceShiftBits) + (((end - j) << kNumBitPriceShiftBits) >> (kNumBits - i - 1));
                }
            }
        }

        public void Init()
        {
            Prob = kBitModelTotal >> 1;
        }

        public void UpdateModel(uint symbol)
        {
            if (symbol == 0)
                Prob += (kBitModelTotal - Prob) >> kNumMoveBits;
            else
                Prob -= (Prob) >> kNumMoveBits;
        }

        public void Encode(Encoder encoder, uint symbol)
        {
            // encoder.EncodeBit(Prob, kNumBitModelTotalBits, symbol);
            // UpdateModel(symbol);
            uint newBound = (encoder.Range >> kNumBitModelTotalBits) * Prob;
            if (symbol == 0)
            {
                encoder.Range = newBound;
                Prob += (kBitModelTotal - Prob) >> kNumMoveBits;
            }
            else
            {
                encoder.Low += newBound;
                encoder.Range -= newBound;
                Prob -= (Prob) >> kNumMoveBits;
            }
            if (encoder.Range < Encoder.kTopValue)
            {
                encoder.Range <<= 8;
                encoder.ShiftLow();
            }
        }

        public uint GetPrice(uint symbol)
        {
            return ProbPrices[(((Prob - symbol) ^ ((-(int) symbol))) & (kBitModelTotal - 1)) >> kNumMoveReducingBits];
        }

        public uint GetPrice0()
        {
            return ProbPrices[Prob >> kNumMoveReducingBits];
        }

        public uint GetPrice1()
        {
            return ProbPrices[(kBitModelTotal - Prob) >> kNumMoveReducingBits];
        }
    }
}