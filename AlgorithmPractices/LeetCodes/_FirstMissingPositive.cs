namespace AlgorithmPractices.LeetCodes {
    public class _FirstMissingPositive {
        public int FirstMissingPositive(int[] nums) {
            System.Array.Sort(nums);
            int index = 0;
            int[] result = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++) {
                if (nums[i] > 0) {
                    result[index++] = nums[i];
                }
            }
            int prev = 1;
            if (index == 0 || result[0] > 1) return 1;
            for (int i = 0; i < index; i++) {
                if (result[i] > 0) {
                    //连续
                    if (result[i] - prev > 1) {
                        return prev + 1;
                    }
                    prev = result[i];
                }
            }
            return result[index - 1] + 1;
        }
    }
}