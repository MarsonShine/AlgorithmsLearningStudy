using System;
using System.Collections.Generic;

namespace AlgorithmPractices.Heads {
    /// <summary>
    /// 堆排序
    /// </summary>
    public class HeadSort<T> {
        private readonly IComparer<T> comparer;
        private T[] sources;
        private int position;
        private readonly int capacity;
        public HeadSort() : this(10) {
            comparer = Comparer<T>.Default;
        }

        public HeadSort(int capacity) {
            sources = new T[capacity];
            this.capacity = capacity;
        }

        public void Insert(T value) {
            sources[position++] = value;
            Headify();
        }
        public bool Pop() {
            if (position == 0) return false;
            var temp = sources[position];
            sources[--position] = default(T);
            sources[0] = temp;
            if (position > 2)
                HeadifyByRemove();
            if (comparer.Compare(sources[0], sources[1]) < 0) swap(0, 1);
            return true;
        }
        private void BuildHeadipy() {
            for (int i = position / 2; i > 0; i--) {
                Headify(i);
            }
        }

        private void Headify(int i) {
            while (true) {
                int max = 0;
                if ((i * 2 + 1) <= position && comparer.Compare(sources[i], sources[i * 2 + 1]) < 0) max = i * 2 + 2;
                if ((i * 2 + 2) <= position && comparer.Compare(sources[max], sources[i * 2 + 2]) < 0) max = i * 2 + 2;
                if (i == max) break;
                swap(i, max);
                i = max;
            }
        }

        private void HeadifyByRemove() {
            if (position < 2) return;
            var p = 0;
            var max = 0;
            while (true) {
                if ((p * 2 + 1) <= position && comparer.Compare(sources[p], sources[p * 2 + 1]) < 0) max = (p * 2 + 1);
                if (p * 2 + 2 <= position && comparer.Compare(sources[max], sources[p * 2 + 2]) < 0) max = (p * 2 + 2);
                if (p == max) break;
                swap(p, max);
                p = max;
            }
        }

        //数组堆化
        private void Headify() {
            if (position == 1) return;
            var p = position - 1;
            while (p > 0) {
                //找出父节点
                int pp = p / 2;
                //比较当前节点的值与父节点的值
                if (comparer.Compare(sources[p], sources[pp]) > 0) {
                    //swap
                    swap(pp, p);
                }
                p = pp;
            }
        }

        private void swap(int pp, int p) {
            T temp = sources[pp];
            sources[pp] = sources[p];
            sources[p] = temp;
        }

        public int Capacity => capacity;
    }
}