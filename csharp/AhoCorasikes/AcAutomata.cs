using System;
using System.Collections.Generic;

namespace AhoCorasikes {
    public class AcAutomata {
        private readonly AcNode root;
        public AcAutomata() {
            root = new AcNode('/');
        }
        public void BuildFailurePointer() {
            Queue<AcNode> queue = new Queue<AcNode>();
            root.fail = null;
            queue.Enqueue(root);
            while (!(queue.Count == 0)) {
                AcNode p = queue.Dequeue();
                for (int i = 0; i < 26; ++i) {
                    AcNode pc = p.children[i];
                    if (pc == null) continue;
                    if (p == null) {
                        pc.fail = root;
                    } else {
                        AcNode q = p.fail;
                        while (q != null) {
                            AcNode qc = q.children[pc.Data - 'a'];
                            if (qc != null) {
                                pc.fail = qc;
                                break;
                            }
                            q = q.fail;
                        }
                        if (q == null) {
                            pc.fail = root;
                        }
                    }
                    queue.Enqueue(pc);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">主串</param>
        public void Match(string text) {
            int n = text.Length;
            AcNode p = root;
            for (int i = 0; i < n; i++) {
                int idx = text[i] - 'a';
                while (p.children[idx] == null && p != root) {
                    p = p.fail; //失败指针发挥作用的地方
                }
                p = p.children[idx];
                if (p == null) p = root; //没有匹配，从 root 开始重新匹配
                AcNode tmp = p;
                while (tmp != root) {
                    if (tmp.IsEndingChar == true) {
                        int pos = i - tmp.length + 1;
                        Console.WriteLine(" 匹配起始下标 " + pos + "; 长度 " + tmp.length);
                    }
                    tmp = tmp.fail;
                }
            }
        }
    }
}