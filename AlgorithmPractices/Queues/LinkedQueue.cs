using System;

namespace AlgorithmPractices.Queues {
    /// <summary>
    /// base of linkedlist
    /// </summary>
    public class LinkedQueue<T> {
        private Node head;
        private int capacity;
        private int length;
        public LinkedQueue(int capacity) {
            this.capacity = capacity;
        }
        public void Enqueue(T data) {
            if (length == capacity) return;
            var newNode = new Node(data);
            InsertNodeToTail(newNode);
        }
        public T Dequeue() {
            if (length == 0) return default;
            return RemoveHead();
        }

        private T RemoveHead() {
            if (head == null) return default;
            T current;
            current = head.Data;
            head = head.Next;
            length--;
            return current;
        }

        public int Capacity => capacity;
        public int Length => length;

        private void InsertNodeToTail(Node node) {
            if (head == null)
                head = node;
            else {
                var tempNode = head;
                while (tempNode.Next != null) {
                    tempNode = tempNode.Next;
                }
                tempNode.Next = node;
            }
            length++;
        }

        private sealed class Node {
            public Node(T data) {
                Data = data;
            }
            public T Data { get; set; }
            public Node Next { get; set; }
        }
    }
}