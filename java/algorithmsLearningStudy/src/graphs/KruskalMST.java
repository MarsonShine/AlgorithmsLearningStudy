import edu.princeton.cs.algs4.MinPQ;
import edu.princeton.cs.algs4.Queue;
import edu.princeton.cs.algs4.UF;

/**
 * 最小生成树的 Kruskal 算法
 * 
 * 主要思想是按照边的权重顺序（从小到大）处理它们，将边加入最小生成树中，加入的边不会与已经加入的边构成环，直到树中含有 V-1
条边为止。
 */
public class KruskalMST {
    private Queue<Edge> mst; // 保存最小生成树的边

    public KruskalMST(EdgeWeightedGraph G) {
        mst = new Queue<>();
        MinPQ<Edge> pq = new MinPQ<>();
        for (Edge edge : G.edges()) {
            pq.insert(edge);
        }
        UF uf = new UF(G.V());

        while (!pq.isEmpty() && mst.size() < G.V() - 1) {
            Edge e = pq.delMin(); // 从优先级队列中取出权重最小的横切边
            int v = e.either(), w = e.other(v);
            // 如果边的两个顶点不在同一个连通分量中，则加入最小生成树
            if (uf.find(v) == uf.find(w)) {
                continue; // 该横切边已经在最小生成树中，跳过
            }
            uf.union(v, w); // 合并分量
            mst.enqueue(e); // 将横切边加入最小生成树
        }
    }

    public Iterable<Edge> edges() {
        return mst;
    }

    public double weight() {
        double weight = 0.0;
        for (Edge e : mst) {
            weight += e.weight();
        }
        return weight;
    }
}
