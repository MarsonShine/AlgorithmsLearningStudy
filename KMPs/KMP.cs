using System;

namespace KMPs {
    public class KMP {
        public static int Kmp(string mainString, string matchString) {
            int[] next = GetNexts(matchString);
            int j = 0;
            for (int i = 0; i < mainString.Length; i++) {
                while (j > 0 && mainString[i] != matchString[j]) { //一直找到主串[i]和模式串[j]
                    j = next[j - 1] + 1;
                }
                if (mainString[i] == matchString[j])
                    ++j;
                if (j == matchString.Length) //找到匹配模式串
                    return i - matchString.Length + 1;
            }
            return -1;
        }

        private static int[] GetNexts(string matchString) {
            throw new NotImplementedException();
        }
    }
}