import edu.princeton.cs.algs4.Bag;
import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.StdOut;

/**
 * 有向图的应用。
 * 【单点可达性】：从一个顶点出发到达另一个顶点的有向路径
 * 【多点可达性】：给定一个有向图和顶点的集合，可以实现“是否存在一条边从集合中的任意一个顶点到达给定的顶点v的有向路径”问题
 * 【单点有向路径】：给定一幅有向图和一个起点 s ，实现“从 s 到给定目的顶点 v 是否存在一条有向路径”
 * 【单点最短有向路径】
 */
public class DirectedDFS {
    private boolean[] marked;

    /**
     * 在 g 中找到从 s 点可达的所有顶点
     * 
     * @param g
     * @param s
     */
    public DirectedDFS(Digraph g, int s) {
        marked = new boolean[g.V()];
        dfs(g, s);
    }

    /**
     * 在 g 中找到从 sources 中的所有顶点可达的所有顶点。
     * 【多点可达性】：给定一个有向图和顶点的集合，可以实现“是否存在一条边从集合中的任意一个顶点到达给定的顶点v的有向路径”问题
     * 
     * @param g
     * @param sources
     */
    public DirectedDFS(Digraph g, Iterable<Integer> sources) {
        marked = new boolean[g.V()];
        for (int s : sources) {
            if (!marked(s)) {
                dfs(g, s);
            }
        }
    }

    public void dfs(Digraph g, int v) {
        marked[v] = true;
        for (int w : g.adj(v)) {
            if (!marked(w))
                dfs(g, w);
        }
    }

    /**
     * 订单v是否可达
     * 
     * @param v
     * @return
     */
    public boolean marked(int v) {
        return marked(v);
    }

    public static void main(String[] args) {
        Digraph g = new Digraph(new In(args[0]));
        Bag<Integer> sources = new Bag<Integer>();
        for (int i = 1; i < args.length; i++)
            sources.add(Integer.parseInt(args[i]));

        DirectedDFS reachable = new DirectedDFS(g, sources);
        for (int v = 0; v < g.V(); v++)
            if (reachable.marked(v))
                StdOut.print(v + " ");
        StdOut.println();
    }
}
