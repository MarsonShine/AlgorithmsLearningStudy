using System.Collections;

namespace AhoCorasikes {
    public class AcNode {
        public char Data { get; set; }
        public AcNode[] children = new AcNode[26]; //假设字符串只包含这26个字母
        public bool IsEndingChar = false; //结尾字符为 true
        public int length = -1; //当IsEndingChar=true时，记录模式串长度
        public AcNode fail; //失败指针
        public AcNode(char data) {
            this.Data = data;
        }
    }
}