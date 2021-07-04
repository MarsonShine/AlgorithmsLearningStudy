using System;

namespace Searchs {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            var array = new [] { 2, 2, 2, 3, 6, 8, 9, 9, 10, 11, 19, 22, 31, 36 };
            var binarySearch = new BinarySearch();
            Console.WriteLine("等于11的第一个值是：" + array[binarySearch.BinarySearch_Complex_One(array, 11)]);
            Console.WriteLine("等于11的最后一个值是：" + array[binarySearch.BinarySearch_Complex_Two_Search_Last_GreaterTargetValue(array, 11)]);
            Console.WriteLine("大于等于11的第一个值是：" + array[binarySearch.BinarySearch_Complex_Three_Search_First_GreaterAndEqualTargetValue(array, 11)]);
            Console.WriteLine("小于等于11的最后一个值是：" + array[binarySearch.BinarySearch_Complex_Four_Search_Last_LessAndEqualTargetValue(array, 11)]);
            // Console.ReadLine();

            string str = "3141592653589793";
            string pat = "26535";
            RabinKarp rk = new RabinKarp(pat);
            var idx = rk.Search(str);
            for (int i = 0; i < idx; i++) {
                System.Console.Write(i);
            }
            Console.WriteLine();
            Console.WriteLine(pat);
        }
    }
}