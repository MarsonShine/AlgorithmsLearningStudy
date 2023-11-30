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
}
