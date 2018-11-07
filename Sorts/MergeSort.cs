using System;

namespace Sorts {
    /// <summary>
    /// 归并排序，主要应用分治思想
    /// </summary>
    public class MergeSort {
        public void Sort (int[] array) {
            SortInternal (array, 0, array.Length - 1);
        }
        public void SortInternal (int[] array, int start, int end) {
            if (start < end) {
                int mid = (start + end) / 2;
                SortInternal (array, start, mid); // 0 0
                SortInternal (array, mid + 1, end); // 1 1  //最底层start = 0，mid = 0 end = 1；
                MergeSortSub (array, start, mid, end);
            }
        }
        /// <summary>
        /// 递归调用函数
        /// </summary>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        private void MergeSortSub (int[] array, int start, int mid, int end) {
            int[] temp = new int[end - start + 1];
            int m = start, n = mid + 1, k = 0;
            while (n <= end && m <= mid) {
                if (array[m] <= array[n]) {
                    temp[k++] = array[m++];
                } else {
                    temp[k++] = array[n++];
                }
            }

            int nstart = m;
            int nend = mid;
            if (n <= end) {
                nstart = n;
                nend = end;
            }

            while (nstart <= nend) {
                temp[k++] = array[nstart++];
            }

            //将temp中的数据拷回到array[start,end]
            for (int i = 0; i < end - start; i++) {
                array[start + i] = temp[i];
            }
        }
    }
}