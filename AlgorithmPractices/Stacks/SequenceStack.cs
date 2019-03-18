using System;

namespace AlgorithmPractices.Stacks {
    /// <summary>
    /// 用数组构成顺序栈
    /// </summary>
    public class SequenceStack<T> {
        private int capacicy;
        private T[] datas;
        private int length;

        public SequenceStack() : this(20) {

        }
        public SequenceStack(int capacicy) {
            this.capacicy = capacicy;
            datas = new T[capacicy];
        }

        public void Push(T data) {
            if (IsFull()) ExpandTo();
            datas[length++] = data;
        }

        public T Pop() {
            if (IsEmpty()) return default;
            return datas[--length];
        }

        private bool IsEmpty() {
            return length == 0;
        }

        //扩容
        private void ExpandTo() {
            T[] newDatas = new T[capacicy * 2];
            //复制
            for (int i = 0; i < capacicy; i++) {
                newDatas[i] = datas[i];
            }
            datas = newDatas;
            capacicy *= 2;
        }

        private bool IsFull() {
            return length == capacicy;
        }
        public int Length => length;
        public int Capacity => capacicy;
    }
}