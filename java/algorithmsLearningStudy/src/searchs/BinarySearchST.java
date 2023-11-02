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
        // 待插入位置之后的元素都向后移动
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

    public TKey min() {
        return keys[0];
    }

    public TKey max() {
        return keys[N - 1];
    }

    public TKey select(int k) {
        return keys[k];
    }

    public TKey delete(TKey key) {
        int i = rank(key);
        // 移动元素
        if (i < N && keys[i].compareTo(key) == 0) {
            for (int j = i; j < N - 1; j++) {
                keys[j] = keys[j + 1];
                vals[j] = vals[j + 1];
            }
            N--;
            return key;
        }
        return null;
    }

    /**
     * Returns the largest key in the symbol table that is smaller than or equal to the given key.
     *
     * @param  key  the key to search for
     * @return      the largest key in the symbol table that is smaller than or equal to the given key, or null if there is no such key
     */
    public TKey floor(TKey key) {
        int i = rank(key);
        if (i < N && keys[i].compareTo(key) == 0) {
            return keys[i];
        }
        if (i == 0) {
            return null;
        }
        return keys[i - 1];
    }

    /**
     * Returns the smallest key in the symbol table greater than or equal to the given key.
     *
     * @param  key	the key to search for
     * @return     	the smallest key greater than or equal to the given key, or null if no such key exists
     */
    public TKey ceiling(TKey key) {
        int i = rank(key);
        return keys[i];
    }

    public static void main(String[] args) {
        String[] keys = {"S", "E", "A", "R", "C"};
        BinarySearchST<String, Integer> st = new BinarySearchST<>(5);
        for (int i = 0; i < keys.length; i++) {
            st.put(keys[i], i);
        }

        st.floor("B");
    }
}
