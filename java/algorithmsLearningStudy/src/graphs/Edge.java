public class Edge implements Comparable<Edge> {
    private final int v; // 一个顶点
    private final int w; // 另一个顶点
    private final double weight; // 边的权重

    /**
     * 边的两个顶点和权重
     * 
     * @param v
     * @param w
     * @param weight
     */
    public Edge(int v, int w, double weight) {
        this.v = v;
        this.w = w;
        this.weight = weight;
    }

    /**
     * 边两端的顶点之一
     * @return
     */
    public int either() {
        return v;
    }
    /**
     * 另一个顶点
     * @param v
     * @return
     */
    public int other(int v) {
        if (w == v)
            return w;
        else if (v == w)
            return v;
        else
            throw new RuntimeException("Illegal endpoint");
    }
    /**
     * 边的权重
     * @return
     */
    public double weight() {
        return weight;
    }

    public int compareTo(Edge that) {
        if (this.weight < that.weight)
            return -1;
        else if (this.weight > that.weight)
            return +1;
        else
            return 0;
    }

    public String toString() {
        return String.format("%d-%d %.5f", v, w, weight);
    }
}
