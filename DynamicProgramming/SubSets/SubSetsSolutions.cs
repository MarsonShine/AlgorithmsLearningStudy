using System;
using System.Collections.Generic;
//http://kalan.rocks/2016/07/30/solving-a-payment-without-change-problem/#more-64
namespace DynamicProgramming {
    public class SubSetsSolutions {
        public static List<int> FindRecursive(int[] set, int targetSum, int currentSum, int currentIndex) {
            for (var i = currentIndex; i < set.Length; i++) {
                var newSum = currentSum + set[i];
                if (newSum > targetSum) {
                    continue;
                }

                if (newSum == targetSum) {
                    return new List<int> { set [i] };
                }

                var result = FindRecursive(set, targetSum, newSum, i + 1);
                if (result == null) {
                    continue;
                }

                result.Add(set[i]);
                return result;
            }

            return null;
        }
        public static List<int> FindDP(int[] set, int sum) {
            var solution = new bool[set.Length + 1, sum + 1];
            for (var i = 0; i <= set.Length; i++) {
                solution[i, 0] = true;
            }

            for (var i = 1; i <= set.Length; i++) {
                for (var j = 1; j <= sum; j++) {
                    solution[i, j] = solution[i - 1, j];

                    if (!solution[i, j] && j >= set[i - 1]) {
                        solution[i, j] = solution[i, j] || solution[i - 1, j - set[i - 1]];
                    }
                }

                if (!solution[i, sum]) {
                    continue;
                }

                var result = new List<int>();
                var q = sum;
                for (var p = i - 1; p >= 0; p--) {
                    if (solution[p, q]) {
                        continue;
                    }

                    var s = set[p];
                    result.Add(s);
                    q -= s;
                }

                return result;
            }

            return null;
        }
        public static List<OrdAmount> FindRecursive(List<OrdAmount> set, long targetSum, long currentSum, int currentIndex) {
            for (var i = currentIndex; i < set.Count; i++) {
                var newSum = currentSum + (long) (set[i].Amount * 100);
                if (newSum > targetSum) {
                    continue;
                }

                if (newSum == targetSum) {
                    return new List<OrdAmount> { set [i] };
                }

                var result = FindRecursive(set, targetSum, newSum, i + 1);
                if (result == null) {
                    continue;
                }

                result.Add(set[i]);
                return result;
            }

            return null;
        }

        public static List<OrdAmount> DP(OrdAmount[] set, long sum) {
            var solution = new bool[set.Length + 1, sum + 1];
            for (var i = 0; i <= set.Length; i++) {
                solution[i, 0] = true;
            }
            for (var i = 1; i <= set.Length; i++) {
                for (var j = 1; j <= sum; j++) {
                    solution[i, j] = solution[i - 1, j];
                    if (!solution[i, j] && j >= (set[i - 1].Amount * 100)) {
                        solution[i, j] = solution[i, j] || solution[i - 1, j - (long) (set[i - 1].Amount * 100)];
                    }
                }
                if (!solution[i, sum]) {
                    continue;
                }
                var result = new List<OrdAmount>();
                var q = sum;
                for (var p = i - 1; p >= 0; p--) {
                    if (solution[p, q]) continue;
                    var s = set[p];
                    result.Add(s);
                    q -= (long) (s.Amount * 100);
                }
                return result;
            }
            return null;
        }

        public static List<OrdAmount> DPOptimistise(OrdAmount[] set, long sum) {
            bool[] states = new bool[sum + 1];
            states[0] = true;
            if (set[0].VirAmount <= sum) {
                states[set[0].VirAmount] = true;
            }

            for (int i = 0; i < set.Length; i++) {
                for (long j = sum - set[i].VirAmount; j >= 0; --j) {
                    if (states[j] == true) states[j + set[i].VirAmount] = true;
                }
                if (states[sum] == false) continue;
                var result = new List<OrdAmount>();
                var q = sum;
                for (var j = i; j >= 0; j--)
                    for (; q > 0;) {
                        if (states[q] == false) continue;
                        var s = set[i];
                        result.Add(s);
                        q -= set[i].VirAmount;
                        break;
                    }
                return result;
            }
            return null;
        }
    }
}