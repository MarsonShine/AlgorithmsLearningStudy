namespace BackTrackings {
    //0-1背包问题
    public class ZeroOneKnapsack {
        private int m_maxW = int.MinValue; //存储背包中物品重量的最大值
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">那个物品</param>
        /// <param name="cw">已经装进去的总重量</param>
        /// <param name="items">物品重量集合</param>
        /// <param name="n">物品个数</param>
        /// <param name="w">背包重量</param>
        public void Call(int i, int cw, int[] items, int n, int w) {
            if (cw == w || i == n) {
                if (cw > m_maxW) m_maxW = cw;
                return;
            }
            Call(i + 1, cw, items, n, w); //不装第i个物品的时候
            if (cw + items[i] <= w) {
                Call(i + 1, cw + items[i], items, n, w); //选择装第i个物品
            }
        }
    }
}