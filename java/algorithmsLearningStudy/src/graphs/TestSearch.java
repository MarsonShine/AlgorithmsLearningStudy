import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.StdOut;

public class TestSearch {
    public static void main(String[] args) {
        Graph g = new Graph(5);
        int s = Integer.parseInt(args[0]);
        GraphSearch gs = new GraphSearch(g, s);
        for (int v = 0; v < g.V(); v++) {
            if (gs.marked(v)) {
                StdOut.print(s + " ");
            }
        }
        if (gs.count() != g.V()) {
            StdOut.print(s + " ");
        }
        StdOut.println("connected");

        Graph g2 = new Graph(new In(args[0]));
        int s2 = Integer.parseInt(args[1]);
        Paths search = new Paths(g2, s2);
        for (int v = 0; v < g2.V(); v++) {
            StdOut.print(s + " to " + v + ": ");
            if (search.hasPathTo(v)) {
                for (int w : search.pathTo(v)) {
                    if (w == s) {
                        StdOut.print(w);
                    } else {
                        StdOut.print("-" + w);
                    }

                }
            }
            StdOut.println();
        }
    }
}
