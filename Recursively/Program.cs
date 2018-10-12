using System;
namespace Recursively {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello World!");
            Console.WriteLine (FabonacciRecurisively (10));
            Console.WriteLine (Fanonacci (10, 0, 1));
            Console.WriteLine (FabonacciTailRecurisively (10, 0, 1));
            int[] source = { 1, 4, 2, 6, 7, 25369, 22, 14, 55, 63, 785, 1532, 3338 };
            Console.WriteLine (Max (source));
            Console.WriteLine (MaxTailRecurisiveliy (source, 0, 1));
        }
        public static int Fanonacci (int n, int acc1, int acc2) {
            while (n != 0) {
                n = n - 1;
                int acc1_old = acc1;
                acc1 = acc2;
                acc2 = acc1_old + acc2;
            }
            return acc1;
        }
        public static int Fibonacci (int n) {
            int acc1 = 0;
            int acc2 = 1;
            while (n != 0) {
                acc2 = acc1 + acc2;
                acc1 = acc2 - acc1;
                n--;
            }
            return acc1;
        }
        public static int FanonacciTailRecurisively2 (int n) {
            int a = 0;
            int b = 1;
            while (n-- != 0) {
                b = a + b;
                a = b - a;
            }
            return a;
        }
        public static int FibonacciTialRecurisively2 (int n, int a, int b) {
            if (n == 0) return a;
            return FibonacciTialRecurisively2 (n - 1, b, a + b);
        }
        public static int FabonacciRecurisively (int n) {
            if (n < 2) return n;
            return FabonacciRecurisively (n - 1) + FabonacciRecurisively (n - 2);
        }
        public static int FabonacciTailRecurisively (int n, int acc1, int acc2) {
            if (n == 0) return acc1;
            return FabonacciTailRecurisively (n - 1, acc2, acc1 + acc2);
        }
        public static int Max (int[] source) {
            int max = source[0];
            for (int i = 1; i < source.Length; i++) {
                if (max < source[i]) max = source[i];
            }
            return max;
        }
        public static int Max2 (int[] source, int max) {
            int i = 1;
            while (i < source.Length) {
                if (max < source[i]) max = source[i];
                i++;
            }
            return max;
        }

        public static int Max3 (int[] source, int max, int i) {
            while (i < source.Length) {
                if (max < source[i]) max = source[i];
                i++;
            }
            return max;
        }

        public static int MaxTailRecurisiveliy (int[] source, int max, int i) {
            if (i >= source.Length) return max;
            if (max < source[i])
                max = source[i];
            return MaxTailRecurisiveliy (source, max, i + 1);
        }
    }
}