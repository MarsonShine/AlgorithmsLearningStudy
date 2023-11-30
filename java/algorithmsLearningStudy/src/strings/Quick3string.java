/**
 * 三向字符串快速排序
 * 
 * 三向字符串快速排序是对高位优先的字符串排序算法的一种改进，当字符串很长，随机性非常强，字母出现频率的分布不均匀且当该栏长度固定时字符串的末尾会被添
 * 加许多空格。这些限制使得高位优先的字符串排序算法会产生许多空子数组，这个时候就需要三向字符串快速排序。
 * 三向字符串快速排序：根据键的首字母进行三向切分，仅在中间子数组中的下一个字符（因为键的首字母都与切分字符相等）继续递归排序
 */
public class Quick3string {
    private static int chartAt(String s, int d) {
        if (d < s.length()) {
            return s.charAt(d);
        } else {
            return -1;
        }
    }

    public static void sort(String[] a, int l, int h, int d) {
        if (h < l) {
            return;
        }
        int lt = l, gt = h;
        int v = chartAt(a[l], d); // v 为索引 l 处字符串的第 d 个字符，基准字符
        int i = l + 1;
        while (i <= gt) {
            int t = chartAt(a[i], d);
            if (t < v) {
                SortHelper.exch(a, lt++, i++);
            } else if (t > v) {
                SortHelper.exch(a, i, gt--);
            } else {
                i++;
            }
        }
        // 调用递归函数 sort 对小于和大于基准字符的子数组进行排序。
        sort(a, l, lt - 1, d);
        if (v >= 0) {
            sort(a, lt, gt, d + 1);
        }
        sort(a, gt + 1, h, d);
    }
}
