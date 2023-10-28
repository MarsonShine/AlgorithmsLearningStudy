import edu.princeton.cs.algs4.StdRandom;

public class Quick {
    public static <T extends Comparable<T>> void sort(T[] array) {
        StdRandom.shuffle(array); // 消除对输入的依赖：随机化处理的原因是为了避免最坏情况的发生。在快速排序算法中，如果输入数组已经有序或接近有序，那么算法的时间复杂度将退化为
                                  // O(n^2)。通过在快速排序之前 shuffle 数组，可以降低这种情况发生的概率，从而使算法保持平均时间复杂度为 O(n*log(n))。
        sort(array, 0, array.length - 1);
    }

    private static <T extends Comparable<T>> void sort(T[] array, int l, int h) {
        if (h <= l)
            return;
        int p = partition(array, l, h);
        sort(array, l, p - 1);
        sort(array, p + 1, h);
    }

    // 《算法》三向切分优化版
    private static <T extends Comparable<T>> void sort_quick3way(T[] array, int l, int h) {
        if (h <= l)
            return;
        int lt = l, i = l + 1, gt = h;
        T v = array[l];
        while (i <= gt) {
            int cmp = array[i].compareTo(v);
            if (cmp < 0)
                SortHelper.exch(array, lt++, i++);
            else if (cmp > 0)
                SortHelper.exch(array, gt--, i);
            else
                i++;
        }
        sort(array, l, lt - 1);
        sort(array, gt + 1, h);
    }

    // pivot 分区函数 partition 必须要实现以下几个目的：
    // 1. 对于区分的 p 点，a[l]~a[p-1]必须小于a[p]
    // 2. 对于区分的 p 点，a[p+1]~a[h]必须大于a[p]
    //
    public static <T extends Comparable<T>> int partition(T[] array, int l, int h) {
        // 将数组切分为 a[l..i-1] a[i] a[i+1..h]
        int i = l, j = h + 1; // 左右双指针，i 在子数组的最左边往右递增，j在子数组的最后边往左递减
        T t = array[l]; // 比 t 小的都在左边，大的都在右边
        while (true) {
            while (SortHelper.less(array[++i], t)) {
                if (i == h)
                    break;
            }
            while (SortHelper.less(t, array[--j])) {
                if (j == l)
                    break;
            }
            if (i >= j)
                break;
            SortHelper.exch(array, i, j);
        }
        SortHelper.exch(array, l, j);
        return j;
    }

    public static void main(String[] args) {
        String[] a = new String[] { "R", "B", "W", "W", "R", "W", "B", "R", "R", "W", "B", "R" };
        sort_quick3way(a, 0, a.length - 1);
        // sort(a);
        assert SortHelper.isSorted(a);
        SortHelper.show(a);
    }
}
