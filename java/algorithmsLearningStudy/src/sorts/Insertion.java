public class Insertion {
    public static <T extends Comparable<T>> void sort(T[] array) {
        int length = array.length;
        for (int i = 1; i < length; i++) {
            // 将a[i]插入到前面的有序序列中
            for (int j = i; j > 0 && SortHelper.less(array[j], array[j - 1]); j--) {
                // 交换
                SortHelper.exch(array, j, j - 1);
            }
        }
    }

    public static <T extends Comparable<T>> void sort(T[] array, int l, int h) {
        int length = h - l + 1;
        for (int i = 1; i < length; i++) {
            // 将a[i]插入到前面的有序序列中
            for (int j = i; j > 0 && SortHelper.less(array[j], array[j - 1]); j--) {
                // 交换
                SortHelper.exch(array, j, j - 1);
            }
        }
    }

    public static void main(String[] args) {
        String[] a = new String[] { "A", "G", "S", "E", "P", "W", "C", "D", "B", "F", "H", "I", "J", "K", "L", "M", "N",
                "Q", "R", "T", "U", "V", "X", "Y", "Z" };
        sort(a);
        assert SortHelper.isSorted(a);
        SortHelper.show(a);
    }
}
