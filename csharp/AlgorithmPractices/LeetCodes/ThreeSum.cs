namespace AlgorithmPractices.LeetCodes {
    using System.Collections.Generic;
    using System.Linq;

    public class ThreeSum {
        private int[] array;
        public ThreeSum(int[] array) {
            this.array = array;
        }
        public IList<IList<int>> Sum(int[] nums) {
            IList<IList<int>> ret = new List<IList<int>>();
            if (nums.Length < 3)
                return ret;
            // if (nums.Length == 3) {
            //     if (nums.Sum() == 0) {
            //         ret.Add(new List<int>(nums));
            //     }
            //     return ret;
            // }
            System.Array.Sort(nums);
            int i = 0;
            for (i = 0; i < nums.Length - 2; i++) {
                if (i == 0 || (i > 0 && nums[i] != nums[i - 1])) {
                    int j = i + 1;
                    int k = nums.Length - 1;
                    while (j < k) {
                        if (-nums[i] == (nums[j] + nums[k])) {
                            ret.Add(new List<int>(new [] { nums[i], nums[j], nums[k] }));
                            while (j < k && nums[j] == nums[j + 1]) j++;
                            while (j < k && nums[k] == nums[k - 1]) k--;
                            k--;
                            j++;
                        } else if (nums[j] + nums[k] < -nums[i]) {
                            while (j < k && nums[j] == nums[j + 1]) {
                                j++;
                            }
                            j++;
                        } else {
                            while (j < k && nums[k] == nums[k - 1]) {
                                k--;
                            }
                            k--;
                        }
                    }
                }
            }
            return ret;
        }
        public List<int[]> Sum() {
            List<int[]> ret = new List<int[]>();
            int i = 0;
            for (i = 0; i < array.Length; i++) {
                int j = i + 1;
                int k = array.Length;
                while (j < k--) {
                    if (-array[i] == (array[j] + array[k])) {
                        ret.Add(new [] { i, j, k });
                    }
                }
            }
            return ret;
        }
    }
}