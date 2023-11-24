import edu.princeton.cs.algs4.MinPQ;
import edu.princeton.cs.algs4.Queue;
/**
 * 最小生成树的 Prim 算法的延时实现;
 * 
 * 横切边的概念：
 * 横切边（Crossing Edge）是指连接两个不同的连通分量的边。当一个图由多个连通分量组成时，横切边是穿过不同连通分量之间的边。
 * 举个例子：
 *  A --- B    E --- F
    |     |    |
    C --- D    G
    上图中，有两个连通分量：{A, B, C, D} 和 {E, F, G}。那么连接这两个连通分量的边就是横切边。在这个例子中，边 {D, E} 是横切边，因为它连接了第一个连通分量中的顶点 D 和第二个连通分量中的顶点 E。
 */
public class LazyPrimPST {
    private boolean[] marked; // 最小生成树的顶点
    private Queue<Edge> mst; // 最小生成树的边
    private MinPQ<Edge> pq; // 横切边（包括失效的边），优先级队列

    public LazyPrimPST(EdgeWeightedGraph G) {
        pq = new MinPQ<>();
        marked = new boolean[G.V()];
        mst = new Queue<>();

        visit(G, 0); // 假设G是连通的，校验代码忽略
        while (!pq.isEmpty()) {
            Edge e = pq.delMin(); // 从优先级队列中取出权重最小的横切边
            int v = e.either(), w = e.other(v);
            if (marked[v] && marked[w]) {
                continue; // 该横切边已经在最小生成树中，跳过
            }
            mst.enqueue(e); // 否则，将横切边加入最小生成树
            if (!marked[v]) {  // 将顶点v/w加入到树中
                visit(G, v);
            }
            if (!marked[w]) {
                visit(G, w);
            }
        }
    }

    private void visit(EdgeWeightedGraph G, int v) {
        // 标记顶点v并将所有连接v和未标记顶点的边加入pq
        marked[v] = true;
        for (Edge e : G.adj(v)) {
            int w = e.other(v);
            if (!marked[w]) { // 防止重复加入
                pq.insert(e);
            }
        }
    }
}
