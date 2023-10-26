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
        /// <summary>
        /// 优化2：针对浮点数的计算，如果像 DPOptimistise 进位变为整数，那么在哪怕是线性的时间复杂度也会因为进位，进行倍数级的循环计算
        /// 价格越大，耗时越长，性能越低。
        /// 这个方法尝试将浮点数一分为二，整数级与小数级；如 12000.45，两位小数，那么最高数字即是 99，而整数部分则跟之前的整数动态规划求解一样
        /// 额外在计算小数部分的值
        /// 用二维数组来标记状态转移函数
        /// </summary>
        /// <param name="set"></param>
        /// <param name="expectedAmount"></param>
        /// <returns></returns>
        public static List<OrdAmount> DPOptimistise2(OrdAmount[] set, decimal expectedAmount) {
            bool[, ] states = new bool[(int) Math.Ceiling(expectedAmount) + 1, 100];
            states[0, 0] = true;
            if (set[0].Amount <= expectedAmount) {
                states[set[0].CeilingAmount, set[0].DecimalsAmount] = true;
            }
            //整数部分
            int expectedIntAmount = (int) Math.Ceiling(expectedAmount);
            //小数部分
            int expectedDecimalsIntAmount = (int) ((expectedAmount - expectedIntAmount) * 100);
            for (int i = 0; i < set.Length; i++) {
                for (int j = expectedIntAmount - set[i].CeilingAmount; j >= 0; --j) {
                    //用整数以及小数运算
                    int restDecimalsIntAmount = expectedDecimalsIntAmount - set[i].DecimalsAmount;
                    if (restDecimalsIntAmount > 0) { //小数相减大于0 说明
                        expectedDecimalsIntAmount = restDecimalsIntAmount;
                    } else if (restDecimalsIntAmount < 0) { //小于0 整数借1 
                        expectedDecimalsIntAmount = 100 - restDecimalsIntAmount;
                        j = j - 1;
                    } else {
                        expectedDecimalsIntAmount = restDecimalsIntAmount;
                        if (states[j, set[i].DecimalsAmount] == true) states[j + set[i].CeilingAmount, set[i].DecimalsAmount] = true;
                    }
                }
                var result = new List<OrdAmount>();
                var q = (int) Math.Ceiling(expectedAmount);
                // for (var j = i; j >= 0; j--)
                //     for (; q > 0;) {
                //         if (states[q] == false) continue;
                //         var s = set[i];
                //         result.Add(s);
                //         q -= set[i].VirAmount;
                //         break;
                //     }
                return result;
            }
            return null;
        }

        public static void TailRecursive(List<OrdAmount> included, List<OrdAmount> notIncluded, List<OrdAmount> expected, decimal expectedAmount, decimal currentSum, int startIndex) {
            for (var i = startIndex; i < notIncluded.Count; i++) {
                if (expected.Count > 0) break;
                var nextValue = notIncluded[i];
                if (currentSum + nextValue.Amount == expectedAmount) {
                    included.Add(nextValue);
                    expected.AddRange(included);
                    break;
                } else if (currentSum + nextValue.Amount < expectedAmount) {
                    included.Add(nextValue);
                    notIncluded.Remove(nextValue);
                    TailRecursive(included, notIncluded, expected, expectedAmount, currentSum + nextValue.Amount, i);
                }
            }
        }
    }
}