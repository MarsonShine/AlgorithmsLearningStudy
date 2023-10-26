public class Selection {
    /**
     * Sorts an array of comparable elements using the selection sort algorithm.
     *
     * @param array the array to be sorted
     * @param <T>   the type of elements in the array
     */
    public static <T extends Comparable<T>> void sort(T[] array) {
        int length = array.length;
        for (int i = 0; i < length; i++) {
            int minIndex = i;
            for (int j = i + 1; j < length; j++) {
                if (SortHelper.less(array[j], array[minIndex])) {
                    minIndex = j;
                }
            }
            SortHelper.exch(array, i, minIndex);
        }
    }

    public static void main(String[] args) {
        String[] a = new String[]{"A","G","S","E","P","W","C","D","B","F","H","I","J","K","L","M","N","Q","R","T","U","V","X","Y","Z"};
        sort(a);
        assert SortHelper.isSorted(a);
        SortHelper.show(a);
    }
}