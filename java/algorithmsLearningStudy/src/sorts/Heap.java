public class Heap<T extends Comparable<T>> {
    private T[] items;
    private int N;

    public Heap(T[] a) {
        items = a;
        N = a.length;
    }

    /**
     * Sorts an array using the Heap Sort algorithm.
     *
     * @param None
     * @return None
     */
    public void heapsort() {
        int N = items.length;
        for (int k = N / 2; k >= 1; k--) {
            sink(k, N);
        }
        while (N > 1) {
            SortHelper.exch(items, 1, N--);
            sink(1, N);
        }
    }

    void sink(int k, int n) {
        while (2 * k <= n) {
            int i = 2 * k;
            if (i < n && less(i, i + 1))
                i++;
            if (!less(k, i))
                SortHelper.exch(items, k, i);
            k = i;
        }
    }

    boolean less(int i, int j) {
        return items[i].compareTo(items[j]) < 0;
    }
}
