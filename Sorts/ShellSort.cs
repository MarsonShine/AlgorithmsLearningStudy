namespace Sorts {
    //希尔排序
    public class ShellSort {
        public void Sort (int[] array) {
            if (array.Length < 1) return;
            int inc = array.Length / 2; //步长
            while (inc >= 1) {
                for (int i = inc; i < array.Length; i++) {
                    int tmp = array[i];
                    int j = i - inc;
                    while (j >= 0 && array[j] > tmp) {
                        array[j + inc] = array[j];
                        j -= inc;
                    }
                    array[j + inc] = tmp;
                }
                inc = inc / 2;
            }
        }
    }
}