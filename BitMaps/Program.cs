using System;

namespace BitMaps {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            Console.WriteLine("char 类型所占字节：" + sizeof(char));
            Console.WriteLine("bool 类型所占字节：" + sizeof(bool));
            BitMapOfChar bitMapOfChar = new BitMapOfChar(16);
            bitMapOfChar.Set(1);
            bitMapOfChar.Set(10);
            bitMapOfChar.Set(14);
            bitMapOfChar.Set(16);
            bitMapOfChar.Set(20);
        }
    }
}