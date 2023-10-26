using System;

namespace BackTrackings {
    //正则式匹配方面的回溯算法
    public class RegexMatching {
        private bool matched = false;
        private string _pattern; //正则表达式

        public RegexMatching(string pattern) {
            _pattern = pattern;
        }
        public bool Match(string matchString) {
            matched = false;
            Rmatch(0, 0, matchString);
            return matched;
        }

        private void Rmatch(int ti, int pj, string matchString) {
            if (matched == true) return;
            if (pj == _pattern.Length) { //正则式匹配到结尾位置
                if (ti == matchString.Length) matched = true;
                return;
            }
            if (_pattern[pj] == '*') {
                for (int k = 0; k <= matchString.Length - ti; ++k) {

                }
            }
        }
    }
}