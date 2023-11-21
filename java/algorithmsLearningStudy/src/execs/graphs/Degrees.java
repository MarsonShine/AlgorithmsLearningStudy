package graphs;

import edu.princeton.cs.algs4.Digraph;
import edu.princeton.cs.algs4.Queue;

public class Degrees {
    private Digraph g;
    private int[] indegree;
    private int[] outdegree;
    private Queue<Integer> sources; // 起点的集合
    private Queue<Integer> sinks; // 终点的集合
    private boolean ismap;

    public Degrees(Digraph G) {
        g = G;
        indegree = new int[g.V()];
        outdegree = new int[g.V()];
        sources = new Queue<>();
        sinks = new Queue<>();
        for (int v = 0; v < g.V(); v++) {
            for (int n : g.adj(v)) {
                outdegree[v]++;
                indegree[n]++;
            }
        }

        for (int v = 0; v < g.V(); v++) {
            if (indegree[v] == 0) // 入度为 0 的顶点为起点
                sources.enqueue(v);
            if (outdegree[v] == 0) // 出度为 0 的顶点为终点
                sinks.enqueue(v);
            if (outdegree[v] != 1) // 出度不为 1 的顶点不是映射
                ismap = false;
        }
    }

    public int indegree(int v) {
        return indegree[v];
    }

    public int outdegree(int v) {
        return outdegree[v];
    }

    /**
     * 所有起点的集合
     * 
     * @return
     */
    public Iterable<Integer> sources() {
        return sources;
    }

    /**
     * 所有终点的集合
     * 
     * @return
     */
    public Iterable<Integer> sinks() {
        return sinks;
    }

    /**
     * G 是否映射
     * 
     * @return
     */
    public boolean isMap() {
        return ismap;
    }
}
