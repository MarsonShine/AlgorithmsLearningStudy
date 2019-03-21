namespace AlgorithmPractices.Recursions {
    public class Demonstrations {
        public int Fib(int n) {
            if (n == 1) return 1;
            if (n == 2) return 2;
            return Fib(n - 1) + Fib(n - 2);
        }

        public int FibTail(int n) {
            if (n == 1) return 1;
            if (n == 2) return 2;
            int ret = 0;
            int pre = 2;
            int prepre = 1;
            for (int i = 2; i <= n; i++) {
                ret = pre + prepre;
                prepre = pre;
                pre = ret;
            }
            return ret;
        }

        public int FibRecursionTail(int n, int pre, int prepre) {
            if (n == 0) return pre;
            return FibRecursionTail(--n, pre + prepre, pre);
        }

        public int Factorial(int n) {
            if (n == 0) return 1;
            int ret = 1;
            for (int i = n; i > 0; i--) {
                ret = ret * i;
            }
            return ret;
        }
        public int FactorialRecursion(int n) {
            if (n == 0) return 1;
            return n * FactorialRecursion(n - 1);
        }

        public int FactorialRecursionTail(int n, int pre) {
            if (n == 0) return pre;
            return FactorialRecursionTail(n - 1, pre * n);
        }
    }
}