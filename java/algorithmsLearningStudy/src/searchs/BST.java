/*
 * 二叉查找树，中序遍历：左 -> 根 -> 右；前序遍历：根 -> 左 -> 右；后序遍历：左 -> 右 -> 根
 */

import edu.princeton.cs.algs4.Queue;

public class BST<TKey extends Comparable<TKey>, TValue> {
    private Node root;

    public class Node {
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
        if (node == null)
            return null;
        int cmp = key.compareTo(node.key);
        if (cmp < 0) {
            delete(node.left, key);
        } else if (cmp > 0) {
            delete(node.right, key);
        } else {
            // 判断是否有子节点
            if (node.left == null) {
                return node.right;
            } else if (node.right == null) {
                return node.left;
            }
            // 目标删除节点有两个子节点
            Node t = node; // 要删除的作为临时节点存储
            node = min(t.right); // 找到删除的右节点的最小节点
            node.right = deleteMin(t.right); // 将右节点的最小节点删除（因为该节点已经成为了新的父节点）
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

    public Iterable<TKey> keys() {
        return keys(min(), max());
    }

    public Iterable<TKey> keys(TKey lo, TKey hi) {
        Queue<TKey> queue = new Queue<>();
        keys(root,queue,lo,hi);
        return queue;
    }

    private void keys(Node node, Queue<TKey> queue, TKey lo, TKey hi) {
        // 中序遍历
        if (node == null) {
            return;
        }
        int cmplo = lo.compareTo(node.key);
        int cmphi = hi.compareTo(node.key);
        if (cmplo < 0) {
            keys(node.left,queue,lo,hi);
        }
        // 在 lo 和 hi 之间
        if (cmplo <= 0 && cmphi >= 0) {
            queue.enqueue(node.key);
        }
        if (cmphi > 0) {
            keys(node.right,queue,lo,hi);
        }
    }

    public static void main(String[] args) {
        BST<Integer, String> bst = new BST<>();

        // Insert some elements into the BST
        bst.put(5, "S");
        bst.put(2, "E");
        bst.put(10, "X");
        bst.put(1, "A");
        bst.put(7, "R");
        bst.put(9, "C");
        bst.put(4, "H");
        bst.put(3, "M");

        bst.delete(10);
    }
}