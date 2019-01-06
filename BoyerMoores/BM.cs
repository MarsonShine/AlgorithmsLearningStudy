namespace BoyerMoores {
    /// <summary>
    /// BM查找字符串算法
    /// </summary>
    public class BM {
        private const int SIZE = 256;
        /// <summary>
        /// 生成一个散列表
        /// </summary>
        /// <param name="b">模式串</param>
        /// <param name="bc">散列表</param>
        private void GenerateBC(string b, int[] bc) {
            for (int i = 0; i < SIZE; i++) {
                bc[i] = -1; //初始化散列表
            }
            for (int i = 0; i < b.Length; i++) {
                int ascii = (int) b[i];
                bc[ascii] = i; //出现的字符转成对应的ASCII码作为下标记录在模式串出现的位置
            }
        }
        /// <summary>
        /// BM主算法
        /// </summary>
        /// <param name="a">主串</param>
        /// <param name="b">模式串</param>
        public void BoyerMoore(string a, string b) {
            int[] bc = new int[SIZE]; //记录模式串中每个字符最后出现的位置
            GenerateBC(b, bc); //构建坏字符串哈希表
            int i = 0; //表示主串与模式串对齐的第一个字符
            while (i <= a.Length - b.Length) {
                int j;
                for (int j = b.Length - 1; j >= 0; j--) { //模式串从后往前匹配
                    if (a[i + j] != b[j]) break; //坏字符串对应模式串中的下标是 j
                }
                if (j < 0)
                    return i;
                //将模式串后移动j-bc[(int)a[i+j]]位
                i = i + (j - bc[(int) a[i + j]]);
            }
            return -1;
        }
    }
}