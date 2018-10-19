using System;

namespace Sorts {
    /// <summary>
    /// 计数排序，是桶排序的优化算法
    /// </summary>
    public class CountingSort {
        public void Sort (int[] a) => SortInternal (a, a.Length - 1);

        private void SortInternal (int[] a, int n) {
            if (n < 1) return;
            //比较最大数
            int max = a[0];
            for (int i = 1; i < n; i++) {
                if (a[i] > max) max = a[i];
            }
            //利用max初始化数组
            int[] c = new int[max + 1];
            //初始化c数组为0
            for (int i = 0; i < c.Length; i++) {
                c[i] = 0;
            }
            //把a数组中相同的数据次数放到对应的c下标数组中
            for (int i = 0; i < n; i++) {
                c[a[i]]++;
            }
            //重新计数，把c数组对应的值改成小于等于下标的值个数
            for (int i = 1; i < max; i++) {
                c[i] = c[i - 1] + c[i];
            }
            //重新申请一个数组 用来存放排序后的数据
            int[] r = new int[n];
            //排序
            for (int i = n - 1; i >= 0; i--) {
                //取原数组元素对应的下标
                int index = c[a[i]] - 1;
                r[index] = c[i];
                c[a[i]]--;
            }
            //复制
            for (int i = 0; i < n; i++)
            {
                a[i] = r[i];
            }
        }
    }
}