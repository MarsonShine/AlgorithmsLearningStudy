namespace Sorts.BubbleSort {
    public static class BubbleSort {
        public static IncreaseCounter increaseCounter = new IncreaseCounter ();
        public static void Sort (this int[] args) {
            if (args.Length < 1) return;
            for (int i = 0; i < args.Length; i++) {
                bool flag = false;
                for (int j = 0; j < args.Length - i - 1; j++) {
                    increaseCounter.Increase ();
                    if (args[j] > args[j + 1]) {
                        int tmp = args[j];
                        args[j] = args[j + 1];
                        args[j + 1] = tmp;
                        flag = true;
                    }
                }
                if (!flag) break;
            }
        }

        public static void SlowerSort (this int[] args) {
            if (args.Length < 1) return;
            for (int i = 0; i < args.Length; i++) {
                for (int j = 0; j < args.Length - i - 1; j++) {
                    increaseCounter.Increase ();
                    if (args[j] > args[j + 1]) {
                        int tmp = args[j];
                        args[j] = args[j + 1];
                        args[j + 1] = tmp;
                    }
                }
            }
        }
        public class IncreaseCounter {
            private int _count;

            public void Increase () {
                _count++;
            }

            public int GetCount () => _count;

            public void ClearCount () => _count = 0;
        }
    }
}