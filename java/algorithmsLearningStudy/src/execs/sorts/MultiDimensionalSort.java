package sorts;

import java.util.*;

public class MultiDimensionalSort {
    /**
     * Sorts a vector of vectors based on their elements.
     *
     * @param  args  the command line arguments
     */
    public static void main(String[] args) {
        Vector<Vector<Integer>> v = new Vector<>();
        v.add(new Vector<>(Arrays.asList(1, 2, 3)));
        v.add(new Vector<>(Arrays.asList(1, 2, 2)));
        v.add(new Vector<>(Arrays.asList(1, 1, 3)));
        v.add(new Vector<>(Arrays.asList(2, 1, 3)));

        System.out.println("Before sorting:");
        printVectors(v);

        Collections.sort(v, new Comparator<Vector<Integer>>() {
            @Override
            public int compare(Vector<Integer> a, Vector<Integer> b) {
                for (int i = 0; i < a.size(); i++) {
                    if(a.get(i) != b.get(i)) {
                        return a.get(i) - b.get(i);
                    }
                }
                return 0;
            }
        });

        System.out.println("After sorting:");  
        printVectors(v);  
    }

    public static void printVectors(Vector<Vector<Integer>> v) {
        for (Vector<Integer> vector : v) {
            System.out.println(vector);
        }
    }
}
