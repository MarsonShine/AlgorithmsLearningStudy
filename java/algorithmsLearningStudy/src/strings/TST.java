public class TST<TValue> {
    private Node root; // 根节点

    private class Node {
        char c; // 字符
        Node left, mid, right; // 左中右子三向单词查找树
        TValue val; // 和字符串相关联的值
    }

    public TValue get(String key) {
        Node x = get(root, key, 0);
        if (x == null) {
            return null;
        }
        return x.val;
    }

    private Node get(Node x, String key, int d) {
        if (x == null) {
            return null;
        }
        char c = key.charAt(d);
        if (c < x.c) {
            return get(x.left, key, d);
        } else if (c > x.c) {
            return get(x.right, key, d);
        } else if (d < key.length() - 1) {
            return get(x.mid, key, d + 1);
        } else {
            return x;
        }
    }

    public void put(String key, TValue val) {
        root = put(root, key, val, 0);
    }
    /**
     * 一个 char 类型的值 c 和三条链接的结点构建了三向单词查找树，其中子树的键的首字母分别小于（左子树）、等于（中子树）和大于（右子树） c 。
     * @param x
     * @param key
     * @param val
     * @param d
     * @return
     */
    private Node put(Node x, String key, TValue val, int d) {
        char c = key.charAt(d);
        if (x == null) {
            x = new Node();
            x.c = c;
        }
        if (c < x.c) {
            x.left = put(x.left, key, val, d);
        } else if (c > x.c) {
            x.right = put(x.right, key, val, d);
        } else if (d < key.length() - 1) {
            x.mid = put(x.mid, key, val, d + 1);
        } else {
            x.val = val;
        }
        return x;
    }
}
