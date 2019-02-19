using System;
using Sorts.BubbleSort;

namespace Sorts {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            int[] array = new int[11] { 4, 5, 6, 3, 2, 1, 7, 2, 17, 22, 9 };
            array.Sort();
            Console.WriteLine("时间复杂度：" + BubbleSort.BubbleSort.increaseCounter.GetCount());

            int[] array2 = new int[11] { 4, 5, 6, 3, 2, 1, 10, 25, 66, 18, 55 };
            BubbleSort.BubbleSort.increaseCounter.ClearCount();
            array.SlowerSort();
            Console.WriteLine("SlowerSort 时间复杂度：" + BubbleSort.BubbleSort.increaseCounter.GetCount());

            Console.WriteLine("======插入排序======");
            InsertionSort insertionSort = new InsertionSort();
            int[] array3 = new int[5] { 4, 5, 3, 2, 1 };
            insertionSort.Sort(array3);
            Console.WriteLine(array3.ToString());
            Console.WriteLine("======插入排序======");
            Console.WriteLine("======选择排序======");
            int[] array4 = new int[7] { 10, 3, 5, 7, 2, 4, 6 };
            ShellSort shellSort = new ShellSort();
            shellSort.Sort(array4);
            Console.WriteLine("======选择排序======");
            Console.WriteLine("======归并排序======");
            int[] array5 = new int[] { 2, 3, 1 };
            new MergeSort().Sort(array5);
            Console.Write($"{nameof(array5)} = ");
            Array.ForEach(array5, n => Console.Write(n + ","));
            Console.Write("\r\n");
            Console.WriteLine("======归并排序======");
            Console.WriteLine("======快速排序======");
            int[] array6 = new int[] { 6, 11, 3, 9, 8 };
            new QuickSort().Sort(array6);
            Console.Write($"{nameof(array6)} = ");
            Array.ForEach(array6, n => Console.Write(n + ","));
            Console.Write("\r\n");
            Console.WriteLine("======快速排序======");

            Console.WriteLine("======拓扑排序======");
            TopologySort topology = new TopologySort();
            topology.TopologySortByKahn();
            Console.WriteLine("======拓扑排序======");

            int[] datas = new int[] { 1, 2, 3, 3, 3, 4, 2, 0, 6, 9, 9, 4 };
            int[] values = new int[10]; //max+1;
            for (int i = 0; i < datas.Length; i++) {
                values[datas[i]] = i;
            }
            for (int i = 0; i < values.Length; i++) {
                Console.WriteLine(i + " : " + (values[i]) + " ");
            }
            Console.ReadLine();
        }
    }
}