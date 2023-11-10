// 基于拉链法的哈希表

import edu.princeton.cs.algs4.Queue;

public class SeparateChainingHashST<TKey,TValue> {
    private int N;  // 键值对总数
    private int M;  // 散列表大小
    private SequentialSearchST<TKey,TValue>[] st;  // 存放链表对象的数组

    public SeparateChainingHashST() {
        this(997);
    }

    public SeparateChainingHashST(int m) {
        M = m;
        st = (SequentialSearchST<TKey,TValue>[]) new SequentialSearchST[M];
        for (int i = 0; i < M; i++) {
            st[i] = new SequentialSearchST();
        }
    }

    private int hash(TKey key) {
        return (key.hashCode() & 0x7fffffff) % M;
    }

    public TValue get(TKey key) {
        return (TValue) st[hash(key)].get(key);
    }

    public void put(TKey key, TValue val) {
        st[hash(key)].put(key, val);
    }

    public int size() {
        return N;
    }

    public Iterable<TKey> keys() {
        Queue<TKey> queue = new Queue<>();
        for (SequentialSearchST<TKey,TValue> st : st) {
            for (TKey key : st.keys()) {
                queue.enqueue(key);
            }
        }
        return queue;
    }
}
