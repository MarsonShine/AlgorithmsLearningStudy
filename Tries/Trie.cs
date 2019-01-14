namespace Tries {
    public class Trie {
        private TrieNode root = new TrieNode('/'); //存储无意义字符
        //Trie 树中插入数据
        private void Insert(string text) {
            TrieNode p = root;
            for (int i = 0; i < text.Length; i++) {
                int index = text[i] - 'a';
                if (p.Children[index] == null) {
                    TrieNode newNode = new TrieNode(text[i]);
                    p.Children[index] = newNode;
                }
                p = p.Children[index];
            }
            p.IsEndingChar = true;
        }

        public bool Find(string pattern) {
            TrieNode p = root;
            for (int i = 0; i < pattern.Length; i++) {
                int index = pattern[i] - 'a';
                if (p.Children[index] == null) {
                    return false;
                }
                p = p.Children[index];
            }
            if (p.IsEndingChar == false) return false;
            else return true; //找到 pattern
        }
    }
}