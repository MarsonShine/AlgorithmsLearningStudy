using System;
using Sorts.BubbleSort;

namespace Sorts {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello World!");
            int[] array = new int[11] { 4, 5, 6, 3, 2, 1, 7, 2, 17, 22, 9 };
            array.Sort ();
            Console.WriteLine ("时间复杂度：" + BubbleSort.BubbleSort.increaseCounter.GetCount ());

            int[] array2 = new int[11] { 4, 5, 6, 3, 2, 1, 10, 25, 66, 18, 55 };
            BubbleSort.BubbleSort.increaseCounter.ClearCount ();
            array.SlowerSort ();
            Console.WriteLine ("SlowerSort 时间复杂度：" + BubbleSort.BubbleSort.increaseCounter.GetCount ());

            Console.WriteLine ("======插入排序======");
            InsertionSort insertionSort = new InsertionSort ();
            int[] array3 = new int[5] { 4, 5, 3, 2, 1 };
            insertionSort.Sort (array3);
            Console.WriteLine (array3.ToString ());
            Console.WriteLine ("======插入排序======");
            Console.WriteLine ("======选择排序======");
            int[] array4 = new int[7] { 10, 3, 5, 7, 2, 4, 6 };
            ShellSort shellSort = new ShellSort ();
            shellSort.Sort (array4);
            Console.WriteLine ("======选择排序======");
        }
    }
}