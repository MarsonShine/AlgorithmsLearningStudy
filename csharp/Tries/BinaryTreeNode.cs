namespace Tries {
    public class BinaryTreeNode {
        public char Data { get; set; }
        public BinaryTreeNode Left { get; set; }
        public BinaryTreeNode Right { get; set; }

    }

    public class TrieNode {
        public TrieNode(char ch) {
            Data = ch;
        }
        public char Data { get; set; }
        public TrieNode[] Children { get; set; }
        public bool IsEndingChar { get; set; }
    }
}