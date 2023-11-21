/*
 * 有向图
 */

import edu.princeton.cs.algs4.Bag;
import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.StdOut;

public class Digraph {
    private final int V;
    private int E;
    private Bag<Integer>[] adj; // 邻接表
    // 创建一副含有 v 个顶点但没有边的有向图

    public Digraph(int v) {
        this.V = v;
        this.E = 0;
        adj = (Bag<Integer>[]) new Bag[V];
        for (int i = 0; i < V; i++) {
            adj[i] = new Bag<>();
        }
    }

    // 从输入流 in 中读取一副有向图
    public Digraph(In in) {
        this.E = 0;
        this.V = 0;
        adj = (Bag<Integer>[]) new Bag[V];
    }

    /**
     * 顶点总数
     */
    public int V() {
        return V;
    }

    /**
     * 边的总数
     */
    public int E() {
        return E;
    }

    /**
     * 向有向图中添加一条边 v -> w
     *
     * @param v the first vertex
     * @param w the second vertex
     */
    public void addEdge(int v, int w) {
        adj[v].add(w);
        E++;
    }

    /**
     * 由 v 指出的边所连接的所有顶点
     * 
     * @param v 顶点v
     * @return
     */
    public Iterable<Integer> adj(int v) {
        return adj(v);
    }

    /**
     * 图的反向图
     * 
     * @return
     */
    public Digraph reverse() {
        Digraph r = new Digraph(V);
        for (int v = 0; v < V; v++) {
            for (int w : adj[v]) {
                r.addEdge(w, v);
            }
        }
        return r;
    }

    public String toString() {
        String s = V + " vertices, " + E + " edges \n";
        for (int v = 0; v < V; v++) {
            s += v + ": ";
            for (int w : adj(v)) {
                s += w + " ";
            }
            s += "\n";
        }
        return s;
    }

    public boolean hasEdge(int v, int w) {
        for (Integer n : adj[v]) {
            if (n == w) {
                return true;
            }
        }
        return false;
    }

    public static void main(String[] args){
        Digraph digraph = new Digraph(5);
        digraph.addEdge(0, 1);
        digraph.addEdge(0, 2);
        digraph.addEdge(1, 3);
        digraph.addEdge(1, 4);
        digraph.addEdge(2, 3);
        digraph.addEdge(2, 4);
    }
}
