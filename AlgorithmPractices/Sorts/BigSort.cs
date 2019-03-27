using System;
using System.Collections.Generic;

namespace AlgorithmPractices.Sorts {
    public class BigSort<T> {
        private IComparer<T> comparer;
        public BigSort() {
            comparer = Comparer<T>.Default;
        }
        public void MergeSort(T[] sources) {
            MergeRecursion(sources, 0, sources.Length - 1);
        }
        public void MergeSort(T[] sources, IComparer<T> comparer) {
            this.comparer = comparer;
            MergeRecursion(sources, 0, sources.Length - 1);
        }
        public void BublleSort(T[] sources, IComparer<T> comparer = null) {
            if (sources == null || sources.Length == 0) return;
            if (comparer != null) this.comparer = comparer;
            for (int i = 0; i < sources.Length; i++) {
                for (int j = i + 1; j < sources.Length; j++) {
                    T temp;
                    if (this.comparer.Compare(sources[i], sources[j]) > 0) {
                        temp = sources[i];
                        sources[i] = sources[j];
                        sources[j] = temp;
                    }
                }
            }
        }
        public void BublleSortOptimize(T[] sources) {
            if (sources == null || sources.Length == 0) return;
            for (int i = 0; i < sources.Length; ++i) {
                bool flag = false;
                for (int j = 0; j < sources.Length - i - 1; ++j) {
                    if (this.comparer.Compare(sources[j], sources[j + 1]) > 0) {
                        T temp = sources[j];
                        sources[j] = sources[j + 1];
                        sources[j + 1] = temp;
                        flag = true;
                    }
                    if (!flag) break;
                }
            }
        }
        public void InsertionSort(T[] sources) {
            if (sources == null || sources.Length == 0) return;
            for (int i = 1; i < sources.Length; i++) {
                var value = sources[i];
                int j = i - 1;
                for (; j >= 0; j--) {
                    if (comparer.Compare(sources[j], value) > 0) {
                        //这里只进行了数据复制，没有数据交换，因为这样可以只做数据的移动，最后一次才把缓存的数据赋值给最后一个比较的数据，省去每次比较之后的值的替换
                        //更高效
                        sources[j + 1] = sources[j];
                    } else break;
                }
                sources[j + 1] = value;
            }
        }
        public void QuicklySort(T[] sources) => QuicklySortInternal(sources, 0, sources.Length - 1);

        private void QuicklySortInternal(T[] sources, int start, int end) {
            if (start >= end) return;
            int q = Partition(sources, start, end);
            QuicklySortInternal(sources, start, q - 1);
            QuicklySortInternal(sources, q + 1, end);
        }

        private int Partition(T[] sources, int start, int end) {
            T pivot = sources[end];
            int i = start;
            for (int j = 0; j < sources.Length; j++) {
                if (comparer.Compare(sources[j], pivot) < 0) {
                    T temp = sources[j];
                    sources[i] = sources[j];
                    sources[j] = temp;
                    ++i;
                }
            }
            T p = sources[i];
            sources[i] = sources[end];
            sources[end] = p;
            return i;
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