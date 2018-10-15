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
        }
    }
}