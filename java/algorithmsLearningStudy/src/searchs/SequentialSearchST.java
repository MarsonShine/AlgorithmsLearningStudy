public class SequentialSearchST<TKey, TValue> {
    private Node first;

    private class Node {
        TKey key;
        TValue val;
        Node next;

        public Node(TKey key, TValue val, Node next) {
            this.key = key;
            this.val = val;
            this.next = next;
        }
    }

    public TValue get(TKey key) {
        // 根据给定的 key 查询值
        for (Node x = first; x != null; x = x.next) {
            if (key.equals(x.key)) {
                return x.val;
            }
        }
        return null;
    }

    public void put(TKey key, TValue val) {
        for (Node x = first; x != null; x = x.next) {
            if (key.equals(x.key)) {
                x.val = val;
                return;
            }
        }
        first = new Node(key, val, first);
    }
}
