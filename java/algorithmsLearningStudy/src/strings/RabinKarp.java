public class RabinKarp {
    private static final int R = 256;
    private String pat; // 模式字符串（仅拉斯维加斯算法需要）
    private long patHash; // 模式字符串的散列值
    private int M; // 模式字符串的长度
    private long Q; // 一个很大的素数
    private long RM; // R^(M-1) % Q

    public RabinKarp(String pat) {
        this.pat = pat;
        M = pat.length();
        Q = 1000000007;
        RM = 1;
        for (int i = 1; i <= M - 1; i++) // 计算R^(M-1) % Q
            RM = (RM * R) % Q; // 用于减去第一个数字时的计算
        patHash = hash(pat, M);
    }

    private long hash(String key, int M) {
        // 计算key[0..M-1]的散列值
        long h = 0;
        for (int j = 0; j < M; j++)
            h = (R * h + key.charAt(j)) % Q;
        return h;
    }

    public int search(String txt) {
        // 在 txt 中查找模式串 pat
        int N = txt.length();
        long txtHash = hash(txt, M);
        if (txtHash == patHash && pat.equals(txt.substring(0, M)))
            return 0;
        for (int i = M; i < N; i++) {
            // 减去第一个数字，加上最后一个数字，再次检查匹配
            txtHash = (txtHash + Q - RM * txt.charAt(i - M) % Q) % Q;
            txtHash = (txtHash * R + txt.charAt(i)) % Q;
            if (txtHash == patHash && pat.equals(txt.substring(i - M + 1, i + 1)))
                return i - M + 1;
        }
        return N;
    }
}
