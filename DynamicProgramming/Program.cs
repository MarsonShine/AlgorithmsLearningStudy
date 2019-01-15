using System;

namespace DynamicProgramming {
    /// <summary>
    /// 动态规划核心思想就是把问题分解成多个小问题
    /// </summary>
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            Compare c = new Compare();
            // c.BackTrackingAvoidRepeatCalculate(0, 0);
            // c.DynamicProgramming();
            c.DynamicProgrammingAdvance();
            Console.ReadLine();
        }
    }
}