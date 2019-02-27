using System;

namespace AlgorithmPractices.LinkedLists {
    //实现一个单链表支持增删操作
    public class SingleLinkedList<T> {
        private int _capacity;
        private int _count;
        private SingleLinkedListNode<T> _head;
        public SingleLinkedList() : this(20) { }
        public SingleLinkedList(int capacity) {
            _capacity = capacity;
        }

        public void InsertToTail(T item) {
            var node = new SingleLinkedListNode<T>(item);
            ++_count;
        }
        public void InsertToTail(SingleLinkedListNode<T> node) {

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
        private void InsertNodeAfter(SingleLinkedListNode<T> node, SingleLinkedListNode<T> newNode) {
            newNode.Next = node.Next;
        }
        private void InsertNodeBefore(SingleLinkedListNode<T> node, SingleLinkedListNode<T> newNode) {
            newNode.Next = node;
            _head = newNode;
            ++_count;
        }

        private void InsertNodeToEmptyList(SingleLinkedListNode<T> newNode) {
            _head = newNode;
            _count++;
        }

        public SingleLinkedListNode<T> Head => _head; //头结点
        public int Count => _count;
    }

    public class SingleLinkedListNode<TNode> {
        public SingleLinkedListNode(TNode node) {
            Data = node;
        }
        public SingleLinkedListNode(TNode node, SingleLinkedListNode<TNode> next) : this(node) {
            Next = next;
        }
        public TNode Data { get; set; }
        public SingleLinkedListNode<TNode> Next { get; set; }
    }
}