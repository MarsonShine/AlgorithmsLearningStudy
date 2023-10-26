import edu.princeton.cs.algs4.StdOut;
import edu.princeton.cs.algs4.StdRandom;
import edu.princeton.cs.algs4.Stopwatch;

public class SortCompare {
    public static <T extends Comparable<T>> double time(String alg, T[] a) {
        Stopwatch timer = new Stopwatch();
        if (alg.equals("Insertion"))
            Insertion.sort(a);
        if (alg.equals("Selection"))
            Selection.sort(a);
        return timer.elapsedTime();
    }

    public static <T extends Comparable<T>> double timeRamdonInput(String alg, int l, int n) {
        // 使用alg算法将n个长度为l的数组排序
        double total = 0.0;
        Double[] a = new Double[l];
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < l; j++) {
                a[j] = StdRandom.uniformDouble();
            }
            total += time(alg, a);
        }
        return total;
    }

    public static void main(String[] args) {
        String alg1 = args[0];
        String alg2 = args[1];
        int n = Integer.parseInt(args[2]);
        int l = Integer.parseInt(args[3]);
        double t1 = timeRamdonInput(alg1, l, n);
        double t2 = timeRamdonInput(alg2, l, n);
        StdOut.printf("For %d random Doubles\n %s is", n, alg1);
        StdOut.printf("%.1f times faster than %s\\n", t2 / t1, alg2);
    }
}