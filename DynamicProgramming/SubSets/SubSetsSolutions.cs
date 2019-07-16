using System;
using System.Collections.Generic;
//http://kalan.rocks/2016/07/30/solving-a-payment-without-change-problem/#more-64
namespace DynamicProgramming.SubSets {
    public class SubSetsSolutions {
        public static List<int> FindRecursive (int[] set, int targetSum, int currentSum, int currentIndex) {
            for (var i = currentIndex; i < set.Length; i++) {
                var newSum = currentSum + set[i];
                if (newSum > targetSum) {
                    continue;
                }

                if (newSum == targetSum) {
                    return new List<int> { set [i] };
                }

                var result = FindRecursive (set, targetSum, newSum, i + 1);
                if (result == null) {
                    continue;
                }

                result.Add (set[i]);
                return result;
            }

            return null;
        }
    }
}