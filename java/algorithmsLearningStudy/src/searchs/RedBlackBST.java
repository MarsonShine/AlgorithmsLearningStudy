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
     * @param  h   the node to rotate
     * @return     the rotated node
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

    private int size(Node x) {
        if (x == null) {
            return 0;
        }
        return x.N;
    }

    /**
     * Flips the colors of the given node and its children.
     *
     * @param  h  the node to flip colors for
     */
    private void flipColors(Node h) {
        h.color = RED;
        h.left.color = BLACK;
        h.right.color = BLACK;
    }

    /**
     * Puts the specified key-value pair into the map.
     *
     * @param  key   the key to be added
     * @param  value the value associated with the key
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

    public static void main(String[] args) {
        RedBlackBST<String, Integer> st = new RedBlackBST<>();
        String str = "E A S Y Q U T I O N";
        String[] strs = str.split(" ");
        for (String s : strs) {
            st.put(s, st.get(s) == null ? 1 : st.get(s) + 1);
        }
    }
}