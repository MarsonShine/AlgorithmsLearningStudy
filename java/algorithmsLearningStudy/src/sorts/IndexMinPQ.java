// 索引优先队列实现
public class IndexMinPQ<T extends Comparable<T>> {
    private int N; // PQ中的元素数量
    private int[] pq; // 索引二叉堆，由1开始,表示索引i对应的元素在堆中的位置。
    private int[] qp; // 逆序：qp[pq[i]] = pq[qp[i]] = i
    private T[] items; // 有优先级之分的元素

    public IndexMinPQ(int maxN) {
        items = (T[]) new Comparable[maxN + 1];
        pq = new int[maxN + 1];
        qp = new int[maxN + 1];
        for (int i = 0; i <= maxN; i++)
            qp[i] = -1;
    }

    public boolean isEmpty() {
        return N == 0;
    }

    public boolean contains(int k){
        return qp[k] != -1;
    }

    public void insert(int k, T v) {
        N++;
        qp[k] = N;
        pq[N] = k;
        items[k] = v;
        swim(N);
    }

    public T min() {
        return items[pq[1]];
    }

    public int delMin() {
        int indexOfMin = pq[1];
        SortHelper.exch(items,indexOfMin,N);
        sink(1);
        items[pq[N]] = null;
        qp[pq[N]] = -1;
        return indexOfMin;
    }

    public int minIndex() {
        return pq[1];
    }

    public void change(int k, T v) {
        items[k] = v;
        swim(qp[k]);
        sink(qp[k]);
    }

    public void delete(int k) {
        int index = qp[k];
        SortHelper.exch(items, index, N--);
        swim(index);
        sink(index);
        items[k] = null;
        qp[k] = -1;
    }

    void sink(int k) {
        while (2 * k <= N) {
            int i = 2 * k;
            if (i < N && less(i, i + 1))
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

    boolean less(int i, int j) {
        return items[i].compareTo(items[j]) < 0;
    }
}
