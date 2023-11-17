package graphs;

import edu.princeton.cs.algs4.Graph;

public class TowColor {
    private boolean[] marked;
    private boolean[] color;
    private boolean isTwoColorable = true;

    public TowColor(Graph g) {
        marked = new boolean[g.V()];
        color = new boolean[g.V()];
        for (int s = 0; s < g.V(); s++) {
            if (!marked[s]) {
                dfs(g, s);
            }
        }
    }

    private void dfs(Graph g, int s) {
        marked[s] = true;
        for (int w : g.adj(s)) {
            if (!marked[w]) {
                color[w] = !color[s];
                dfs(g, w);
            } else if (color[w] == color[s]) {
                isTwoColorable = false;
            }
        }
    }

    public boolean isTwoColorable() {
        return isTwoColorable;
    }

}
