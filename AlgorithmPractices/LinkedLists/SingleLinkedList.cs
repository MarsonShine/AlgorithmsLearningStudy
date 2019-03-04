using System;
using System.Collections;
using System.Globalization;

namespace AlgorithmPractices.LinkedLists {
    //实现一个单链表支持增删操作
    public class SingleLinkedList<T> {
        private int _capacity;
        private int _count;
        private SingleLinkedListNode<T> _head;
        private IComparer comparer;
        public SingleLinkedList() : this(20) {

        }
        public SingleLinkedList(int capacity) {
            _capacity = capacity;
            this.comparer = new Comparer(CultureInfo.CurrentCulture);
        }

        public SingleLinkedList(IComparer comparer) : this() {
            if (comparer != null) this.comparer = comparer;
        }

        public void InsertToTail(T item) {
            var newNode = new SingleLinkedListNode<T>(item);
            if (_head == null)
                InsertNodeToEmptyList(newNode);
            else {
                InsertToTail(newNode);
            }
        }
        public void InsertToTail(SingleLinkedListNode<T> newNode) {
            var node = _head;
            var tempNode = _head;
            while (tempNode.Next != null) {
                tempNode = tempNode.Next;
            }
            newNode.Next = tempNode.Next;
            tempNode.Next = newNode;
            ++_count;
        }
        public void InsertToHead(T item) {
            var node = new SingleLinkedListNode<T>(item);
            InsertToHead(node);
        }
        public void InsertToHead(SingleLinkedListNode<T> newNode) {
            if (_head == null)
                InsertNodeToEmptyList(newNode);
            else {
                _head.Next = newNode.Next;
                newNode.Next = _head;
                _head = newNode;
            }
        }
        public void AddAfter(SingleLinkedListNode<T> node, T item) {
            var newNode = new SingleLinkedListNode<T>(item);
            if (_head == null) {
                InsertNodeToEmptyList(newNode);
            } else {
                newNode.Next = node.Next;
                node.Next = newNode;
                ++_count;
            }
        }

        public void AddBefore(SingleLinkedListNode<T> node, T item) {

        }
        public void Remove(SingleLinkedListNode<T> node) {
            if (node == null || _head == null) return;
            //删除的是否为头结点
            if (node == _head) {
                _head = node.Next;
                return;
            }
            //找到node的上一个以及node的下一个
            var tempNode = _head;
            while (_head != null && tempNode.Next != node) {
                tempNode = tempNode.Next;
            }
            if (tempNode == null) return;
            tempNode.Next = tempNode.Next.Next;
        }

        public SingleLinkedListNode<T> MergeSortedList(SingleLinkedListNode<T> a, SingleLinkedListNode<T> b) {
            if (a == null) return b;
            if (b == null) return a;
            if (comparer == null) throw new InvalidOperationException("must be provide IComparer");
            var p = a;
            var q = b;
            SingleLinkedListNode<T> mergeNode;
            //比较头，来确定新合并之后的头结点
            if (comparer.Compare(p.Data, q.Data) < 0) {
                mergeNode = p;
                p = p.Next;
            } else {
                mergeNode = q;
                q = q.Next;
            }
            SingleLinkedListNode<T> r = mergeNode;
            while (p != null && q != null) {
                if (comparer.Compare(p.Data, q.Data) < 0) {
                    r.Next = p;
                    p = p.Next;
                } else {
                    r.Next = q;
                    q = q.Next;
                }
                r = r.Next;
            }
            if (p != null) {
                r.Next = p;
            } else {
                r.Next = q;
            }
            return mergeNode;

        }

        public SingleLinkedListNode<T> Reverse(SingleLinkedListNode<T> p) {
            if (_head == null || _head.Next == null) return _head;
            var head = new SingleLinkedListNode<T>(_head.Data);
            head.Next = p;
            var cur = p.Next;
            p.Next = null;
            SingleLinkedListNode<T> next = default;

            while (cur != null) {
                //把tmlHead恒定的插入到到第二个结点
                next = cur.Next;
                cur.Next = head.Next;
                head.Next = cur;
                cur = next;
            }
            //把头结点放到尾部
            var hnext = head.Next;
            head.Next = null;
            head = hnext;
            return head;
        }
        //用快慢指针，遍历同一个节点，如果快慢指针相等，则说明有环
        public bool HasCycle(SingleLinkedListNode<T> head) {
            if (head == null) return false;
            SingleLinkedListNode<T> fast = head.Next.Next;
            SingleLinkedListNode<T> slow = head.Next;
            while (fast != null && fast.Next != null) {
                fast = fast.Next.Next;
                slow = slow.Next;
                if (slow == fast) return true;
            }
            return false;
        }

        private void InsertNodeToEmptyList(SingleLinkedListNode<T> newNode) {
            _head = newNode;
            _count++;
        }

        public SingleLinkedListNode<T> Head => _head; //头结点
        public int Count => _count;
    }
}