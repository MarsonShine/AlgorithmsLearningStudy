public class Shell {
    /**
     * Sorts an array using the Shellsort algorithm.
     *
     * @param array the array to be sorted
     * @param <T>   the type of elements in the array
     */
    public static <T extends Comparable<T>> void sort(T[] array) {
        int length = array.length;
        int h = 1;// 步长
        // 选择合适的步长
        while (h < length / 3) {
            h = 3 * h + 1;
        }
        while (h >= 1) {
            // 将数组变为 h 有序
            for (int i = h; i < length; i++) {
                // 将 array[i] 插入到 i-h, i-2*h, i-3*h, ...
                for (int j = i; j >= h && SortHelper.less(array[j], array[j - h]); j-=h) {
                    SortHelper.exch(array, j, j - h);
                }
            }
            h = h / 3;
        }
    }

    public static void main(String[] args) {
        String[] a = new String[]{"A","G","S","E","P","W","C","D","B","F","H","I","J","K","L","M","N","Q","R","T","U","V","X","Y","Z"};
        sort(a);
        assert SortHelper.isSorted(a);
        SortHelper.show(a);
    }
}
