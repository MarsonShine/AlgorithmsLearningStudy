namespace DynamicProgramming {
    public class MaxLcs {
        private readonly string a;
        private readonly string b;
        private readonly int n;
        private readonly int m;
        public MaxLcs(string a, string b) {
            this.a = a;
            this.b = b;
            n = a.Length;
            m = b.Length;
        }

        public int Lcs() {
            int[, ] maxlcs = new int[n, m];
            //初始化
            for (int i = 0; i < n; ++i) {
                if (a[i] == b[0]) maxlcs[i, 0] = 1;
                else if (i != 0) maxlcs[i, 0] = maxlcs[i - 1, 0];
                else maxlcs[i, 0] = 0;
            }
            for (int j = 0; j < m; ++j) {
                if (a[0] == b[j]) maxlcs[0, j] = 1;
                else if (j != 0) maxlcs[0, j] = maxlcs[0, j - 1];
                else maxlcs[0, j] = 0;
            }
            for (int i = 1; i < n; ++i) {
                for (int j = 1; j < m; ++j) {
                    if (a[i] == b[j]) maxlcs[i, j] = Max(maxlcs[i - 1, j], maxlcs[i, j - 1], maxlcs[i - 1, j - 1] + 1);
                    else maxlcs[i, j] = Max(maxlcs[i - 1, j], maxlcs[i, j - 1], maxlcs[i - 1, j - 1]);
                }
            }
            return maxlcs[n - 1, m - 1];
        }

        private int Max(int x, int y, int z) {
            var max = int.MinValue;
            if (x > max) max = x;
            if (y > max) max = y;
            if (z > max) max = z;
            return max;
        }
    }
}