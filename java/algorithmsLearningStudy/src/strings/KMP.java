import edu.princeton.cs.algs4.StdOut;

public class KMP {
    private String pat;
    private int[][] dfa;

    public KMP(String pat) {
        this.pat = pat;
        int M = pat.length();
        int R = 256;
        dfa = new int[R][M];
        dfa[pat.charAt(0)][0] = 1;
        for (int X = 0, j = 1; j < M; j++) {
            // 计算 dfa[][j]
            for (int c = 0; c < R; c++)
                dfa[c][j] = dfa[c][X]; // 赋值匹配失败情况下的值
            dfa[pat.charAt(j)][j] = j + 1; // 设置匹配成功情况下的值
            X = dfa[pat.charAt(j)][X]; // 更新重启状态
        }
    }

    public int search(String txt) {
        int i, j, N = txt.length(), M = pat.length();
        for (i = 0, j = 0; i < N && j < M; i++) {
            char c = txt.charAt(i);
            StdOut.print(" dfa['" + c + "'][" + j + "]=" + dfa[c][j]);
            j = dfa[c][j];
        }
        if (j == M) {
            return i - M; // 匹配成功
        } else {
            return N; // 匹配失败
        }
    }

    public static void main(String[] args) {
        String pat = "AACAA";
        String txt = "AABRAACADABRAACAADABRA";
        KMP kmp = new KMP(pat);
        StdOut.println("text:    " + txt);
        int offset = kmp.search(txt);
        StdOut.print("pattern: ");
        for (int i = 0; i < offset; i++)
            StdOut.print(" ");
        StdOut.println(pat);
    }
}
