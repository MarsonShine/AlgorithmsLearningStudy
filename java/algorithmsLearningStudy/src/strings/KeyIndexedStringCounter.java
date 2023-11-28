/**
 * 字符串的键索引计数法
 */
public class KeyIndexedStringCounter {
    public static void keyIndexedCount(String[] a, int R) {
        int N = a.length;
        String[] aux = new String[N];
        int[] count = new int[R + 1]; // 频率统计
        // 计算频率
        for (int i = 0; i < N; i++) {
            count[a[i].length() + 1]++;
        }

        // 将频率转换为索引
        // 这一步骤通过 count[r + 1] += count[r] 实现，将 count 数组中的元素累加，使得 count[r + 1] 存储键小于等于 r 的字符串在排序后的数组中的结束索引。
        for (int r = 0; r < R; r++)
            count[r + 1] += count[r]; // 将当前位置 r+1 的元素的值，更新为当前位置及之前所有元素的总和。换句话说，这行代码将数组 count 转换为一个累加数组。累加数组的性质是，每个位置上的值等于原数组中对应位置及之前所有元素的和。

        // 将元素分类
        // 将每个字符串根据其键的频率存储到辅助数组 aux 中的正确位置上
        for (int i = 0; i < N; i++)
            aux[count[a[i].length()]++] = a[i];

        // 回写
        for (int i = 0; i < N; i++)
            a[i] = aux[i];
    }

    public static void main(String[] args) {
        String[] strings = { "apple", "banana", "cherry", "date", "elderberry" };
        int R = 11;

        System.out.println("Before sorting:");
        for (String s : strings) {
            System.out.println(s);
        }

        keyIndexedCount(strings, R);

        System.out.println("\nAfter sorting:");
        for (String s : strings) {
            System.out.println(s);
        }
    }
}
