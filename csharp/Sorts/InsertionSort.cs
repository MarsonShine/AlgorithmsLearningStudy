namespace Sorts {
    public class InsertionSort {
        public void Sort(int[] a) {
            if (a.Length < 1) return;
            for (int i = 1; i < a.Length; i++) { //未排序集合
                int value = a[i];
                int j = i - 1;
                for (; j >= 0; j--) { //已排序集合
                    if (a[j] > value) {
                        //移动数据
                        a[j + 1] = a[j];
                    } else {
                        break;
                    }
                }
                a[j + 1] = value;
            }
        }
    }
}