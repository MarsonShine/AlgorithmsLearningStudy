/**
 * 高位优先的字符串排序
 */
public class MSD {
    private static int R = 256; // 基数
    private static final int M = 15; // 小数组的切换阈值
    private static String[] aux; // 数据分类的辅助数组

    private static int charAt(String s, int d) {
        if (d < s.length())
            return s.charAt(d);
        else
            return -1;
    }

    public static void sort(String[] a) {
        int N = a.length;
        aux = new String[N];
        sort(a, 0, N - 1, 0);
    }

    private static void sort(String[] a, int lo, int hi, int d) {
        // 以第d个字符为键将a[lo]至a[hi]排序
        if (hi <= lo + M) {
            Insertion.sort(a, lo, hi);
            return;
        }
        // 计算频率
        int[] count = new int[R + 2];
        for (int i = lo; i <= hi; i++)
            count[charAt(a[i], d) + 2]++;
        // 将频率转换为索引
        for (int r = 0; r < R + 1; r++)
            count[r + 1] += count[r];
        // 将元素分类
        for (int i = lo; i <= hi; i++)
            aux[count[charAt(a[i], d) + 1]++] = a[i];
        // 回写
        for (int i = lo; i <= hi; i++)
            a[i] = aux[i - lo];

        // 递归以每个字符为键进行排序
        for (int r = 0; r < R; r++)
            sort(a, lo + count[r], lo + count[r + 1] - 1, d + 1);
    }
}
