// 归并，自底向上
public class MergeBU {
    private static Comparable[] temp;

    public static <T extends Comparable<T>> void sort(T[] array) {
        // 先进行两两归并
        int n = array.length;
        temp = new Comparable[n];
        for (int sz = 1; sz < n; sz = sz + sz) { // 合并的子数组大小，先从最小的size=1的数组开始合并
            for (int l = 0; l < n - sz; l += sz + sz) { // 将相邻的两个最小子数组归并
                int mid = l + sz - 1;
                merge(array, l, mid, Math.min(l + sz + sz - 1, n - 1));
            }
        }
    }

    public static <T extends Comparable<T>> void merge(T[] array, int l, int mid, int h) {
        // 双指针
        int i = l, j = mid + 1;
        // 复制数据
        for (int k = l; k <= h; k++) {
            temp[k] = (String) array[k];
        }
        for (int k = l; k <= h; k++) {
            if (i > mid) {
                array[k] = (T) temp[j++];
            } else if (j > h) {
                array[k] = (T) temp[i++];
            } else if (SortHelper.less(temp[i], temp[j])) {
                array[k] = (T) temp[i++];
            } else {
                array[k] = (T) temp[j++];
            }
        }
    }
}
