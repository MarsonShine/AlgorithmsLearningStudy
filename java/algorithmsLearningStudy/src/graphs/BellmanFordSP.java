import edu.princeton.cs.algs4.DirectedEdge;
import edu.princeton.cs.algs4.EdgeWeightedDigraph;
import edu.princeton.cs.algs4.EdgeWeightedDirectedCycle;
import edu.princeton.cs.algs4.Queue;

/**
 * 基于队列的 Bellman-Ford 算法
 */
public class BellmanFordSP {
    private double[] distTo; // 从起点到某个顶点的路径长度
    private DirectedEdge[] edgeTo; // 从起点到某个顶点的最后一条边
    private boolean[] onQ; // 该顶点是否存在于队列中
    private Queue<Integer> queue; // 正在被放松的顶点
    private int cost; // relax()的调用次数
    private Iterable<DirectedEdge> cycle; // edgeTo[]中的是否有负权重环

    public BellmanFordSP(EdgeWeightedDigraph G, int s) {
        distTo = new double[G.V()];
        edgeTo = new DirectedEdge[G.V()];
        onQ = new boolean[G.V()];
        queue = new Queue<>();
        for (int v = 0; v < G.V(); v++) {
            distTo[v] = Double.POSITIVE_INFINITY;
        }
        distTo[s] = 0.0;
        queue.enqueue(s);
        onQ[s] = true;
        while (!queue.isEmpty() && !hasNegativeCycle()) {
            int v = queue.dequeue();
            onQ[v] = false;
            relax(G, v);
        }
    }

    private void relax(EdgeWeightedDigraph G, int v) {
        for (DirectedEdge e : G.adj(v)) {
            int w = e.to();
            if (distTo[w] > distTo[v] + e.weight()) {
                distTo[w] = distTo[v] + e.weight();
                edgeTo[w] = e;
                if (!onQ[w]) {
                    queue.enqueue(w);
                    onQ[w] = true;
                }
            }
            // 码检查了一个计数器（cost）是否达到了 G.V() 的倍数。如果是，说明进行了足够多的松弛操作，此时可能存在负权重环。通过调用 findNegativeCycle() 方法来检测是否存在负权重环。
            if (cost++%G.V() == 0) {
                findNegativeCycle();
            }
        }
    }

    public void findNegativeCycle() {
        int v = edgeTo.length;
        EdgeWeightedDigraph spt = new EdgeWeightedDigraph(v);
        for (int i = 0; i < v; i++) {
            if (edgeTo[i] != null) {
                spt.addEdge(edgeTo[i]);
            }
        }
        EdgeWeightedDirectedCycle finder = new EdgeWeightedDirectedCycle(spt);
        cycle = finder.cycle();
    }

    public boolean hasNegativeCycle() {
        return cycle != null;
    }

    public Iterable<DirectedEdge> negativeCycle() {
        return cycle;
    }
}
