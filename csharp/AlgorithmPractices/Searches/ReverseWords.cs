namespace AlgorithmPractices.Searches {
    using System;
    public class ReverseWords {
        public static string MyReverseWords(string s) {
            s = s.Trim();
            var ar = s.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(ar, 0, ar.Length);
            return string.Join(' ', ar);
        }
    }
}