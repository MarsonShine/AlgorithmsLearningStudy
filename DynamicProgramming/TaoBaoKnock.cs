using System;

namespace DynamicProgramming {
    /// <summary>
    /// 淘宝凑单问题，如满100减5元活动，在用户选择购物车买单时，会只能提示凑单参与活动
    /// </summary>
    public class TaoBaoKnock {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prices">商品价格</param>
        /// <param name="n">商品个数</param>
        /// <param name="condition">满减条件</param>
        public void DynamicProgrammingKnock(int[] prices, int n, int condition) {
            //设置状态
            bool[, ] states = new bool[n, 2 * condition + 1];
            states[0, 0] = true;
            states[0, prices[0]] = true;
            for (int i = 1; i < n; ++i) {
                for (int j = 0; j <= 2 * condition; ++j) {
                    //不购买第i个商品
                    if (states[i - 1, j] == true) states[i, j] = states[i - 1, j];
                }
                //购买第i个商品
                for (int j = 0; j <= 2 * condition - prices[i]; ++j) {
                    if (states[i - 1, j] == true) states[i, j + prices[i]] = true;
                }
            }

            int j;
            //输出结果大于等于condition的最小值
            for (j = condition; j < 2 + condition + 1; j++) {
                if (states[n - 1, j] == true) break;
            }
            if (j == -1) return; //没有可行解
            //i表示二维数组中的行，j表示列
            for (int i = n - 1; i >= 1; --i) {
                if (j - prices[i] >= 0 && states[i - 1, j - prices[i]] == true) {
                    Console.WriteLine(prices[i] + " "); //购买了这个商品
                    j = j - prices[i];
                } //否则没有购买这个商品，j不变
            }
            if (j != 0) Console.WriteLine(prices[0]);
        }
    }
}