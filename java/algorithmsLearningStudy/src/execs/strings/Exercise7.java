package strings;

import edu.princeton.cs.algs4.Queue;

@SuppressWarnings("unused")
public class Exercise7 {
    public static void keyIndexedCountWithQueue(String[] a, int r) {
        int size = 256;
        Queue<String> count[] = new Queue[size + 1];

        for (int i = 0; i < count.length; i++) {
            count[i] = new Queue<>();
        }

        for (int i = size - 1; i >= 0; i--) {
            // 计算频率
            for (int j = 0; j < a.length; j++) {
                int index = a[j].charAt(i);
                count[index].enqueue(a[j]);
            }

            // 将频率转换为索引和回写
            int indexArray = 0;
            for (int j = 0; j < count.length; j++) {
                while (!count[j].isEmpty()) {
                    a[indexArray++] = count[j].dequeue();
                }
            }
        }
    }
}
