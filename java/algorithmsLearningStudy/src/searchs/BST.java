public class BST<TKey extends Comparable<TKey>, TValue> {
    private Node root;

    private class Node {
        private TKey key;
        private TValue value;
        private Node left;
        private Node right;
        private int N;

        public Node(TKey key, TValue value, int n) {
            this.key = key;
            this.value = value;
            this.N = n;
        }
    }

    public int size() {
        return size(root);
    }

    public int size(Node x) {
        if (x == null) {
            return 0;
        }
        return x.N;
    }

    public TValue get(Node node, TKey key) {
        // 查询
        if (node == null) {
            return null;
        }
        int cmp = key.compareTo(node.key);
        if (cmp < 0) {
            return get(node.left, key);
        } else if (cmp > 0) {
            return get(node.right, key);
        }
        return node.value;
    }

    public void put(TKey key, TValue value) {
        root = put(root, key, value);
    }

    private Node put(Node node, TKey key, TValue value) {
        if (node == null)
            return new Node(key, value, 1);
        int cmp = key.compareTo(node.key);
        if (cmp < 0) {
            node.left = put(node.left, key, value);
        } else if (cmp > 0) {
            node.right = put(node.right, key, value);
        } else {
            node.value = value;
        }
        node.N = 1 + size(node.left) + size(node.right);
        return node;
    }

    public boolean isEmpty() {
        return root == null;
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

    public TKey select(int k) {
        return select(root, k).key;
    }

    private Node select(Node node, int k) {
        // 返回排名为 k 的结点
        if (node == null) {
            return null;
        }
        int t = size(node.left);
        if (t > k)
            return select(node.left, k);
        else if (t < k)
            return select(node.right, k - t - 1);
        else
            return node;
    }

    public void delete(TKey key) {
        root = delete(root, key); 
    }

    public Node delete(Node node, TKey key) {
        if(node == null) return null;
        int cmp = key.compareTo(node.key);
        if(cmp < 0) {
            delete(node.left, key);
        } else if (cmp > 0) {
            delete(node.right, key);
        }else {
            // 判断是否有子节点
            if (node.left == null) {
                return node.right;
            } else if(node.right == null) {
                return node.left;
            }
            // 目标删除节点有两个子节点
            Node t = node; // 要删除的作为临时节点存储
            node = min(t.right);
            node.right = deleteMin(t.right);
            node.left = t.left;
        }
        node.N = 1 + size(node.left) + size(node.right);
        return node;
    }

    public Node deleteMin() {
        return deleteMin(root);
    }

    private Node deleteMin(Node node) {
        if (node.left == null) {
            return node.right;
        }
        node.left = deleteMin(node.left);
        node.N = 1 + size(node.left) + size(node.right);
        return node;
    }

    public Node min() {
        return min(root);
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

    public TKey floor(TKey key) {
        Node node = floor(root, key);
        return node == null ? null : node.key;
    }

    private Node floor(Node node, TKey key) {
        if (node == null)
            return null;
        int cmp = key.compareTo(node.key);
        if (cmp == 0)
            return node;
        if (cmp < 0)
            return floor(node.left, key);
        Node t = floor(node.right, key);
        if (t != null)
            return t;
        else
            return node;
    }
}