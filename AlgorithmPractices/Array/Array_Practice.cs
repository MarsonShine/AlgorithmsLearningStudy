using System;

namespace AlgorithmPractices.Array {
    //实现一个动态扩容的数组
    public class DynamicExpansionArray<T> {
        private T[] _arrays;
        private int _length;
        private int _capacity; //数组容量
        public DynamicExpansionArray() : this(10) {

        }
        public DynamicExpansionArray(int capacity) {
            _capacity = capacity;
            _arrays = new T[capacity];
        }
        /// <summary>
        /// 插入一个数据，支持动态扩容
        /// </summary>
        /// <param name="item"></param>
        public void Insert(T item) {
            if (++_length == _capacity) Expand(_arrays);
            _arrays[_length] = item;
        }
        public bool Remove() {
            if (_length == 0) return false;
            --_length; //移动索引值即可
            return true;
        }
        public T Find(T item, Predicate<T> match) {
            if (item == null) return default;
            for (int i = 0; i < _length; ++i) {
                if (match(item)) {
                    return _arrays[i];
                }
            }
            return default;
        }

        private void Expand(T[] arrays) {
            Resize(ref arrays, _capacity *= 2);
        }
        //合并两个有效数组为一个有效数组
        public T[] Concat(T[] ta, T[] tb) {
            if (ta?.Length > 0 && tb?.Length > 0) {
                T[] larray = new T[ta.Length + tb.Length];
                System.Array.Copy(larray, 0, ta, 0, ta.Length);
                System.Array.Copy(larray, ta.Length, tb, 0, tb.Length);
                return larray;
            }
            return default;
        }
        private void Resize(ref T[] array, int newSize) {
            T[] larray = array;
            if (larray == null) {
                array = new T[newSize];
                return;
            }
            if (larray.Length != newSize) {
                T[] newArray = new T[newSize];
                System.Array.Copy(larray, 0, newArray, 0, larray.Length > newSize?newSize : larray.Length);
                array = newArray;
            }
        }

        public T[] Values => _arrays;
        public int Length => _length;
    }
}