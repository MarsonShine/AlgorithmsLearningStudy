using System;

namespace Sorts {
    public class QuickSort {
        public void Sort (int[] array) => SortInternal (array, 0, array.Length - 1);
        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="array">待排序数组</param>
        /// <param name="start">数组下标</param>
        /// <param name="end">数组下标</param>
        private void SortInternal (int[] array, int start, int end) {
            if (start >= end) return;
            int q = Partition (array, start, end);
            SortInternal (array, start, q - 1);
            SortInternal (array, q + 1, end);
        }
        /// <summary>
        /// 获取数组分区下标
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int Partition (int[] array, int start, int end) {
            int pivot = array[end];
            int i = start;
            for (int j = start; j < end; ++j) {
                if (array[j] < pivot) {
                    int tmp = array[i];
                    array[i] = array[j];
                    array[j] = tmp;
                    ++i;
                }
            }

            int temp = array[i];
            array[i] = array[end];
            array[end] = temp;
            return i;
        }
    }
}