using System;

namespace AlgorithmPractices.LeetCodes {
    /// <summary>
    /// 求 X 的平方根的整数值
    /// </summary>
    public class SqrtX {
        public int Sqrt(int x) {
            if (x <= 0) return 0;
            if (x == 1) return x;
            //根据二分法求得近似值
            return GetApproximate(x);
        }

        private int GetApproximate(int x) {
            //获取位数
            // int p = GetBit(x);
            int start = 0;
            int mid = (x + start) / 2;
            int end = x;
            while (true) {
                if (Math.Pow(mid, 2) < x) {
                    start = mid;
                } else if (Math.Pow(mid, 2) > x) {
                    end = mid;
                } else {
                    return mid;
                }
                mid = (start + end) / 2;
                if (end - start == 1) return start;
            }
        }

        private int GetBit(int x) {
            int p = 1;
            while ((x /= 10) > 0) {
                p++;
            }
            return p;
        }
    }
}