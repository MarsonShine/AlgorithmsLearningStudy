public class DirectedEdge {
    private int V; // 顶点
    private int W; // 另一个顶点
    private double weight; // 边的权重
    public DirectedEdge(int v, int w, double weight) {
        this.V = v;
        this.W = w;
        this.weight = weight;
    }

    public int from() {
        return V;
    }

    public int to() {
        return W;
    }

    public double weight() {
        return weight;
    }

    public String toString() {
        return String.format("%d->%d %.5f", V, W, weight);
    }
}
