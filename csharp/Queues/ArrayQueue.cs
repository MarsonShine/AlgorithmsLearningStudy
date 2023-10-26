namespace Queues {
    public class ArrayQueue {
        private string[] items;
        private int n;
        private int head = 0;
        private int tail = 0;
        public ArrayQueue (int capcity) {
            n = capcity;
            items = new string[capcity];
        }

        //入队列
        public bool Enqueue (string value) {
            //如果tail==n，队列满
            if (tail == n) return false;
            items[tail] = value;
            ++tail;
            return true;
        }
        //出棧
        public string Dequeue () {
            //判斷隊列是否為空
            if (tail == head) return default (string);
            string value = items[head];
            ++head;
            return value;
        }
        //入隊列
        //當隊列出棧至隊列滿時，這是需要將數據搬移
        //例如[0,5]隊列，出棧变成[5,5],已经没有足够的空间入栈（此时[0,4]地址上的内容是空的，但因为无法入队列导致内存浪费）
        //这个时候就需要数据搬移
        public bool EnqueueAndAlignData (string value) {
            //队列尾部没有空间
            if (tail == n) {
                //并且占满整个队列空间
                if (head == 0) return false;
                //数据搬移
                for (int i = head; i < tail; ++i) {
                    items[i - head] = items[i];
                }
                //重新设置head，tail
                tail -= head;
                head = 0;
            }
            items[tail] = value;
            ++tail;
            return true;
        }
    }
}