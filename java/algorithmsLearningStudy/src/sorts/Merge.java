public class Merge {
    public static <T extends Comparable<T>> void sort(T[] array) {
        temp = new String[array.length];
        // 分成不同的小块进行各自排序
        sort(array, 0, array.length - 1);
    }

    public static <T extends Comparable<T>> void sort(T[] array, int l, int h) {
        if (l >= h)
            return;
        int mid = l + (h - l) / 2;
        // left
        sort(array, l, mid);
        // right
        sort(array, mid + 1, h);
        merge(array, l, mid, h);
    }

    // 合并 l-mid,mid+1,h 两个数组
    private static String[] temp;

    public static <T extends Comparable<T>> void merge(T[] array, int l, int mid, int h) {
        // 双指针
        int i = l, j = mid + 1;
        // 复制数据
        for (int k = l; k <= h; k++) {
            temp[k] = (String)array[k];
        }
        for (int k = l; k <= h; k++) {
            if (i > mid) {
                array[k] = (T)temp[j++];
            } else if (j > h) {
                array[k] = (T)temp[i++];
            } else if (SortHelper.less(temp[i], temp[j])) {
                array[k] = (T)temp[i++];
            } else {
                array[k] = (T)temp[j++];
            }
        }
    }
    // 这样就去掉了边界判断
    // 还可以先判断待合并的子数组是否已经有序，如果有序则可以跳过排序直接按顺序合并即可。
    public static <T extends Comparable<T>> void quickMerge(T[] array,int l,int mid,int r) {
        // 三指针
        int i = l; // left
        int j = mid+1; // right
        int k = 0; // temp
        while (i <= mid && j <= r) {
            if(array[i].compareTo(array[j]) <= 0) {
                temp[k] = (String)array[i++];
            } else {
                temp[k] = (String)array[j++];
            }
        }
        while (i <= mid) {
            temp[k] = (String)array[i++];
        }
        while (j <= r) {
            temp[k] = (String)array[j++];
        }
        for (i = l; i <= r; i++) {
            array[i] = (T)temp[i-l];
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