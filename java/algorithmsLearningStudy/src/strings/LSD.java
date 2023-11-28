/**
 * 低位优先的字符串排序
 */
public class LSD {
    public static void sort(String[] a, int W) {
        // 通过前W个字符对a进行排序
        int N = a.length;
        int R = 256;
        String[] aux = new String[N];
        // 根据第d个字符用键索引计数法排序
        for (int d = W - 1; d >= 0; d--) {
            int[] count = new int[R + 1]; // 计算出频率
            for (int i = 0; i < N; i++) {
                count[a[i].charAt(d) + 1]++;
            }
            for (int r = 0; r < R; r++) {
                count[r + 1] += count[r]; // 将频率转换为索引
            }
            for (int i = 0; i < N; i++) { // 将元素分类
                aux[count[a[i].charAt(d)]++] = a[i];
            }
            for (int i = 0; i < N; i++) { // 回写
                a[i] = aux[i];
            }
        }
    }

    public static void main(String[] args) {
        String[] strings = { "4PGC938", "2IYE230", "3CIO720", "1ICK750", "1OHV845", "4JZY524", "1ICK750", "3CIO720",
                "1OHV845", "1OHV845", "2RLA629", "2RLA629", "3ATW723" };

        System.out.println("Before sorting:");
        for (String s : strings) {
            System.out.println(s);
        }

        sort(strings, 7);

        System.out.println("\nAfter sorting:");
        for (String s : strings) {
            System.out.println(s);
        }
    }
}
