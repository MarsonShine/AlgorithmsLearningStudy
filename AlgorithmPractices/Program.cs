using System;
using System.Collections.Generic;
using AlgorithmPractices.Array;

namespace AlgorithmPractices {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            Array_Test();
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

        class Person {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}