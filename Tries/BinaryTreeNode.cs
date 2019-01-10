namespace Tries {
    public class BinaryTreeNode {
        public char Data { get; set; }
        public BinaryTreeNode Left { get; set; }
        public BinaryTreeNode Right { get; set; }

    }

    class TrieNode {
        char Data { get; set; }
        TrieNode[] Children { get; set; }
    }
}