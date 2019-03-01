namespace AlgorithmPractices.LinkedLists {
    public class LinkedListNode<T> {
        private T value;
        public LinkedListNode(T value) {
            this.value = value;
        }

        public T Value { get; set; }
        public LinkedListNode<T> Prev { get; set; }
        public LinkedListNode<T> Next { get; set; }

    }
}