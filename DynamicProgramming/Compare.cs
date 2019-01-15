namespace DynamicProgramming {
    /// <summary>
    /// 比较 01背包问题的回溯算法与动态规划
    /// </summary>
    public class Compare {
        private int _maxWeight = int.MinValue; //存储包中物品的最大值
        private int[] _weightArray = new int[] { 2, 2, 4, 6, 3 }; //物品重量
        private int _count = 5; //物品个数
        private int _maxWeightInPackage = 9; //背包承受的最大重量
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">物品个数</param>
        /// <param name="cw">当前物品的重量</param>
        public void BackTracking(int i, int cw) {
            if (cw == _maxWeightInPackage && i == _count) {
                _maxWeight = cw;
                if (cw > _maxWeight) _maxWeight = cw;
                return;
            }

            BackTracking(i + 1, cw); //不装第i个
            if (cw + _weightArray[i] <= _maxWeightInPackage) {
                BackTracking(i, cw + _weightArray[i]);
            }
        }
        private bool[, ] _states = new bool[5, 10];
        public void BackTrackingAvoidRepeatCalculate(int i, int cw) {
            if (cw == _maxWeightInPackage && i == _count) {
                _maxWeight = cw;
                if (cw > _maxWeight) _maxWeight = cw;
                return;
            }
            if (_states[i, cw] == true) return; //重复计算
            _states[i, cw] = true;
            BackTrackingAvoidRepeatCalculate(i + 1, cw);
            if (cw + _weightArray[i] <= _maxWeightInPackage) {
                BackTrackingAvoidRepeatCalculate(i, cw + _weightArray[i]);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="weightArray">物品重量集合</param>
        /// <param name="count">物品个数</param>
        /// <param name="weightInPackage">背包容纳的最大重量</param>
        public int DynamicProgramming() {
            int[] weightArray = _weightArray;
            int count = _count;
            int weightInPackage = _maxWeightInPackage;
            bool[, ] states = new bool[count, weightInPackage + 1];
            states[0, 0] = true; //哨兵
            states[0, weightArray[0]] = true;
            for (int i = 1; i < count; i++) {
                for (int j = 0; j <= weightInPackage; j++) {
                    if (states[i - 1, j] == true) states[i, j] = states[i - 1, j];
                }
                for (int j = 0; j <= weightInPackage - weightArray[i]; j++) {
                    if (states[i - 1, j] == true) states[i, j + weightArray[i]] = true;
                }
            }
            for (int i = weightInPackage; i >= 0; --i) {
                if (states[count - 1, i] == true) return i;
            }
            return 0;
        }

        public int DynamicProgramming2() {
            int[] weightArray = _weightArray;
            int count = _count;
            int weightInPackage = _maxWeightInPackage;
            bool[] states = new bool[count + 1];
            states[0] = true; //哨兵
            states[weightArray[0]] = true;
            for (int i = 1; i < count; i++) { //动态规划
                for (int j = weightInPackage - weightArray[i]; j >= 0; --j) {
                    if (states[j] == true) states[j + weightArray[i]] = true;
                }
            }
            //取装到最后装的最大的体积，所以要从大到小排序
            for (int i = weightInPackage; i >= 0; i--) {
                if (states[i] == true) return i;
            }
            return 0;
        }
        //引入物品的价值概念
        //在满足不超过背包体积的前提下，价值最大
        private int[] values = new int[5] { 3, 4, 8, 9, 6 };
        private int _maxValue = int.MinValue;
        public void BackTrackingAdvance(int i, int cw, int cv) {
            if (i == _count && cw == _maxWeightInPackage) {
                if (cv > _maxValue) _maxValue = cv;
                return;
            }
            BackTrackingAdvance(i + 1, cw, cv);
            if (cw + _weightArray[i] <= _maxWeightInPackage) {
                BackTrackingAdvance(i, cw + _weightArray[i], cv + values[i]);
            }
        }
        public int DynamicProgrammingAdvance() {
            int[, ] states = new int[_count, _maxWeightInPackage + 1];
            //init
            for (int i = 0; i < _count; i++) {
                for (int j = 0; j < _maxWeightInPackage + 1; j++) {
                    states[i, j] = -1;
                }
            }
            states[0, 0] = 0; //哨兵
            states[0, _weightArray[0]] = values[0];
            for (int i = 1; i < _count; i++) {
                for (int j = 0; j <= _maxWeightInPackage; j++) { //不选择第i个物品
                    if (states[i - 1, j] >= 0) states[i, j] = states[i - 1, j];
                }
                for (int j = 0; j <= _maxWeightInPackage - _weightArray[i]; ++j) {
                    if (states[i - 1, j] >= 0) {
                        int value = states[i - 1, j] + values[i];
                        if (value > states[i, j + _weightArray[i]])
                            states[i, j + _weightArray[i]] = value;
                    }
                }
            }
            //找出最大值
            int maxValue = 0;
            for (int i = 0; i <= _maxWeightInPackage; i++) {
                if (states[_count - 1, i] > maxValue) maxValue = states[_count - 1, i];
            }
            return maxValue;
        }
    }
}