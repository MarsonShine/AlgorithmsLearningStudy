using System;

namespace SkipLinks {
    class Program {
        static void Main(string[] args) {
            SkipList skipList = new SkipList();
            for (int i = 0; i < 1000; i++) {
                skipList.Add(i);
            }
            var node = skipList.Find(1);
            Console.WriteLine(node.ToString());
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}