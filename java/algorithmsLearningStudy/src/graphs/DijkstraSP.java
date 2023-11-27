import edu.princeton.cs.algs4.Stack;

/**
 * 最短路径的 Dijkstra 算法
 */
public class DijkstraSP {
    private DirectedEdge[] edgeTo; // 从顶点 s 到顶点 v 的最短路径的最后一条边
    private double[] distTo; // 从顶点 s 到顶点 v 的最短路径的距离
    private IndexMinPQ<Double> pq; // 用于存储待处理的顶点及其对应的最短路径估计值。

    public DijkstraSP(EdgeWeightDigraph G, int s) {
        edgeTo = new DirectedEdge[G.V()];
        distTo = new double[G.V()];
        pq = new IndexMinPQ<>(G.V());

        for (int v = 0; v < G.V(); v++) {
            distTo[v] = Double.POSITIVE_INFINITY;
        }
        distTo[s] = 0;

        pq.insert(s, distTo[s]); // 初始化，将起点和对应的最短路径估计值插入到优先级队列中，方便后续处理
        while (!pq.isEmpty()) {
            relax(G, pq.delMin());
        }
    }
    /**
     * 松弛操作
     * @param g
     * @param v
     */
    private void relax(EdgeWeightDigraph g, int v) {
        // 取出的顶点v，遍历了从v出发的所有边。
        // 对于每条边e,获取其终点顶点w
        for (DirectedEdge e : g.adj(v)) {
            int w = e.to();
            // 判断是否可以通过顶点v松弛到顶点w，即通过v的路径更新到达w的最短路径估计值
            // 如果条件成立，则更新distTo[w]为更小的值，更新edgeTo[w]为边e，表示从起点到达w的最短路径的上一条边（即最后一条边）
            if (distTo[w] > distTo[v] + e.weight()) {
                distTo[w] = distTo[v] + e.weight();
                edgeTo[w] = e;
                // 如果顶点w已经存在于优先级队列中，则更新pq中w对应的最短路径估计值。否则就插入到pq中
                if (pq.contains(w)) { 
                    pq.change(w, distTo[w]);
                } else {
                    pq.insert(w, distTo[w]);
                }
            }
        }
    }

    /**
     * 求从 s 到 v 的最短路径的距离总和。
     * 如果不存在则无穷大
     * 
     * @param v
     * @return
     */
    public double distTo(int v) {
        return distTo[v];
    }

    /**
     * 是否存在从顶点 s 到顶点 v 的路径
     * 
     * @param v
     */
    public boolean hasPathTo(int v) {
        return distTo[v] < Double.POSITIVE_INFINITY;
    }

    /**
     * 从顶点 s 到顶点 v 的路径，如果不存在则返回null
     * 
     * @param v
     * @return
     */
    public Iterable<DirectedEdge> pathTo(int v) {
        if (!hasPathTo(v)) {
            return null;
        }
        Stack<DirectedEdge> path = new Stack<>();
        for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.from()]) {
            path.push(e);
        }
        return path;
    }
}
