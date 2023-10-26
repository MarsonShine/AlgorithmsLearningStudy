namespace AlgorithmPractices.LinkedLists {
    using System.Collections.Generic;
    using System;

    public class DoublyLinkedList<T> {
        private LinkedListNode<T> head;
        private int count;

        public DoublyLinkedList() { }
        public DoublyLinkedList(LinkedListNode<T> node) {
            head = node;
        }

        public void AddBefore(LinkedListNode<T> node, T value) {
            ValidateNode(node);
            var newNode = new LinkedListNode<T>(value);
            if (head == null)
                InsertNodeToEmptyList(newNode);
            else {
                InsertNodeToNodeBefore(node, newNode);
            }
            //判断node是否为头结点，插入头结点之前，即value就是头结点
            if (head == node) {
                head = newNode;
            }
        }

        public void AddAfter(LinkedListNode<T> node, T value) {
            ValidateNode(node);
            var newNode = new LinkedListNode<T>(value);
            if (head == null) {
                InsertNodeToEmptyList(newNode);
            } else {
                InsertNodeToNodeBefore(newNode, node);
            }
        }
        public LinkedListNode<T> AddLast(T value) {
            var node = new LinkedListNode<T>(value);
            if (head == null) {
                InsertNodeToEmptyList(node);
            } else {
                AddLast(node);
            }
            return node;
        }
        public void AddLast(LinkedListNode<T> node) {
            ValidateNode(node);
            if (head == null) {
                InsertNodeToEmptyList(node);
            } else {
                InsertNodeToNodeBefore(head, node);
            }
        }

        public void AddFirst(LinkedListNode<T> node) {
            ValidateNode(node);
            if (head == null) {
                InsertNodeToEmptyList(node);
            } else {
                InsertNodeToNodeBefore(head, node);
                head = node;
            }
        }
        public LinkedListNode<T> Find(T value) {
            LinkedListNode<T> node = head;
            EqualityComparer<T> c = EqualityComparer<T>.Default;
            if (node != null) {
                if (value != null) {
                    while (node != head) {
                        if (c.Equals(node.Value, value)) {
                            return node;
                        }
                        node = node.Next;
                    }
                }
            }
            return default;
        }

        public int Count => count;

        private void InsertNodeToNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode) {
            newNode.Next = node;
            newNode.Prev = node.Prev;
            node.Prev = newNode;
            node.Prev.Next = newNode;
            count++;
        }

        private void InsertNodeToEmptyList(LinkedListNode<T> newNode) {
            newNode.Next = newNode;
            newNode.Prev = newNode;
            head = newNode;
            count++;
        }

        private void ValidateNode(LinkedListNode<T> node) {
            if (node == null) throw new ArgumentNullException(nameof(node));
        }
    }
}