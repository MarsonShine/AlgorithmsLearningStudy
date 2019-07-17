using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicProgramming {
    /// <summary>
    /// 动态规划核心思想就是把问题分解成多个小问题
    /// </summary>
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            // Compare c = new Compare();
            // c.BackTrackingAvoidRepeatCalculate(0, 0);
            // c.DynamicProgramming();
            // c.DynamicProgrammingAdvance();
            var rets = SubSetsSolutions.FindRecursive(SubSets.CountGenerator.Create(30000).ToArray(), 10000, 0, 0);
            Console.WriteLine(
                rets?.Select(x => x.ToString()).Aggregate((x, y) => x + "," + y) ?? "null");
            var set = new [] { 100, 40, 5, 1, 1, 1, 1 };

            var result = SubSetsSolutions.FindDP(set, 9);
            Console.WriteLine(
                result?.Select(x => x.ToString()).Aggregate((x, y) => x + "," + y) ?? "null");
            var ords = OrdAmountGenerator.Create(1000).ToList();
            long targetSum = (long) (18823.33M * 100);
            // var ordamounts = SubSetsSolutions.FindRecursive(ords, targetSum, 0, 0);
            // var ordamounts = SubSetsSolutions.DP(ords.ToArray(), targetSum);
            var ordamounts = SubSetsSolutions.DPOptimistise(ords.ToArray(), targetSum);
            Console.WriteLine(
                ordamounts?.Select(x => x.Amount.ToString()).Aggregate((x, y) => x + "," + y) ?? "null"
            );
            Console.ReadLine();
        }
    }
}