import edu.princeton.cs.algs4.Stack;

public class DepthFirstPaths {
    private boolean[] marked; // 标记是否已经到达过
    private int[] edgeTo; // 从起点到一个顶点的已知路径上的最后一个顶点，edgeTo[w]=v 表示v-w是第一次访问 w 时经过的边
    private final int s; // 起点

    public DepthFirstPaths(Graph g, int s) {
        marked = new boolean[g.V()];
        edgeTo = new int[g.V()];
        this.s = s;
        dfs(g, s);
    }

    private void dfs(Graph g, int v) {
        marked[v] = true;
        for (int w : g.adj(v)) {
            if (!marked[w]) {
                edgeTo[w] = v;
                dfs(g, w);
            }
        }
    }

    public boolean hasPathTo(int v) {
        return marked[v];
    }

    /**
     * 返回从起点到指定顶点经过的路径。
     *
     * @param v the vertex to find the path to
     * @return an iterable containing the path to the vertex, or null if no path
     *         exists
     */
    public Iterable<Integer> pathTo(int v) {
        if (!hasPathTo(v)) {
            return null;
        }
        Stack<Integer> path = new Stack<>();
        for (int x = v; x != s; x = edgeTo[x]) {
            path.push(x);
        }
        path.push(s);
        return path;
    }
}