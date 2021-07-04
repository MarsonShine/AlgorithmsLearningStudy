using System;

namespace Searchs {
    public class RabinKarp {
        private readonly string pad; // 模式串,仅供拉斯维加斯算法使用
        private readonly long RM; // R^(M-1)
        private long patHash; // 模式串的哈希散列值
        private readonly int M; // 字符串长度
        private readonly long Q; // 一个很大的素数
        private readonly int R = 256; // 字母表的大小
        public RabinKarp(string str) {
            pad = str;
            M = str.Length;
            // 随机去一个尽量大的数
            // Q = longRandomPrime();
            Q = 997;
            RM = 1;
            for (int i = 0; i < M - 1; i++) { // 计算 R^(M-1)%Q
                RM = (R * RM) % Q; // 用于减去第一个数字时的计算
            }
            // 预处理
            patHash = hash(pad, M);
        }

        public bool check(int i) => true;

        public int Search(string txt) {
            // 在文本中查询相等的散列值
            int n = txt.Length;
            long txtHash = hash(txt, M);
            if (patHash == txtHash && check(0))
                return 0; // 一开始就匹配成功
            for (int i = M; i < n; i++) {
                // 减去第一个数字，再加上最后一个数字
                // 检查是否匹配
                txtHash = (txtHash + Q - RM * txt[i - M] % Q) % Q;
                txtHash = (txtHash * R + txt[i]) % Q;
                if (patHash == txtHash) {
                    if (check(i - M + 1)) return i - M + 1; // 找到匹配
                }
            }
            return n;
        }

        private long hash(string pad, int l) {
            // 计算key[0...L-1] 的散列值
            long h = 0;
            for (int i = 0; i < l; i++) {
                h = (R * h + pad[i]) % Q;
            }
            return h;
        }

        private static long longRandomPrime() {
            var rd = new Random();
            return rd.Next(3000, 99999);
        }
    }
}