using System;

namespace Searchs {
    public class BinarySearch {
        /// <summary>
        /// 二分法查询给定值
        /// </summary>
        /// <param name="array">源数组</param>
        /// <param name="targetValue">要查找的目标值</param>
        /// <returns></returns>
        public int Search(int[] array, int targetValue) {
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

        public int SearchRecursively(int[] array, int targetValue) =>
            SearchRecursivelyInternal(array, 0, array[array.Length - 1], targetValue);

        private int SearchRecursivelyInternal(int[] array, int low, int high, int targetValue) {
            if (low > high) return -1;
            int mid = low + ((high - low) >> 1);
            if (array[mid] == targetValue) {
                return mid;
            } else if (array[mid] < targetValue) {
                low = mid + 1;
                return SearchRecursivelyInternal(array, low, high, targetValue);
            } else {
                high = mid - 1;
                return SearchRecursivelyInternal(array, low, high, targetValue);
            }
        }

        //变体1:查找第一个值等于给定值的元素
        public int BinarySearch_Complex_One(int[] array, int targetValue) {
            int low = 0;
            int high = array.Length - 1;
            while (low <= high) {
                int mid = low + ((high - low) >> 1);
                if (array[mid] > targetValue) {
                    high = mid - 1;
                } else if (array[mid] < targetValue) {
                    low = mid + 1;
                } else {
                    if (mid == 0 || array[mid - 1] != targetValue) return mid;
                    else high = mid - 1;
                }
            }
            return -1;
        }

        //变体2: 查找最后一个值等于给定值的元素
        public int BinarySearch_Complex_Two_Search_Last_GreaterTargetValue(int[] array, int targetValue) {
            int low = 0;
            int high = array.Length - 1;
            while (low <= high) {
                int mid = low + ((high - low) >> 1);
                if (array[mid] > targetValue) {
                    high = mid - 1;
                } else if (array[mid] < targetValue) {
                    low = mid + 1;
                } else {
                    if (mid == array.Length - 1 || array[mid + 1] != targetValue) return mid;
                    else low = mid + 1;
                }
            }
            return -1;
        }

        //变体3:查找第一个值大于等于给定值的元素
        public int BinarySearch_Complex_Three_Search_First_GreaterAndEqualTargetValue(int[] array, int targetValue) {
            int low = 0;
            int high = array.Length - 1;
            while (low <= high) {
                int mid = low + ((high - low) >> 1);
                if (array[mid] < targetValue) {
                    low = mid + 1;
                } else {
                    if (mid == 0 || array[mid - 1] != targetValue) return mid;
                    else high = mid - 1;
                }
            }
            return -1;
        }

        //变体4:查找最后一个值小于等于给定值的元素
        public int BinarySearch_Complex_Four_Search_Last_LessAndEqualTargetValue(int[] array, int targetValue) {
            int low = 0;
            int high = array.Length - 1;
            while (low <= high) {
                int mid = low + ((high - low) >> 1);
                if (array[mid] > targetValue) {
                    high = mid - 1;
                } else {
                    if (mid == array.Length - 1 || array[mid + 1] != targetValue) return mid;
                    else low = mid + 1;
                }
            }
            return -1;
        }
    }
}