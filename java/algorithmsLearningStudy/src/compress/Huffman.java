import edu.princeton.cs.algs4.BinaryStdIn;
import edu.princeton.cs.algs4.BinaryStdOut;
import edu.princeton.cs.algs4.MinPQ;

/**
 * 霍夫曼压缩算法就是用较少的比特来表示出现频繁的字符，用较多的比特来表示不出现频繁的字符。
 * 所以霍夫曼算法首先要知道目标字符串出现的频率，所以它需要两轮扫描：第一轮扫描是将字符及其频率，然后再是读取一边进行压缩操作。
 */
public class Huffman {
    private static int R = 256; // 基数

    private static class Node implements Comparable<Node> {
        // 霍夫曼单词查找树中的节点
        public char ch; // 内部节点不会使用该变量
        private int freq; // 展开过程不会使用该变量
        public Node left, right;

        Node(char ch, int freq, Node left, Node right) {
            this.ch = ch;
            this.freq = freq;
            this.left = left;
            this.right = right;
        }

        public boolean isLeaf() {
            return left == null && right == null;
        }

        public int compareTo(Node that) {
            return this.freq - that.freq;
        }
    }

    public static void expand() {
        Node root = readTrie();
        int N = BinaryStdIn.readInt();
        for (int i = 0; i < N; i++) { // 展开第i个编码所对应的字母
            Node x = root;
            while (!x.isLeaf())
                if (BinaryStdIn.readBoolean())
                    x = x.right;
                else
                    x = x.left;
            BinaryStdOut.write(x.ch);
        }
        BinaryStdOut.close();
    }

    private static Node readTrie() {
        if (BinaryStdIn.readBoolean())
            return new Node(BinaryStdIn.readChar(), 0, null, null);
        return new Node('\0', 0, readTrie(), readTrie());
    }

    private static Node readTrie(int[] freq) {
        MinPQ<Node> pq = new MinPQ<>();
        for (char c = 0; c < R; c++) {
            if (freq[c] > 0) {
                pq.insert(new Node(c, freq[c], null, null));
            }
        }

        while (pq.size() > 1) {
            // 合并两颗频率最小的树
            Node x = pq.delMin();
            Node y = pq.delMin();
            Node parent = new Node('\0', x.freq + y.freq, x, y);
            pq.insert(parent);
        }

        return pq.delMin();
    }

    private static String[] buildCode(Node root) {
        // 使用单词查找树来构造编码表
        String[] st = new String[R];
        buildCode(st, root, "");
        return st;
    }

    private static void buildCode(String[] st, Node x, String s) {
        if (x.isLeaf()) {
            st[x.ch] = s;
            return;
        } else {
            buildCode(st, x.left, s + "0");
            buildCode(st, x.right, s + "1");
        }
    }

    public static void compress() {
        // 读取输入
        String s = BinaryStdIn.readString();
        char[] input = s.toCharArray();
        // 统计频率
        int[] freq = new int[R];
        for (char c : input) {
            freq[c]++;
        }
        // 构造霍夫曼编码树
        Node root = readTrie(freq);
        // 递归构造编译符号表
        String[] st = buildCode(root);
        // 递归打印解码用的单词查找树
        writeTrie(root);
        // 打印字符总数
        BinaryStdOut.write(input.length);
        // 使用霍夫曼编码处理输入
        for (char c : input) {
            String code = st[c];
            for (int i = 0; i < code.length(); i++) {
                if (code.charAt(i) == '0') {
                    BinaryStdOut.write(false);
                } else {
                    BinaryStdOut.write(true);
                }
            }
        }
        BinaryStdOut.close();
    }

    private static void writeTrie(Node x) {
        // 输出单词查找树的比特字符串
        if (x.isLeaf()) {
            BinaryStdOut.write(true);
            BinaryStdOut.write(x.ch);
            return;
        }
        BinaryStdOut.write(false);
        writeTrie(x.left);
        writeTrie(x.right);
    }
}
