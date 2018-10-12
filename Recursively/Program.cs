using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Recursively {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello World!");
            Console.WriteLine (FabonacciRecurisively (10));
            Console.WriteLine (Fanonacci (10, 0, 1));
            Console.WriteLine ("FibonacciRecurisivelyAvoidRepeat:" +
                FibonacciRecurisivelyAvoidRepeat (10));
            Console.WriteLine (FabonacciTailRecurisively (10, 0, 1));
            Console.WriteLine ("FibonacciContinuation:" + FibonacciContinuation (10, x => x));
            int[] source = { 1, 4, 2, 6, 7, 25369, 22, 14, 55, 63, 785, 1532, 3338 };
            Console.WriteLine (Max (source));
            Console.WriteLine (MaxTailRecurisiveliy (source, 0, 1));

            Console.WriteLine ("阶乘：" + FactorialGeneral (10));
            Console.WriteLine (FactorialRecurisively (10));
            Console.WriteLine (FactorialTailRecurisively (10, 1, 1));
            Console.WriteLine (FactorialContinuation (10, x => x));

            Console.WriteLine ("---------性能比较---------");
            Stopwatch sw = new Stopwatch ();
            int deep = 40;
            sw.Start ();
            var r = FabonacciRecurisively (deep);
            sw.Stop ();
            Console.WriteLine ("value=" + r + " 递归:" + sw.ElapsedMilliseconds + "毫秒");
            sw.Restart ();
            r = FibonacciRecurisivelyAvoidRepeat (deep);
            sw.Stop ();
            Console.WriteLine ("value=" + r + " 避免重复计算:" + sw.ElapsedMilliseconds + "毫秒");
            sw.Restart ();
            r = Fibonacci (deep);
            sw.Stop ();
            Console.WriteLine ("value=" + r + " 循环体：" + sw.ElapsedMilliseconds + "毫秒");
            sw.Restart ();
            r = FibonacciTialRecurisively2 (deep, 0, 1);
            sw.Stop ();
            Console.WriteLine ("value=" + r + " 尾递归:" + sw.ElapsedMilliseconds + "毫秒");
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

        private static Dictionary<int, int> dic = new Dictionary<int, int> ();
        public static int FibonacciRecurisivelyAvoidRepeat (int n) {
            if (n < 2) return n;
            if (dic.ContainsKey (n)) return dic[n];
            int ret = FabonacciRecurisively (n - 1) + FabonacciRecurisively (n - 2);
            dic.Add (n, ret);
            return ret;
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

        private List<Menu> m_menus;
        public static Menu GetHighestTopParentMenuGeneral (List<Menu> menus, Menu target) {
            if (target.ParentId == 0) return target;
            while (target.ParentId != 0) {
                var middel = menus.FirstOrDefault (m => m.Id == target.ParentId);
                target = middel;
            }
            return target;
        }

        public static Menu GetHighestTopParentMenuRecurisively (List<Menu> source, Menu target) {
            if (target.ParentId == 0) return target;
            return GetHighestTopParentMenuRecurisively (source, source.First (m => m.Id == target.ParentId));
        }

        public static int FactorialRecurisively (int n) {
            if (n == 0) return 1;
            return FactorialRecurisively (n - 1) * n;
        }

        public static int FactorialGeneral (int n) {
            int ret = 1;
            var i = 1;
            if (n == 0) return ret;
            while (i < n) {
                var prev = (i + 1);
                ret = ret * prev;
                i++;
            }
            return ret;
        }

        public static int FactorialTailRecurisively (int n, int ret, int i) {
            if (n == 0) return ret;
            return FactorialTailRecurisively (n - 1, ret * i, i + 1);
        }

        //continuation
        public static int FactorialRecurisively2 (int n) {
            return FactorialContinuation (n - 1, r => n * r);
        }

        public static int FactorialContinuation (int n, Func<int, int> continuation) {
            if (n == 0) return continuation (1);
            return FactorialContinuation (n - 1, r => continuation (n * r));
        }

        public static int FibonacciContinuation (int n, Func<int, int> continuation) {
            if (n < 2) return continuation (n);
            return FibonacciContinuation (n - 1,
                r1 => FibonacciContinuation (n - 2,
                    r2 => continuation (r1 + r2)));
        }
    }
}