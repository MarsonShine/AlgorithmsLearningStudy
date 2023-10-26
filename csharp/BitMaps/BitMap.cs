using System;
namespace BitMaps {
    public class BitMap {
        private readonly bool[] bits;
        private readonly int nbit;
        public BitMap(int k) {
            bits = new bool[k];
            nbit = k;
        }
        public void Set(int k) {
            if (k > nbit) return;
            bits[k] = true;
        }
        public bool GetBoolean(int k) {
            if (k > nbit) return false;
            return bits[k];
        }
    }

    public class BitMapOfChar {
        private readonly char[] bytes;
        private readonly int nbit;
        private readonly int bitCount = sizeof(char) * 8;
        public BitMapOfChar(int nbit) {
            this.nbit = nbit;
            bytes = new char[nbit / bitCount + 1];
        }
        public void Set(int k) {
            if (k > nbit) return;
            int byteIndex = k / bitCount;
            int bitIndex = k % bitCount;
            bytes[byteIndex] |= (char) (1 << bitIndex);
        }
        public bool GetBoolean(int k) {
            if (k > nbit) return false;
            int byteIndex = k / bitCount;
            int bitIndex = k % bitCount;
            return (bytes[byteIndex] & (1 << bitIndex)) != 0;
        }
    }
}