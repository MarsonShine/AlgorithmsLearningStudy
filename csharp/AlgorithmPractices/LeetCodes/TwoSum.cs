namespace AlgorithmPractices.LeetCodes {
    public class TwoSum {
        public int[] _TwoSum(int[] nums, int target) {
            for (int i = 0; i < nums.Length; i++) {
                int k = i + 1;
                while (k <= nums.Length - 1) {
                    if (nums[i] + nums[k] == target)
                        return new [] { i, k };
                    k++;
                }
            }
            return default;
        }
        //排序去重
        public int[] __TwoSum(int[] nums, int target) {
            for (int i = 0; i < nums.Length; i++) {
                if (i == 0 || i > 0 && nums[i] != nums[i - 1]) {
                    int k = i + 1;
                    int l = nums.Length - 1;
                    while (k <= l) {
                        if (nums[i] + nums[k] == target)
                            return new [] { i, k };
                        else {
                            while (k < l && nums[k] == nums[k + 1]) k++;
                            k++;
                        }
                    }
                }
            }
            return default;
        }
    }
}