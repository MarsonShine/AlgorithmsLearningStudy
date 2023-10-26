using System;

namespace AlgorithmPractices.Queues {
    /// <summary>
    /// circular queue
    /// </summary>
    public class CircleQueue<T> {
        private Node head;
        private int capacity;
        private int length;
        public CircleQueue(int capacity) {
            this.capacity = capacity;
        }
        public void Enqueue(T data) {
            var newNode = new Node(data);
            if (head == null) {
                head = newNode;
                newNode.Next = head;
                length++;
            } else {
                if (length == capacity) return;
                InsertNodeToTail(newNode);
            }
        }

        public T Dequeue() {
            if (length == 0) return default;
            return RemoveHead();
        }

        private T RemoveHead() {
            if (head == null) return default;
            var nhead = head.Next;
            while (nhead.Next != null && nhead.Next != head) {
                nhead = nhead.Next;
            }
            T current;
            current = head.Data;
            head = head.Next;
            nhead.Next = head;
            length--;
            return current;
        }

        private void InsertNodeToTail(Node newNode) {
            var nhead = head.Next;
            while (nhead.Next != null && nhead.Next != head) {
                nhead = nhead.Next;
            }
            newNode.Next = nhead.Next;
            nhead.Next = newNode;
            length++;
        }

        public int Capacity => capacity;
        public int Length => length;
        private sealed class Node {
            public Node(T data) {
                Data = data;
            }
            public T Data { get; set; }
            public Node Next { get; set; }
        }
    }
}