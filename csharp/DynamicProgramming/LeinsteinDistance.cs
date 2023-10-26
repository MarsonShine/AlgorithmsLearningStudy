using System;

namespace DynamicProgramming {
    /// <summary>
    /// 莱恩斯坦距离，值越大说明两个字符串差异越大
    /// </summary>
    public class LeinsteinDistance {
        public int LDistance(string main, string target) {
            var n = main.Length;
            int m = target.Length;
            int[, ] minDistance = new int[n, m];
            //初始化
            for (int i = 0; i < n; i++) {
                if (main[i] == target[0]) minDistance[i, 0] = i;
                else if (i != 0) minDistance[i, 0] = minDistance[i - 1, 0] + 1;
                else minDistance[i, 0] = 1;
            }
            for (int j = 0; j < m; ++j) {
                if (target[j] == main[0]) minDistance[0, j] = j;
                else if (j != 0) minDistance[0, j] = minDistance[0, j - 1] + 1;
                else minDistance[0, j] = 1;
            }
            for (int i = 1; i < n; ++i) {
                for (int j = 1; j < m; ++j) {
                    if (main[i] == target[j]) minDistance[i, j] = MinDistance(minDistance[i - 1, j] + 1, minDistance[i, j - 1] + 1, minDistance[i - 1, j - 1]);
                    else minDistance[i, j] = MinDistance(minDistance[i - 1, j] + 1, minDistance[i, j - 1] + 1, minDistance[i - 1, j - 1] + 1);
                }
            }
            return minDistance[n - 1, m - 1];
        }

        private int MinDistance(int x, int y, int z) {
            int minValue = int.MinValue;
            if (x < minValue) minValue = x;
            if (y < minValue) minValue = y;
            if (z < minValue) minValue = z;
            return minValue;
        }
    }
}