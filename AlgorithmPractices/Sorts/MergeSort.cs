using System;
using System.Collections.Generic;

namespace AlgorithmPractices.Sorts {
    public class MergeSort<T> {
        private IComparer<T> comparer;
        public MergeSort() {
            comparer = Comparer<T>.Default;
        }
        public void Sort(T[] sources) {
            MergeRecursion(sources, 0, sources.Length - 1);
        }
        public void Sort(T[] sources, IComparer<T> comparer) {
            this.comparer = comparer;
            MergeRecursion(sources, 0, sources.Length - 1);
        }
        //拆分
        public void MergeRecursion(T[] sources, int index, int end) {
            if (index >= end) return;
            int m = (index + end) / 2;
            MergeRecursion(sources, index, m);
            MergeRecursion(sources, m + 1, end);
            MergeSwapSort(sources, index, m, end);
        }

        private void MergeSwapSort(T[] sources, int index, int mid, int end) {
            //重新申请source等规模的新数组
            var newArrays = new T[end - index + 1];
            int p = index;
            int q = mid + 1;
            int i = 0;
            while (q <= end && p <= mid) {
                //比较
                if (comparer.Compare(sources[p], sources[q]) >= 0) {
                    newArrays[i++] = sources[q++];
                } else {
                    newArrays[i++] = sources[p++];
                }
            }
            //比较完，肯定有一方没有完全比较完
            int nstart = p;
            int nend = mid;
            if (q <= end) {
                nstart = q;
                nend = end;
            }
            while (nstart <= nend) {
                newArrays[i++] = sources[nstart++];
            }
            //把排序后的新数组赋值到原数组上
            for (int k = 0; k <= end - index; k++) {
                sources[index + k] = newArrays[k];
            }
        }
    }
}