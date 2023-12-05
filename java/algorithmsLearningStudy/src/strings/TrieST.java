import edu.princeton.cs.algs4.Queue;

/**
 * 基于单词查找树的符号表
 */
public class TrieST<TValue> {
    private static int R = 256; // 基数
    private Node root; // 单词查找树的根结点

    private static class Node {

        private Object val;
        private Node[] next = new Node[R];
    }

    public TValue get(String key) {
        Node x = get(root, key, 0);
        if (x == null) {
            return null;
        }
        return (TValue) x.val;
    }

    private Node get(Node x, String key, int d) {
        // 返回以x作为根结点的子单词查找树中与key相关联的值
        if (x == null) {
            return null;
        }
        if (d == key.length()) {
            return x;
        }
        char c = key.charAt(d); // 找到第d个字符所对应的子单词查找树
        return get(x.next[c], key, d + 1);
    }

    public void put(String key, TValue val) {
        root = put(root, key, val, 0);
    }

    private Node put(Node x, String key, TValue val, int d) {
        // 如果key存在于以x为根结点的子单词查找树中则更新与它相关联的值
        if (x == null) {
            x = new Node();
        }
        if (d == key.length()) {
            x.val = val;
            return x;
        }
        char c = key.charAt(d);
        x.next[c] = put(x.next[c], key, val, d + 1);
        return x;
    }

    public Iterable<String> keys() {
        return keysWithPrefix("");
    }

    public Iterable<String> keysWithPrefix(String pre) {
        Queue<String> q = new Queue<>();
        collect(get(root, pre, 0), pre, q);
        return q;
    }

    private void collect(Node x, String pre, Queue<String> q) {
        if (x == null) {
            return;
        }
        if (x.val != null) {
            q.enqueue(pre);
        }
        for (char c = 0; c < R; c++) {
            collect(x.next[c], pre + c, q);
        }
    }

    public Iterable<String> keysThatMatch(String pat) {
        Queue<String> q = new Queue<>();
        collect(root, "", pat, q);
        return q;
    }

    /**
     * 递归地收集与模式匹配的键，并将它们添加到队列 q 中。
     * 获取当前已匹配的前缀字符串长度 d，即 pre 的长度。
     * 如果 d 等于模式字符串 pat 的长度，并且当前节点 x 的值不为空，则将当前前缀字符串 pre 加入队列 q 中。
     * 
     * 接下来，使用一个循环遍历可能的字符取值，即从 0 到 R（字符集大小）：
     * 如果 next 是通配符字符 '.'，或者等于当前遍历到的字符 c，则递归调用 collect() 方法，传入下一个节点
     * x.next[c]、当前前缀字符串 pre 加上字符 c、模式字符串 pat 和队列 q。注意，x.next[c] 表示当前节点 x 在字符 c
     * 的边所对应的下一个节点。
     * 
     * @param x
     * @param pre
     * @param pat
     * @param q
     */
    private void collect(Node x, String pre, String pat, Queue<String> q) {
        if (x == null) {
            return;
        }
        int d = pre.length();
        if (d == pat.length() && x.val != null) {
            q.enqueue(pre);
        }
        if (d == pat.length()) {
            return;
        }
        char next = pat.charAt(d);
        for (char c = 0; c < R; c++) {
            if (next == '.' || next == c) {
                collect(x.next[c], pre + c, pat, q);
            }
        }
    }

    /**
     * 找到给定字符串的最长键前缀
     * 
     * @param s
     * @return
     */
    public String longestPrefixOf(String s) {
        int length = search(root, s, 0, 0);
        return s.substring(0, length);
    }

    /**
     * 递归搜索最长前缀
     * 如果当前节点x为空，则表示已经搜索到字典树的底部，直接返回当前的前缀长度length
     * 如果当前节点x不为空（即到当前节点为止存在一个键），则将当前深度 d 更新为最长前缀长度 length
     * @param x
     * @param s
     * @param d
     * @param length
     * @return
     */
    private int search(Node x, String s, int d, int length) {
        if (x == null) {
            return length;
        }
        if (x.val != null) {
            length = d;
        }
        if (d == s.length()) {
            return length;
        }
        char c = s.charAt(d);
        // 递归调用 search() 方法，传入下一个节点 x.next[c]、字符串 s、深度 d + 1 和当前最长前缀的长度 length。
        return search(x.next[c], s, d + 1, length);
    }

    public static void main(String[] args){
        TrieST<String> trie = new TrieST<>();
        trie.put("app", "app");
        trie.put("apple", "apple");
        trie.put("application", "application");

        String s = "applesauce";
        String longestPrefix = trie.longestPrefixOf(s);
        System.out.println("Longest prefix of " + s + ": " + longestPrefix);
    }
    /**
     * 从一棵单词查找树中删去一个键值对的第一步是，找到键所对应的结点并将它的值设为空 （null） 。
     * 如果该结点含有一个非空的链接指向某个子结点，那么就不需要再进行其他操作了。
     * 如果它的所有链接均为空，那就需要从数据结构中删去这个结点。
     * 如果删去它使得它的父结点的所有链接也均为空，就需要继续删除它的父结点，依此类推。
     * @param key
     */
    public void delete(String key) {
        root = delete(root, key, 0);
    }

    public Node delete(Node x, String key, int d) {
        if (x == null) {
            return null;
        }
        if (d == key.length()) {
            x.val = null;
        } else {
            char c = key.charAt(d);
            x.next[c] = delete(x.next[c], key, d + 1);
        }
        if (x.val != null) {
            return x;
        }
        for (char c = 0; c < R; c++) {
            if (x.next[c] != null) {
                return x;
            }
        }
        return x;
    }
}
