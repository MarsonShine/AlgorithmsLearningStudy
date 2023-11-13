package searchs;

public class exec24 {
    private class Point2D {
    
        private double x;
        private double y;
        public Point2D(double x, double y) {
            this.x = x;
            this.y = y;
        }

        @Override
        public int hashCode() {
            int hash = 31;
            return hash * ((Double)x).hashCode() + hash * ((Double)y).hashCode();
        }
    }
}
