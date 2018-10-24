using System;

namespace Searchs {
    public class BinarySearch {
        /// <summary>
        /// 二分法查询给定值
        /// </summary>
        /// <param name="array">源数组</param>
        /// <param name="targetValue">要查找的目标值</param>
        /// <returns></returns>
        public int Search (int[] array, int targetValue) {
            int low = 0;
            int high = array.Length - 1;
            while (low <= high) {
                int mid = (low + high) / 2;
                if (array[mid] == targetValue) {
                    return mid;
                } else if (array[mid] < targetValue) {
                    low = mid + 1;
                } else {
                    high = mid - 1;
                }
            }
            return -1;
        }

        public int SearchRecursively (int[] array, int targetValue) =>
            SearchRecursivelyInternal (array, 0, array[array.Length - 1], targetValue);

        private int SearchRecursivelyInternal (int[] array, int low, int high, int targetValue) {
            if (low > high) return -1;
            int mid = low + ((high - low) >> 1);
            if (array[mid] == targetValue) {
                return mid;
            } else if (array[mid] < targetValue) {
                low = mid + 1;
                return SearchRecursivelyInternal (array, low, high, targetValue);
            } else {
                high = mid - 1;
                return SearchRecursivelyInternal (array, low, high, targetValue);
            }
        }
    }
}