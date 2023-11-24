import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.Queue;
import edu.princeton.cs.algs4.StdOut;

/**
 * 最小生成树
 */
public class MST {
    private Queue<Edge> mst; // 保存最小生成树的边
    private double weight; // 最小生成树的权重

    public MST(EdgeWeightedGraph G) {
        mst = new Queue<>();
        weight = 0.0;
        // 使用适当的算法计算最小生成树
        // 这里需要根据具体的算法实现来完成构造函数的代码
    }

    /**
     * 最小生成树的所有边
     * 
     * @return
     */
    Iterable<Edge> edges() {
        return mst;
    }

    /**
     * 最小生成树的权重
     * 
     * @return
     */
    public double weight() {
        return weight;
    }

    public static void main(String[] args) {
        In in = new In(args[0]);
        EdgeWeightedGraph G = new EdgeWeightedGraph(in);

        MST mst = new MST(G);
        for (Edge e : mst.edges()) {
            StdOut.println(e);
        }
        StdOut.println(mst.weight());
    }
}
