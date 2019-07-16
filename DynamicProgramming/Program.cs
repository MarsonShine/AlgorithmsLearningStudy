using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicProgramming {
    /// <summary>
    /// 动态规划核心思想就是把问题分解成多个小问题
    /// </summary>
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello World!");

            // Compare c = new Compare();
            // c.BackTrackingAvoidRepeatCalculate(0, 0);
            // c.DynamicProgramming();
            // c.DynamicProgrammingAdvance();
            var rets = SubSets.SubSetsSolutions.FindRecursive (SubSets.CountGenerator.Create (1000).ToArray (), 1000, 0, 0);
            Console.WriteLine (
                rets?.Select (x => x.ToString ()).Aggregate ((x, y) => x + "," + y) ?? "null");
            Console.ReadLine ();
        }
    }
}