public class BinarySearchST<TKey extends Comparable<TKey>, TValue> {
    private TKey[] keys;
    private TValue[] vals;
    private int N;

    public BinarySearchST(int capacity) {
        keys = (TKey[]) new Comparable[capacity];
        vals = (TValue[]) new Object[capacity];
    }

    public int size() {
        return N;
    }

    public TValue get(TKey key) {
        if (isEmpty()) {
            return null;
        }
        int i = rank(key);
        if (i < N && keys[i].compareTo(key) == 0) {
            return vals[i];
        }
        return null;
    }

    public int rank(TKey key) {
        int lo = 0;
        int hi = N - 1;
        while (lo <= hi) {
            int mid = lo + (hi - lo) / 2;
            int cmp = key.compareTo(keys[mid]);
            if (cmp < 0) {
                hi = mid - 1;
            } else if (cmp > 0) {
                lo = mid + 1;
            } else {
                return mid;
            }
        }
        return lo;
    }

    public void put(TKey key, TValue value) {
        // 查找键，如果存在则更新值，否则插入
        int i = rank(key);
        if (i < N && keys[i].compareTo(key) == 0) {
            vals[i] = value;
            return;
        }
        for (int j = N; j > i; j--) {
            keys[j] = keys[j - 1];
            vals[j] = vals[j - 1];
        }
        keys[i] = key;
        vals[i] = value;
        N++;
    }

    public boolean isEmpty() {
        return N == 0;
    }

    public static void main(String[] args) {
        String[] keys = {"S", "E", "A", "R", "C"};
        BinarySearchST<String, Integer> st = new BinarySearchST<>(5);
        for (int i = 0; i < keys.length; i++) {
            st.put(keys[i], i);
        }
    }
}
