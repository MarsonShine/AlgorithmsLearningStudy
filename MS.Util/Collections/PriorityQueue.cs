using System;
using System.Collections.Generic;

namespace MS.Util.Collections {
    public class PriorityQueue<T> {
        T[] heap;
        IComparer<T> comparer;
        int count;

        public PriorityQueue(int capacity) : this(capacity, null) { }

        public PriorityQueue(int capacity, IComparer<T> comparer) {
            this.comparer = comparer == null?Comparer<T>.Default : comparer;
            this.heap = new T[capacity];
        }

        public void Push(T t) {
            if (heap.Length < count) Array.Resize(ref heap, count * 2);
            heap[count] = t;
            SiftUp(count++);
        }
        public T Pop() {
            var v = Top();
            heap[0] = heap[--count];
            if (count > 0) SiftDown(0);
            return v;
        }

        private T Top() {
            if (count > 0) return heap[0];
            throw new InvalidOperationException("queue empty");
        }

        public void Update(T t) {

        }
        private void SiftUp(int n) {
            var v = heap[n];
            for (var n2 = n / 2; n > 0 && comparer.Compare(heap[n2], v) > 0; n = n2, n2 /= 2) heap[n] = heap[n2];
            heap[n] = v;
        }

        private void SiftDown(int n) {
            var v = heap[n];
            for (var n2 = n * 2; n2 < count; n = n2, n2 *= 2) {
                if (n2 + 1 < count && comparer.Compare(heap[n2], heap[n2 + 1]) > 0) n2++;
                if (comparer.Compare(heap[n2], v) >= 0) break;
                heap[n] = heap[n2];
            }
            heap[n] = v;
        }

        public int Count { get { return count; } }
    }
}