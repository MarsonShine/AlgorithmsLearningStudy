namespace AlgorithmPractices.Searches {
    public class ReverseString {
        public void Reverse(char[] s) {
            if (s.Length == 1) return;
            int i = 0;
            int j = s.Length - 1;
            while (j > i) {
                char t = s[i];
                s[i++] = s[j];
                s[j--] = t;
            }
        }
    }
}