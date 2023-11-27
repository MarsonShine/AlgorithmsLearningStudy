import edu.princeton.cs.algs4.Stack;

/**
 * 有向无环图
 */
public class DirectedCycle {
    private boolean[] marked;
    private int[] edgeTo;
    private Stack<Integer> cycle; // 有向环中的所有顶点（如果存在）
    private boolean[] onStack; // 递归调用的栈上的所有顶点

    public DirectedCycle(Digraph G){
        onStack = new boolean[G.V()];
        edgeTo = new int[G.V()];
        marked = new boolean[G.V()];
        for (int v = 0; v < G.V(); v++)
        if (!marked[v])
            dfs(G, v);
    }

    private void dfs(Digraph G, int v){
        marked[v] = true;
        onStack[v] = true;
        for (int w : G.adj(v)){
            if (hasCycle()) return;
            else if (!marked[w]){
                edgeTo[w] = v;
                dfs(G, w);
            }
            else if (onStack[w]){ // 检测到有环
                cycle = new Stack<>();
                // 并从顶点 v 开始，通过追踪 edgeTo 数组中的边，将环路的顶点依次放入栈中。最后，将起点 v 也放入栈中，形成一个完整的环路。
                for (int x = v; x != w; x = edgeTo[x]){
                    cycle.push(x);
                }
                cycle.push(w);
                cycle.push(v);
            }
            onStack[v] = false;
        }
    }

    /**
     * g 是否含有环
     * 
     * @return
     */
    public boolean hasCycle() {
        return cycle != null;
    }

    /**
     * 有向环的所有顶点
     * 
     * @return
     */
    public Iterable<Integer> cycle() {
        return cycle;
    }
}
