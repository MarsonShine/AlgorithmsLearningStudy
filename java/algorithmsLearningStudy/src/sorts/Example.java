package sorts;

import edu.princeton.cs.algs4.StdIn;
import edu.princeton.cs.algs4.StdOut;

public class Example {
    public static void sort(Comparable<String>[] a) {
        String s=  "";
    }

    private static <T extends Comparable<String>> boolean less(T v, T w) {
        return v.compareTo((String)w) < 0;
    }

    private static void exch(Comparable<String>[] a, int i, int j) {
        Comparable<String> t = a[i];
        a[i] = a[j];
        a[j] = t;
    }

    private static void show(Comparable<String>[] a) { // 在单行中打印数组
        for (int i = 0; i < a.length; i++)
            StdOut.print(a[i] + " ");
        StdOut.println();
    }

    public static boolean isSorted(Comparable<String>[] a) {
        for (int i = 1; i < a.length; i++) {
            if (less(a[i], a[i - 1])) {
                return false;
            }
        }
        return true;
    }

    public static void main(String[] args) {
        String[] a = StdIn.readAllStrings();
        sort(a);
        assert isSorted(a);
        show(a);
    }
}
