import edu.princeton.cs.algs4.Queue;

public class BreadthFirstPaths {
    private boolean marked[]; // 标记节点是否被访问
    private int[] edgeTo; // 记录从起点到某个节点的路径
    private final int s; // 起点

    public BreadthFirstPaths(Graph g, int s) {
        marked = new boolean[g.V()];
        edgeTo = new int[g.V()];
        this.s = s;
        bfs(g, s);
    }

    /**
     * Performs a breadth-first search on the given graph starting from the specified source vertex.
     *
     * @param  g  the graph on which to perform the breadth-first search
     * @param  s  the source vertex from which to start the search
     */
    private void bfs(Graph g, int s) {
        Queue<Integer> queue = new Queue<>();
        marked[s] = true;
        queue.enqueue(s);
        while (!queue.isEmpty()) {
            int v = queue.dequeue(); // 从队列中取出一个节点
            for (int w : g.adj(v)) { // 找到该顶点相邻的顶点
                if (!marked[w]) {
                    edgeTo[w] = v;
                    marked[w] = true;
                    queue.enqueue(w);
                }
            }
        }
    }

    public boolean hasPathTo(int v) {
        return marked[v];
    }

    public Iterable<Integer> pathTo(int v) {
        if (!hasPathTo(v)) {
            return null;
        }
        Queue<Integer> path = new Queue<>();
        for (int x = v; x != s; x = edgeTo[x]) {
            path.enqueue(x);
        }
        path.enqueue(s);
        return path;
    }
}
