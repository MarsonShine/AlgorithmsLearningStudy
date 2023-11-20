import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.ST;
import edu.princeton.cs.algs4.StdIn;
import edu.princeton.cs.algs4.StdOut;

public class SymbolDigraph {
    private ST<String, Integer> st; // 符号名 -> 索引
    private String[] keys; // 索引 -> 符号名
    private Digraph G; // 图
    // 根据文件和分隔符构造符号图
    public SymbolDigraph(String filename, String delim) {
        st = new ST<>();
        In in = new In(filename);
        while (in.hasNextLine()) {
            String[] a = in.readLine().split(delim);
            for (int i = 0; i < a.length; i++) {
                if (!st.contains(a[i])) {
                    st.put(a[i], st.size());
                }
            }
        }
        keys = new String[st.size()];
        for (String name : st.keys()) {
            keys[st.get(name)] = name;
        }

        // 第二遍，构造图
        G = new Digraph(st.size());
        in = new In(filename);
        while (in.hasNextLine()) {
            String[] a = in.readLine().split(delim);
            int v = st.get(a[0]);
            for (int i = 1; i < a.length; i++) {
                G.addEdge(v, st.get(a[i]));
            }
        }
    }

    // key 是一个顶点么
    public boolean contains(String key) {
        return st.contains(key);
    }

    // key 的索引
    public int index(String key) {
        return st.get(key);
    }

    // 索引 v 的定点名
    public String name(int v) {
        return keys[v];
    }

    public Digraph G() {
        return G;
    }

    public static void main(String[] args) {
        String filename = args[0];
        String delim = args[1];
        SymbolDigraph sg = new SymbolDigraph(filename, delim);
        Digraph G = sg.G();

        while (!StdIn.hasNextLine()) {
            String source = StdIn.readLine();
            for (int w : G.adj(sg.index(source))) {
                StdOut.println("   " + sg.name(w));
            }
        }
    }
}
