public class LinearProbingHashST<TKey, TValue> {
    private int N; // 符号表中键值对的总数
    private int M = 16; // 线性探测表的大小
    private TKey[] keys; // 键
    private TValue[] vals; // 值

    public LinearProbingHashST() {
        keys = (TKey[]) new Object[M];
        vals = (TValue[]) new Object[M];
    }

    public LinearProbingHashST(int cap) {
        M = cap;
        keys = (TKey[]) new Object[M];
        vals = (TValue[]) new Object[M];
    }

    private int hash(TKey key) {
        return (key.hashCode() & 0x7fffffff) % M;
    }

    // 扩充
    private void resize(int cap) {
        LinearProbingHashST<TKey, TValue> temp = new LinearProbingHashST<>(cap);
        for (int i = 0; i < M; i++) {
            if (keys[i] != null) {
                temp.put(keys[i], vals[i]);
            }
        }
        keys = temp.keys;
        vals = temp.vals;
        M = temp.M;
    }

    public void put(TKey key, TValue value) {
        if (N >= M / 2) {
            resize(2 * M);
        }
        int i;
        for (i = hash(key); keys[i] != null; i = (i + 1) % M) {
            if (keys[i].equals(key)) {
                vals[i] = value;
                return;
            }
        }
        keys[i] = key;
        vals[i] = value;
        N++;
    }

    public TValue get(TKey key) {
        for (int i = hash(key); keys[i] != null; i = (i + 1) % M) {
            if (keys[i].equals(key)) {
                return vals[i];
            }
        }
        return null;
    }

    public void delete(TKey key) {
        if (!contains(key)) {
            return;
        }
        int i = hash(key);
        while (!key.equals(keys[i])) {
            i = (i + 1) % M;
        }
        keys[i] = null;
        vals[i] = null;
        i = (i + 1) % M;
        // 将后面的元素依次向前移动
        while (keys[i] != null) {
            TKey tkey = keys[i];
            TValue tvalue = vals[i];
            keys[i] = null;
            vals[i] = null;
            put(tkey, tvalue);
            i = (i + 1) % M;
        }
        N--;
        // 缩小
        if (N > 0 && N <= M / 8) {
            resize(2 / M);
        }
    }

    public boolean contains(TKey key) {
        return get(key) != null;
    }
}
