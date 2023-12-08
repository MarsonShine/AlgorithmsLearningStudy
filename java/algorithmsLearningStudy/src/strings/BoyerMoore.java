import edu.princeton.cs.algs4.StdOut;

public class BoyerMoore {
    private int[] right;
    private String pat;

    BoyerMoore(String pat) {
        // 计算跳转表
        this.pat = pat;
        int M = pat.length();
        int R = 256;
        right = new int[R];
        // 初始化
        for (int c = 0; c < R; c++)
            right[c] = -1;
        // 计算
        for (int j = 0; j < M; j++)
            right[pat.charAt(j)] = j;
    }

    public int search(String txt) {
        // 在 txt 中查找模式串 pat
        int N = txt.length();
        int M = pat.length();
        int skip;
        for (int i = 0; i <= N - M; i += skip) {
            skip = 0;
             for (int j = M - 1; j >= 0; j--) { // 从模式串的尾部开始，向前比较字符
                // 比较模式串 pat 的当前字符 pat.charAt(j) 和文本字符串 txt 中相应位置的字符 txt.charAt(i + j)。如果它们不相等，则表示当前位置不是一个匹配点。
                if (pat.charAt(j) != txt.charAt(i + j)) {
                    // 当出现不匹配时，需要进行以下逻辑
                    // 1. 使用 txt.charAt(i + j) 作为索引，查找一个预先计算好的数组 right 中的值，即 right[txt.charAt(i + j)]。这个数组存储每个字符在模式串中最右出现的位置。
                    // 2. 计算需要跳过的字符数 skip。它的值是 j - right[txt.charAt(i + j)]，表示将模式串向右移动的距离。
                    // 3. 使用 Math.max(1, skip) 来确保 skip 的值至少为1，以避免出现负数。
                    skip = Math.max(1, j - right[txt.charAt(i + j)]);
                    break;
                }
            }
            if (skip == 0) // 找到匹配
                return i;
        }
        return N; // 未找到匹配
    }

    public static void main(String[] args) {
        String pat = "NEEDLE";
        String txt = "FINDINAHAYSTACKNEEDLEINA";
        BoyerMoore kmp = new BoyerMoore(pat);
        StdOut.println("text:    " + txt);
        int offset = kmp.search(txt);
        StdOut.print("pattern: ");
        for (int i = 0; i < offset; i++)
            StdOut.print(" ");
        StdOut.println(pat);
    }
}
