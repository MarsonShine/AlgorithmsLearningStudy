namespace AlgorithmPractices.LeetCodes {
    public class _MajorityElement {
        public int MajorityElement(int[] nums) {
            //排序
            System.Array.Sort(nums);
            for (int i = 0; i < nums.Length; i++) {
                int k = i + 1;
                int count = 1;
                while (k < nums.Length && nums[i] == nums[k]) {
                    count++;
                    i = k++;
                }
                if (count > 1 && count > nums.Length / 2)
                    return nums[i];
            }
            return default;
        }
    }
}