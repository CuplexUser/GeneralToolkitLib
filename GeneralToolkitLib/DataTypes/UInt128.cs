using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;

namespace GeneralToolkitLib.DataTypes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1), Serializable]
    public struct UInt128 : IComparable<UInt128>, IEquatable<UInt128>, IFormattable
    {
        public const int SizeOf = 16;
        public static readonly UInt128 MinValue = 0;
        public static readonly UInt128 MaxValue = new UInt128(ulong.MaxValue, ulong.MaxValue);
        public static readonly UInt128 Zero = 0;
        public static readonly UInt128 One = 1;

        private readonly ulong _leastSignificant;
        private readonly ulong _mostSignificant;

        public UInt128(byte[] Uint128ByteArray, bool isBigendianByteArray)
        {
            if (isBigendianByteArray && BitConverter.IsLittleEndian)
            {
                byte[] mostSignificantByteArray = new byte[8];
                byte[] leastSignificantByteArray = new byte[8];

                Array.Copy(Uint128ByteArray, 0, mostSignificantByteArray, 0, 8);
                Array.Copy(Uint128ByteArray, 8, leastSignificantByteArray, 0, 8);

                Array.Reverse(mostSignificantByteArray);
                Array.Reverse(leastSignificantByteArray);

                _leastSignificant = BitConverter.ToUInt64(leastSignificantByteArray, 0);
                _mostSignificant = BitConverter.ToUInt64(mostSignificantByteArray, 0);
            }
            else
            {
                _mostSignificant = BitConverter.ToUInt64(Uint128ByteArray, 0);
                _leastSignificant = BitConverter.ToUInt64(Uint128ByteArray, 8);
            }
        }

        public UInt128(ulong mostSignificant, ulong leastSignificant)
        {
            _mostSignificant = mostSignificant;
            _leastSignificant = leastSignificant;
        }

        public UInt128(BigInteger value) : this((ulong) (value >> 64), (ulong) (value & ulong.MaxValue))
        {
        }

        public static explicit operator UInt128(BigInteger value)
        {
            return new UInt128(value);
        }

        public static implicit operator BigInteger(UInt128 value)
        {
            return value.ToBigInteger();
        }

        public static implicit operator UInt128(ulong value)
        {
            return new UInt128(0, value);
        }

        public static explicit operator ulong(UInt128 value)
        {
            return value._leastSignificant;
        }

        public static UInt128 Parse(string value, NumberStyles style, IFormatProvider provider)
        {
            BigInteger bigIntegerValue = BigInteger.Parse(value, style, provider);
            if (bigIntegerValue < 0 || bigIntegerValue > MaxValue)
                throw new OverflowException("Value was either too large or too small for an UInt128.");

            return (UInt128) bigIntegerValue;
        }

        public static UInt128 Parse(string value, IFormatProvider provider)
        {
            return Parse(value, NumberStyles.Integer, provider);
        }

        public static UInt128 Parse(string value, NumberStyles style)
        {
            return Parse(value, style, CultureInfo.CurrentCulture);
        }

        public static UInt128 Parse(string value)
        {
            return Parse(value, NumberStyles.Integer, CultureInfo.CurrentCulture);
        }

        public static bool TryParse(string value, NumberStyles style, IFormatProvider provider, out UInt128 result)
        {
            bool success = BigInteger.TryParse(value, style, provider, out var bigIntegerValue);
            if (success && (bigIntegerValue < 0 || bigIntegerValue > MaxValue))
            {
                result = Zero;
                return false;
            }
            result = (UInt128) bigIntegerValue;
            return success;
        }

        public static bool TryParse(string value, out UInt128 result)
        {
            return TryParse(value, NumberStyles.Integer, CultureInfo.CurrentCulture, out result);
        }

        public bool Equals(UInt128 other)
        {
            return _mostSignificant == other._mostSignificant && _leastSignificant == other._leastSignificant;
        }

        public override bool Equals(object obj)
        {
            return (obj is UInt128) && Equals((UInt128) obj);
        }

        public int CompareTo(UInt128 other)
        {
            if (_mostSignificant != other._mostSignificant)
                return _mostSignificant.CompareTo(other._mostSignificant);
            return _leastSignificant.CompareTo(other._leastSignificant);
        }

        public static bool operator ==(UInt128 value1, UInt128 value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(UInt128 value1, UInt128 value2)
        {
            return !(value1 == value2);
        }

        public static bool operator <(UInt128 value1, UInt128 value2)
        {
            return value1.CompareTo(value2) < 0;
        }

        public static bool operator >(UInt128 value1, UInt128 value2)
        {
            return value1.CompareTo(value2) > 0;
        }

        public static bool operator <=(UInt128 value1, UInt128 value2)
        {
            return value1.CompareTo(value2) <= 0;
        }

        public static bool operator >=(UInt128 value1, UInt128 value2)
        {
            return value1.CompareTo(value2) >= 0;
        }

        public static UInt128 operator >>(UInt128 value, int numberOfBits)
        {
            return RightShift(value, numberOfBits);
        }

        public static UInt128 operator <<(UInt128 value, int numberOfBits)
        {
            return LeftShift(value, numberOfBits);
        }

        public static UInt128 RightShift(UInt128 value, int numberOfBits)
        {
            if (numberOfBits >= 128)
                return Zero;
            if (numberOfBits >= 64)
                return new UInt128(0, value._mostSignificant >> (numberOfBits - 64));
            if (numberOfBits == 0)
                return value;
            return new UInt128(value._mostSignificant >> numberOfBits, (value._leastSignificant >> numberOfBits) + (value._mostSignificant << (64 - numberOfBits)));
        }

        public static UInt128 LeftShift(UInt128 value, int numberOfBits)
        {
            numberOfBits %= 128;
            if (numberOfBits >= 64)
                return new UInt128(value._leastSignificant << (numberOfBits - 64), 0);
            if (numberOfBits == 0)
                return value;
            return new UInt128((value._mostSignificant << numberOfBits) + (value._leastSignificant >> (64 - numberOfBits)), value._leastSignificant << numberOfBits);
        }

        public static UInt128 operator &(UInt128 value1, UInt128 value2)
        {
            return BitwiseAnd(value1, value2);
        }

        public static UInt128 BitwiseAnd(UInt128 value1, UInt128 value2)
        {
            return new UInt128(value1._mostSignificant & value2._mostSignificant, value1._leastSignificant & value2._leastSignificant);
        }

        public static UInt128 operator +(UInt128 value1, UInt128 value2)
        {
            return Add(value1, value2);
        }

        public static UInt128 Add(UInt128 value1, UInt128 value2)
        {
            ulong leastSignificant = value1._leastSignificant + value2._leastSignificant;
            bool overflow = (leastSignificant < Math.Max(value1._leastSignificant, value2._leastSignificant));
            return new UInt128(value1._mostSignificant + value2._mostSignificant + (ulong) (overflow ? 1 : 0), leastSignificant);
        }

        public static UInt128 operator -(UInt128 value1, UInt128 value2)
        {
            return Subtract(value1, value2);
        }

        public static UInt128 Subtract(UInt128 value1, UInt128 value2)
        {
            ulong leastSignificant = value1._leastSignificant - value2._leastSignificant;
            bool overflow = (leastSignificant > value1._leastSignificant);
            return new UInt128(value1._mostSignificant - value2._mostSignificant - (ulong) (overflow ? 1 : 0), leastSignificant);
        }

        public override int GetHashCode()
        {
            return Sequence.GetHashCode(_mostSignificant, _leastSignificant);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return (_mostSignificant).ToString(format, formatProvider) + (_leastSignificant).ToString(format, formatProvider);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(IFormatProvider provider)
        {
            return ToString("G", provider);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        private BigInteger ToBigInteger()
        {
            BigInteger value = _mostSignificant;
            value <<= 64;
            value += _leastSignificant;
            return value;
        }
    }
}