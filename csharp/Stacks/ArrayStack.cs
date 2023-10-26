using System;
using System.Collections.Generic;
using System.Text;

namespace Stacks
{
    public class ArrayStack<T>
    {
        private T[] items;  //数组
        private int count;  //栈元素中的个数
        private int n;  //数组大小

        public ArrayStack(int n)
        {
            this.items = new T[n];
            this.n = n;
            this.count = 0;
        }

        //push
        public bool Push(T item)
        {
            //数组控件不够，入栈失败
            if (count == n) return false;
            //入栈
            items[count] = item;
            ++count;
            return true;
        }

        //pop
        public T Pop()
        {
            if (count == 0) return default(T);
            T item = items[count - 1];
            --count;
            return item;
        }
    }
}
