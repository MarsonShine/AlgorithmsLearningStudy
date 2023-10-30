public class MaxPQ<T extends Comparable<T>> {
    private T[] items;
    private int n;

    public MaxPQ() {

    }

    public MaxPQ(int max) {
        items = (T[]) new Comparable[max + 1];
    }

    public MaxPQ(T[] a) {
        items = a;
    }

    void insert(T v) {
        for (int i = 0; i < items.length; i++) {

        }
    }

    T max() {
        return items[1];
    }

    T delMax() {
        T v = items[1];
        SortHelper.exch(items, n, 1);
        items[n + 1] = null;
        // 堆化
        sink(1);
        return v;
    }

    boolean isEmpty() {
        return n == 0;
    }

    int size() {
        return n;
    }

    boolean less(int i, int j) {
        return items[i].compareTo(items[j]) < 0;
    }

    void sink(int k) {
        while (2 * k <= n) {
            int i = 2 * k;
            if (i < n && less(i, i + 1))
                i++;
            if (!less(k, i))
                SortHelper.exch(items, k, i);
            k = i;
        }
    }

    void swim(int k) {
        while (k > 1 && less(k / 2, k)) {
            SortHelper.exch(items, k / 2, k);
            k = k / 2;
        }
    }
}