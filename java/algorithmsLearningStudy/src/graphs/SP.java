import edu.princeton.cs.algs4.StdOut;

/**
 * 最短路径
 */
public class SP {
    public SP(EdgeWeightDigraph G, int s) {

    }

    /**
     * 求从 s 到 v 的最短路径的距离总和。
     * 如果不存在则无穷大
     * 
     * @param v
     * @return
     */
    public double distTo(int v) {

    }

    /**
     * 是否存在从顶点 s 到顶点 v 的路径
     * 
     * @param v
     */
    public boolean hasPathTo(int v) {

    }

    /**
     * 从顶点 s 到顶点 v 的路径，如果不存在则返回null
     * 
     * @param v
     * @return
     */
    public Iterable<DirectedEdge> pathTo(int v) {

    }

    public static void main(String[] args) {
        EdgeWeightDigraph G = new EdgeWeightDigraph(10);
        G.addEdge(new DirectedEdge(4, 5, 0.35));
        G.addEdge(new DirectedEdge(5, 4, 0.35));
        G.addEdge(new DirectedEdge(4, 7, 0.37));
        G.addEdge(new DirectedEdge(5, 7, 0.28));
        G.addEdge(new DirectedEdge(7, 5, 0.28));
        G.addEdge(new DirectedEdge(5, 1, 0.32));
        G.addEdge(new DirectedEdge(0, 4, 0.38));
        G.addEdge(new DirectedEdge(0, 2, 0.26));
        G.addEdge(new DirectedEdge(7, 3, 0.39));
        G.addEdge(new DirectedEdge(1, 3, 0.29));
        int s = 10;
        SP sp = new SP(G, s);
        for (int t = 0; t < G.V(); t++) {
            StdOut.print(s + " to " + t);
            StdOut.printf(" (%4.2f): ", sp.distTo(t));
            if (sp.hasPathTo(t))
                for (DirectedEdge e : sp.pathTo(t))
                    StdOut.print(e + " ");
            StdOut.println();
        }
    }
}
