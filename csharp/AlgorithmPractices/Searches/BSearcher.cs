using System;
using System.Collections;

namespace AlgorithmPractices.Searches {
    public class BSearcher {
        private readonly IComparer comparer;
        public BSearcher(IComparer comparer = null) {
            this.comparer = comparer ?? Comparer.Default;
        }
        public int Get<T>(T[] datas, T data) {
            if (datas == null || datas.Length == 0) return -1;
            return Search(datas, 0, datas.Length - 1, data);
        }

        private int Search<T>(T[] datas, int start, int end, T value) {
            int mid = (start + end) / 2; //probebly calculate overflowexception  int mid = low + ((hight - low) >> 1)
            int comparerResult = comparer.Compare(datas[mid], value);
            if (comparerResult == 0) return mid;
            else if (comparerResult < 0) return Search(datas, start, mid - 1, value);
            else return Search(datas, mid + 1, end, value);
        }
    }
}