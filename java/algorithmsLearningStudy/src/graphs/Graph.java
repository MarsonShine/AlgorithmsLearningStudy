import edu.princeton.cs.algs4.Bag;
import edu.princeton.cs.algs4.In;

public class Graph {
    private final int V;// 订单数目
    private int E; // 边数目
    private Bag<Integer>[] adj; // 邻接表
    // 图结构的一般方法定义

    public Graph(int v) {
        V = v;
        E = 0;
        adj = (Bag<Integer>[]) new Bag[V]; // 创建邻接表
        for (int i = 0; i < V; i++) { // 初始化
            adj[i] = new Bag<>();
        }
    }

    public Graph(In in) {
        this(in.readInt()); // 读取V并将图初始化
        int e = in.readInt(); // 读取E
        for (int i = 0; i < e; i++) {
            // 添加一条边
            int v = in.readInt(); // 读取一个顶点
            int w = in.readInt(); // 读取另一个顶点
            addEdge(v, w); // 添加一条连接到它们的边
        }
    }

    /**
     * 顶点数
     *
     * @param paramName description of parameter
     * @return description of return value
     */
    int V() {
        return V;
    }

    /**
     * 边数
     *
     * @param paramName description of parameter
     * @return description of return value
     */
    int E() {
        return E;
    }

    /**
     * 向图中添加一条边,v-w
     *
     * @param v the first vertex
     * @param w the second vertex
     */
    void addEdge(int v, int w) {
        adj[v].add(w);
        adj[w].add(v);
        E++;
    }

    /**
     * 和 v 相邻的所有顶点，图化
     *
     * @param v description of parameter
     * @return description of return value
     */
    Iterable<Integer> adj(int v) {
        return adj[v];
    }

    public String toString() {
        String s = V + " vertices, " + E + " edges \n";
        for (int v = 0; v < V; v++) {
            s += v + ": ";
            for (int w : adj(v)) {
                s += w + " ";
            }
            s += "\n";
        }
        return s;
    }

    public boolean hasEdge(int v, int w) {
        var edgeBag = adj[v];
        for (Integer edge : edgeBag) {
            if (edge == w) {
                return true;
            }
        }
        return false;
    }
}
