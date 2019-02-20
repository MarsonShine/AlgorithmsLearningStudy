using System;
using MS.Util.Collections;

namespace Maps {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            PriorityQueue<int> queue = new PriorityQueue<int>(3);
            queue.Push(5);
            queue.Push(2);
            queue.Push(10);

            var t = queue.Pop();
        }
    }
}