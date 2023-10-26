using System;

namespace AlgorithmPractices.Stacks {
    /// <summary>
    /// maded up stack by linkedlist
    /// </summary>
    public class LinkedStack<T> {
        private Node head;
        private int capacity;
        private int length;
        public LinkedStack(int capacity) {
            this.capacity = capacity;
        }

        public void Push(T data) {
            if (IsFull()) return;
            InsertData(data);
        }

        private void InsertData(T data) {
            if (length == 0) InsertHeadNode(new Node(data));
            else InsertNodeToTail(new Node(data));
        }

        private void InsertNodeToTail(Node node) {
            if (length == capacity) return;
            Node tail = head;
            while (tail.Next != null) {
                tail = tail.Next;
            }
            tail.Next = node.Next;
            tail.Next = node;
            length++;
        }

        private void InsertHeadNode(Node node) {
            this.head = node;
            length++;
        }

        private bool IsFull() {
            return length == capacity;
        }

        private sealed class Node {
            public Node(T data) {
                this.Data = data;
            }
            public T Data { get; set; }
            public Node Next { get; set; }
        }
    }
}