import edu.princeton.cs.algs4.StdOut;

/**
 * 拓扑排序
 * 当且仅当图中不存在有向环时才能进行拓扑排序
 */
public class Topological {
    private Iterable<Integer> order;

    public Topological(Digraph g) {
        DirectedCycle cycleFinder = new DirectedCycle(g);
        if (!cycleFinder.hasCycle()) {
            DepthFirstOrder dfs = new DepthFirstOrder(g);
            order = dfs.reversePost();
        }
    }

    /**
     * 判断是否是有向无环图
     * 
     * @return
     */
    public boolean isDAG() {
        return order != null;
    }

    /**
     * 拓扑排序的所有顶点
     * 
     * @return
     */
    public Iterable<Integer> order() {
        return order;
    }

    public static void main(String[] args) {
        String filename = args[0];
        String separator = args[1];
        SymbolDigraph sg = new SymbolDigraph(filename, separator);
        Topological top = new Topological(sg.G());
        for (int v : top.order()) {
            StdOut.println(sg.name(v));
        }
    }
}
