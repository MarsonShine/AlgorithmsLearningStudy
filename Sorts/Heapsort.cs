using System;

namespace Sorts {
    /// <summary>
    /// 堆排序时间复杂度n*logn
    /// </summary>
    public class Heapsort {
        private int[] a;
        private int n;
        private int count;
        public Heapsort(int capicity) {
            a = new int[capicity + 1];
            n = capicity;
            count = 0;
        }
        public void Insert(int data) {
            if (count >= n) return; //堆满了
            ++count;
            a[count] = data;
            int i = count;
            while (i / 2 > 0 && a[i] > a[i / 2]) { //比较上个元素与当前，当前元素大，交替位置
                Swap(a, i, i / 2);
                i = i / 2; //继续与上级的上级结点元素比较
            }
        }

        private void Swap(int[] a, int current, int prev) {
            var temp = a[current];
            a[current] = a[prev];
            a[prev] = temp;
        }
    }
}