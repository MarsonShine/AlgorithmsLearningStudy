import edu.princeton.cs.algs4.Bag;

/**
 * 加权有向图
 */
public class EdgeWeightDigraph {
    private int V; // 顶点总数
    private int E; // 边总数
    private Bag<DirectedEdge>[] adj; // 邻接表

    /**
     * 含有V个顶点的空有向图
     * 
     * @param V
     */
    public EdgeWeightDigraph(int V) {
        this.V = V;
        this.E = 0;
        adj = (Bag<DirectedEdge>[]) new Bag[V];
        for (int v = 0; v < V; v++) {
            adj[v] = new Bag<DirectedEdge>();
        }
    }

    public int V() {
        return V;
    }

    public int E() {
        return 0;
    }

    /**
     * 将 e 添加到该有向图中
     * 
     * @param e
     */
    public void addEdge(DirectedEdge e) {
        adj[e.from()].add(e);
        E++;
    }

    /**
     * 从 v 指出的所有边
     * 
     * @param v
     * @return
     */
    public Iterable<DirectedEdge> adj(int v) {
        return adj[v];
    }

    /**
     * 该有向图中的所有边
     * 
     * @return
     */
    public Iterable<DirectedEdge> edges() {
        Bag<DirectedEdge> list = new Bag<DirectedEdge>();
        for (int v = 0; v < V; v++) {
            for (DirectedEdge e : adj[v]) {
                list.add(e);
            }
        }
        return list;
    }
}
