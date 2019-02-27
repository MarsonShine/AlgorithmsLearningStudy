namespace AlgorithmPractices.LinkedLists {
    //实现一个单链表支持增删操作
    public class SingleLinkedList<T> {

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