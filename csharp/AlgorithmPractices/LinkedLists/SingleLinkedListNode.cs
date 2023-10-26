namespace AlgorithmPractices.LinkedLists {
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