using System;

namespace Stacks {
    /// <summary>
    /// 基于链表实现的栈
    /// </summary>
    public class StackBasedOnLinkedList {
        private Node top = null;
        public void Push (int value) {
            Node newNode = new Node (value, null);
            //判断栈是否为空
            if (top == null) {
                top = newNode;
            } else {
                newNode.Next = top;
                top = newNode;
            }
        }
        /// <summary>
        /// 用-1表示栈中没有数据
        /// </summary>
        /// <returns></returns>
        public int Pop(){
            if(top == null) return -1;
            int value = top.Data;
            top = top.Next;
            return value;
        }

        public void PrintAll(){
            Node p = top;
            while (p!=null)
            {
                Console.WriteLine(p.Data+"   ");
                p = p.Next;
            }
            Console.ReadLine();
        }
        private class Node {
            private int data;
            private Node next;
            public Node (int data, Node next) {
                this.data = data;
                this.next = next;
            }
            public int Data {
                get { return data; }
            }

            public Node Next { get { return next; } set { next = value; } }
        }
    }
}