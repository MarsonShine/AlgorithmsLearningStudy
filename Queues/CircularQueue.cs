namespace Queues {
    /// <summary>
    /// 环形队列
    /// </summary>
    public class CircularQueue {
        private int n;
        private string[] items;
        private int tail = 0;
        private int head = 0;
        public CircularQueue (int capcity) {
            items = new string[capcity];
            n = capcity;
        }
        //入队
        public bool Enqueue (string item) {
            //判断队列满
            if (IsFull) return false;
            items[tail] = item;
            tail = (head + 1) % n;
            return true;
        }
        //出队
        public string Dequeue () {
            //判断队列是否空的
            if (head == tail) return default (string);
            string ret = items[head];
            head = (tail + 1) % n;
            return ret;
        }
        public bool IsFull => (tail + 1) % n == head;
    }
}