namespace Queues {
    /// <summary>
    /// 基于链表的链表队列
    /// </summary>
    public class QueueBasedOnLinkedList {
        //队列的首尾
        private Node head;
        private Node tail;
        public QueueBasedOnLinkedList () {

        }
        public void Enqueue (string item) {
            //队列空
            if (tail == null) {
                Node newNode = new Node (item, null);
                head = newNode;
                tail = newNode;
            } else {
                tail.Next = new Node (item, null);
                tail = tail.Next;
            }
        }
        // 出队
        public string Dequeue () {
            if (head == null) return null;
            string value = head.data;
            head = head.next;
            if (head == null) {
                tail = null;
            }
            return value;
        }
        private class Node {
            private string data;
            private Node next;
            public Node (string data, Node next) {
                this.data = data;
                this.next = next;
            }
            public string Data { get => data; set => data = value; }
            public Node Next { get => next; set => next = value; }
        }
    }
}