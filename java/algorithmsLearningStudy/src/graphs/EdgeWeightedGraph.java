import edu.princeton.cs.algs4.Bag;
import edu.princeton.cs.algs4.In;

/**
 * 加权无向图
 */
public class EdgeWeightedGraph {
    private final int V; // 顶点总和
    private int E; // 边总和
    private Bag<Edge>[] adj; // 邻接表

    /**
     * 创建一副含有V个顶点的空图
     * 
     * @param V
     */
    public EdgeWeightedGraph(int V) {
        this.V = V;
        this.E = 0;
        adj = (Bag<Edge>[]) new Bag[V];
        for (int v = 0; v < V; v++) {
            adj[v] = new Bag<>();
        }
    }

    /**
     * 从输入流中读取图
     * 
     * @param in
     */
    public EdgeWeightedGraph(In in) {
        this(in.readInt());
        int E = in.readInt();
        for (int i = 0; i < E; i++) {
            int v = in.readInt();
            int w = in.readInt();
            double weight = in.readDouble();
            Edge e = new Edge(v, w, weight);
            addEdge(e);
        }
    }

    public int V() {
        return V;
    }

    public int E() {
        return E;
    }

    /**
     * 添加一条边
     * 
     * @param e
     */
    public void addEdge(Edge e) {
        int v = e.either();
        int w = e.other(v);
        adj[v].add(e);
        adj[w].add(e);
        E++;
    }

    /**
     * 返回与顶点v相邻的所有边
     * 
     * @param v
     * @return
     */
    public Iterable<Edge> adj(int v) {
        return adj[v];
    }

    /**
     * 返回图中所有边
     * 
     * @return
     */
    public Iterable<Edge> edges() {
        Bag<Edge> bag = new Bag<>();
        for (int v = 0; v < V; v++) {
            for (Edge e : adj(v)) {
                if (e.other(v) > v) { // 为了防止重复添加边（v.other(w) 与 w.other(v) 的情况）
                    bag.add(e);
                }
            }
        }
        return bag;
    }

    public String toString() {
        StringBuilder s = new StringBuilder();
        s.append(V + " vertices, " + E + " edges\n");
        for (int v = 0; v < this.V; v++) {
            s.append(v + ": ");
            for (Edge e : adj(v)) {
                s.append(e + " ");
            }
            s.append("\n");
        }
        return s.toString();
    }
}
