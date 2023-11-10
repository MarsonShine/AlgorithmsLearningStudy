using System;

namespace HashTables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            double key = 0.5;
            int mask = 1023; // 13 位二进制数，可以表示 0 到 1023 之间的整数  
            int hashValue = RealNumberHash(key, mask);
            Console.WriteLine("Hash value: " + hashValue);
            Console.WriteLine("Hornel Hash value: " + HornelHash("marsonshine", mask));
        }

        // 实数散列函数
        public static int BetterHash(double key)
        {
            uint hash = MurmurHash2.Hash(BitConverter.GetBytes(key));
            return (int)hash;
        }

        static int RealNumberHash(double key, int mask)
        {
            // 将实数键转换为二进制数
            long n = BitConverter.DoubleToInt64Bits(key);
            string binaryKey = Convert.ToString(n, toBase: 2);
            int hash = 0;
            foreach (char c in binaryKey)
            {
                hash = (hash * 256 + (c - '0')) % mask;
            }
            return hash;
        }

        static int HornelHash(string input,int mask)
        {
            int hash = 0;
            int length = input.Length;
            for (int i = 0; i < length; i++)
            {
                char c = input[i];
                hash = (hash * 31 + (c - 'a' + 1)) % mask;
            }
            return hash;
        }
    }
}
