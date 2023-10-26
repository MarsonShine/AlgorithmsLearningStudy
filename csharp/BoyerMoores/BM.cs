using System;

namespace BoyerMoores {
    /// <summary>
    /// BM查找字符串算法
    /// </summary>
    public class BM {
        private const int SIZE = 256;
        /// <summary>
        /// 生成一个坏字符串散列表
        /// </summary>
        /// <param name="matchString">模式串</param>
        /// <param name="bc">散列表</param>
        private void GenerateBadCharHashTable(string matchString, int[] bc) {
            for (int i = 0; i < SIZE; i++) {
                bc[i] = -1; //初始化散列表
            }
            for (int i = 0; i < matchString.Length; i++) {
                int ascii = (int) matchString[i];
                bc[ascii] = i; //出现的字符转成对应的ASCII码作为下标记录在模式串出现的位置
            }
        }
        /// <summary>
        /// BM主算法
        /// </summary>
        /// <param name="mainString">主串</param>
        /// <param name="matchString">模式串</param>
        public int BoyerMoore(string mainString, string matchString) {
            int[] bc = new int[SIZE]; //记录模式串中每个字符最后出现的位置
            GenerateBadCharHashTable(matchString, bc); //构建坏字符串哈希表
            int[] suffix = new int[matchString.Length];
            bool[] prefix = new bool[matchString.Length];
            GenerateGC(mainString, matchString, suffix, prefix);
            int i = 0; //表示主串与模式串对齐的第一个字符
            while (i <= mainString.Length - matchString.Length) {
                int j;
                for (j = matchString.Length - 1; j >= 0; j--) { //模式串从后往前匹配
                    if (mainString[i + j] != matchString[j]) break; //坏字符串对应模式串中的下标是 j
                }
                if (j < 0)
                    return i;
                //将模式串后移动j-bc[(int)a[i+j]]位
                int x = i + (j - bc[(int) mainString[i + j]]);

                int y = 0;
                if (y < matchString.Length - 1) {
                    //如果有好后缀的话
                    y = MoveByGS(j, matchString.Length, suffix, prefix);
                }
                i = i + Math.Max(x, y);
            }
            return -1;
        }
        //j 表示坏字符对应的模式串中的字符下标 ; m 表示模式串长度
        private int MoveByGS(int j, int mainStringLength, int[] suffix, bool[] prefix) {
            int k = mainStringLength - 1 - j; //好后缀长度
            if (suffix[k] != -1) return j - suffix[k] + 1;
            for (int r = j + 2; r <= mainStringLength - 1; ++r) {
                if (prefix[mainStringLength - r] == true)
                    return r;
            }
            return mainStringLength;
        }

        private void GenerateGC(string mainString, string matchString, int[] suffix, bool[] prefix) {
            for (int i = 0; i < mainString.Length; i++) {
                //初始化
                suffix[i] = -1;
                prefix[i] = false;
            }
            for (int i = 0; i < mainString.Length - 1; ++i) {
                int j = i;
                int k = 0; //公共后缀子串长度
                while (j >= 0 && mainString[j] == mainString[mainString.Length - 1 - k]) {
                    --j;
                    ++k;
                    suffix[k] = j + 1; //j+1 表示公共后缀子串在 b[0,i] 中的起始下标
                }
                if (j == -1) prefix[k] = true; //如果公共后缀也是模式串中的前缀子串
            }
        }
    }
}