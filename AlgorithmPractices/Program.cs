using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AlgorithmPractices.Array;
using AlgorithmPractices.LeetCodes;
using AlgorithmPractices.LinkedLists;
using AlgorithmPractices.Queues;
using AlgorithmPractices.Recursions;
using AlgorithmPractices.Sorts;
using AlgorithmPractices.Stacks;

namespace AlgorithmPractices {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            SingleLinkedList<int> list = new SingleLinkedList<int>();
            for (int i = 0; i < 4; i++) {
                list.InsertToTail(i + 1);
            }
            list.Reverse(list.Head);
            Array_Test();

            Merge_LinkedList_Test();
            var q = new Person { Id = 1, Name = "marson" };
            var p = q;
            q = new Person { Id = 2, Name = "shine" };
            Console.WriteLine(ReferenceEquals(p, q));
            Console.WriteLine(q == p);
            Console.WriteLine("q.hashCode=" + q.GetHashCode() + " p.hashCode=" + p.GetHashCode());

            var a = new ThreeSum(new int[] { 0, 1, 2, -1, -4 }).Sum(new int[] { 0, 1, 2, -1, -4 });
            string str = "[";
            foreach (var b in a) {
                str += "[";
                foreach (var c in b) {
                    str += c + ",";
                }
                str += "],";
            }
            str += "]";
            Console.WriteLine(str);

            // new TwoSum()._TwoSum(new [] { 2, 7, 11, 15 }, 9);
            new TwoSum().__TwoSum(new [] { 3, 2, 4 }, 6);
            new _MajorityElement().MajorityElement(new [] { 2, 2, 1, 1, 1, 2, 2 });
            Console.WriteLine(new _FirstMissingPositive().FirstMissingPositive(new [] { 1, 2, 0 }));

            var nodes = new SingleLinkedList<int>();
            var n = new SingleLinkedListNode<int>(0);
            nodes.InsertToHead(new SingleLinkedListNode<int>(3));
            nodes.InsertToTail(new SingleLinkedListNode<int>(2));
            nodes.InsertToTail(n);
            nodes.InsertToTail(new SingleLinkedListNode<int>(-4));
            // var n = new SingleLinkedListNode<int>(1);
            nodes.HasCycle(nodes.Head);

            var sequenceStack = new SequenceStack<string>(15);
            for (int i = 0; i < 25; i++) {
                sequenceStack.Push("数据内容" + (i + 1));
            }
            int count = sequenceStack.Length;
            for (int i = 0; i < count; i++) {
                Console.WriteLine(sequenceStack.Pop());
            }

            var linkedStack = new LinkedStack<string>(15);
            for (int i = 0; i < 15; i++) {
                linkedStack.Push("数据内容" + (i + 1));
            }

            var linkedQueue = new LinkedQueue<string>(5);
            for (int i = 0; i < 5; i++) {
                linkedQueue.Enqueue("数据内容" + i);
                Console.WriteLine($"数据容量 capacity={linkedQueue.Capacity} 数据个数 length={linkedQueue.Length}");
            }
            for (int i = 0; i < 5; i++) {
                linkedQueue.Dequeue();
                Console.WriteLine($"数据容量 capacity={linkedQueue.Capacity} 数据个数 length={linkedQueue.Length}");
            }
            var circle = new CircleQueue<string>(5);
            for (int i = 0; i < 5; i++) {
                circle.Enqueue("数据内容" + i);
                Console.WriteLine($"数据容量 capacity={circle.Capacity} 数据个数 length={circle.Length}");
            }
            for (int i = 0; i < 5; i++) {
                var c = circle.Dequeue();
                Console.WriteLine($"数据容量 capacity={circle.Capacity} 数据个数 length={circle.Length}");
            }

            Demonstrations demonstrations = new Demonstrations();
            Console.WriteLine(demonstrations.FibRecursionTail(8, 0, 1));
            Console.WriteLine(demonstrations.Factorial(8) + " 递归：" + demonstrations.FactorialRecursion(8) + " 尾递归：" + demonstrations.FactorialRecursionTail(8, 1));

            BigSort<int> sort = new BigSort<int>();
            var source = new [] { 1, 2, 3, 6, 2, 4, 8, 0, 11, 2, 3, 6, 3, 1 };
            sort.MergeSort(source);
            Console.WriteLine(source);
            var sources = new [] { 4, 5, 6, 1, 2, 3 };
            // sort.BublleSort(sources);
            // sort.BublleSortOptimize(sources);
            // sort.InsertionSort(sources);
            sort.SelectionSort(new [] { 1, 2, 3, 7, 1, 2, 3, 5, 6, 7, 10, 9, 6, 5, 4, 2 });
        }

        private static void Array_Test() {
            DynamicExpansionArray<Person> array = new DynamicExpansionArray<Person>(3);
            Console.WriteLine($"数组长度：{array.Length}");
            for (int i = 0; i < 3; i++) {
                array.Insert(new Person { Id = i + 1, Name = "name" + i });
            }
            Console.WriteLine($"数组长度：{array.Length} 数组容量：{array.Capacity}");
            array.Insert(new Person { Id = 4, Name = "marson" });
            Console.WriteLine($"数组长度：{array.Length} 数组容量：{array.Capacity}");
            for (int i = 0; i < array.Length; i++) {
                Console.Write("ID=" + array.Values[i].Id + " Name=" + array.Values[i].Name + " ");
            }
            var item = array.Find(new Person { Id = 4, Name = "marson" }, p => p.Name == "marson");
            Console.WriteLine("查询结果：item=" + item.Id + " name=" + item.Name);
            array.Remove();
            for (int i = 0; i < array.Length; i++) {
                Console.Write("ID=" + array.Values[i].Id + " Name=" + array.Values[i].Name + " ");
            }
        }

        private static void Merge_LinkedList_Test() {
            SingleLinkedList<Person> personsA = new SingleLinkedList<Person>(new Person());
            for (int i = 1; i <= 10; i *= 2) {
                personsA.InsertToTail(new Person { Id = i, Name = "name" + i });
            }
            SingleLinkedList<Person> personsB = new SingleLinkedList<Person>(new Person());
            for (int i = 1; i <= 10; i = i * 2 + 1) {
                personsB.InsertToTail(new Person { Id = i, Name = "name" + i });
            }
            SingleLinkedList<Person> merge = new SingleLinkedList<Person>(new Person());
            var t = merge.MergeSortedList(personsA.Head, personsB.Head);
        }

        class Person : IComparer, IComparer<Person> {
            public int Id { get; set; }
            public string Name { get; set; }

            public int Compare(Person x, Person y) {
                return x.Id - y.Id;
            }

            public int Compare(object x, object y) {
                return ((Person) x).Id - ((Person) y).Id;
            }
        }
    }
}