import edu.princeton.cs.algs4.StdOut;

public class RedBlackBST<TKey extends Comparable<TKey>, TValue> {
    private static final boolean RED = true;
    private static final boolean BLACK = false;
    private Node root;

    private class Node {
        TKey key;
        TValue value;
        Node left;
        Node right;
        int N;
        boolean color;

        public Node(TKey key, TValue value, int n, boolean color) {
            this.key = key;
            this.value = value;
            this.N = n;
            this.color = color;
        }
    }

    private boolean isRed(Node x) {
        if (x == null) {
            return false;
        }
        return x.color == RED;
    }

    /**
     * Rotate the given node to the left.
     *
     * @param h the node to rotate
     * @return the rotated node
     */
    private Node rotateLeft(Node h) {
        Node x = h.right;
        h.right = x.left;
        x.left = h;
        x.color = h.color;
        h.color = RED;
        x.N = h.N;
        h.N = 1 + size(h.left) + size(h.right);
        return x;
    }

    private Node rotateRight(Node h) {
        Node x = h.left;
        h.left = x.right;
        x.right = h;
        x.color = h.color;
        h.color = RED;
        x.N = h.N;
        h.N = 1 + size(h.left) + size(h.right);
        return x;
    }

    public int size() {
        return size(root);
    }

    private int size(Node x) {
        if (x == null) {
            return 0;
        }
        return x.N;
    }

    /**
     * 翻转节点及其两个子节点的颜色
     *
     * @param h the node to flip colors for
     */
    private void flipColors(Node h) {
        // h 的颜色必须与两个子代的颜色相反
        h.color = !h.color;
        h.left.color = !h.left.color;
        h.right.color = !h.right.color;
    }

    /**
     * Puts the specified key-value pair into the map.
     *
     * @param key   the key to be added
     * @param value the value associated with the key
     */
    public void put(TKey key, TValue value) {
        root = put(root, key, value);
        root.color = BLACK;
    }

    public TValue get(TKey key) {
        return get(root, key);
    }

    private TValue get(Node h, TKey key) {
        if (h == null) {
            return null;
        }
        int cmp = key.compareTo(h.key);
        if (cmp < 0) {
            return get(h.left, key);
        } else if (cmp > 0) {
            return get(h.right, key);
        } else {
            return h.value;
        }
    }

    private Node put(Node h, TKey key, TValue value) {
        if (h == null) {
            return new Node(key, value, 1, RED);
        }
        int cmp = key.compareTo(h.key);
        if (cmp < 0) {
            h.left = put(h.left, key, value);
        } else if (cmp > 0) {
            h.right = put(h.right, key, value);
        } else {
            h.value = value;
        }
        if (isRed(h.right) && !isRed(h.left)) {
            h = rotateLeft(h);
        }
        if (isRed(h.left) && isRed(h.left.left)) {
            h = rotateRight(h);
        }
        if (isRed(h.left) && isRed(h.right)) {
            flipColors(h);
        }
        h.N = 1 + size(h.left) + size(h.right);
        return h;
    }

    public int rank(TKey key) {
        return rank(root, key);
    }

    private int rank(Node node, TKey key) {
        if (node == null) {
            return 0;
        }
        int cmp = key.compareTo(node.key);
        if (cmp < 0) {
            return rank(node.left, key);
        } else if (cmp > 0) {
            return 1 + size(node.left) + rank(node.right, key);
        } else {
            return size(node.left);
        }
    }

    public TKey min() {
        return min(root).key;
    }

    private Node min(Node node) {
        if (node.left == null) {
            return node;
        }
        return min(node.left);
    }

    public TKey max() {
        return max(root);
    }

    private TKey max(Node node) {
        if (node.right == null) {
            return node.key;
        }
        return max(node.right);
    }

    public void deleteMin() {
        root = deleteMin(root);
    }

    private Node deleteMin(Node node) {
        if (node.left == null)
            return null;
        if(!isRed(node.left) && !isRed(node.left.left)){
            node = moveRedLeft(node);
        }
        node.left = deleteMin(node.left);
        return balance(node);
    }

    /**
     * 假设 node 是红色的，而 node.left 和 node.left.left 都是黑色的。请将 node.left 或它的一个孩子节点设为红色。
     *
     * @param  node  目标节点
     * @return       the updated node after the red node has been moved
     */
    private Node moveRedLeft(Node node) {
        flipColors(node);
        if (isRed(node.right.left)) {
            node.right = rotateRight(node.right);
            node = rotateLeft(node);
            flipColors(node);
        }
        return node;
    }

    private Node balance(Node node) {
        if (isRed(node.right) && !isRed(node.left)) {
            node = rotateLeft(node);
        }
        if(isRed(node.left) && isRed(node.left.left)){
            node = rotateRight(node);
        }
        if(isRed(node.left) && isRed(node.right)){
            flipColors(node);
        }
        node.N = size(node.left) + size(node.right) + 1;
        return node;
    }

    public static void main(String[] args) {
        RedBlackBST<String, Integer> st = new RedBlackBST<>();
        String str = "E A S Y Q U T I O N";
        String[] strs = str.split(" ");
        for (String s : strs) {
            st.put(s, st.get(s) == null ? 1 : st.get(s) + 1);
        }
        StdOut.println("size = " + st.size());
        StdOut.println("min = " + st.min());
        StdOut.println("max = " + st.max());
    }
}