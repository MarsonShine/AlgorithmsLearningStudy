using System;

namespace AlgorithmPractices.Queues {
    /// <summary>
    /// 顺序队列 made up by array
    /// </summary>
    public class SequenceQueue<T> {
        private T[] datas;
        private int length;
        private int capacity;

        public SequenceQueue(int capacity) {
            this.capacity = capacity;
            datas = new T[capacity];
        }

        public void Enqueue(T data) {
            if (length == capacity) ExpandeTo();
            datas[length++] = data;
        }

        public T Dequeue() {
            if (length == 0) return default;
            return datas[length--];
        }

        private void ExpandeTo() {
            throw new NotImplementedException();
        }

        public int Length => length;
        public int Capacity => capacity;
    }
}