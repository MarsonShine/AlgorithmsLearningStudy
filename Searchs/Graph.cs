using System;
using System.Collections.Generic;

namespace Searchs {
    /// <summary>
    /// 无向图
    /// </summary>
    public class Graph {
        private int v; //顶点的个数
        private List<int>[] adjs; //邻接表
        public Graph(int v) {
            this.v = v;
            adjs = new List<int>[v];
            for (int i = 0; i < v; i++) {
                adjs[i] = new List<int>();
            }
        }

        public void AddEdge(int s, int t) { //无向图一条边存两次
            adjs[s].Add(t);
            adjs[t].Add(s);
        }

        /// <summary>
        /// 广度优先搜索算法
        /// 求从 s 顶点到 t 顶点的最短举例
        /// </summary>
        /// <param name="s">起始顶点</param>
        /// <param name="t">终止顶点</param>
        public void BFS(int s, int t) {
            if (s == t) return;
            bool[] visited = new bool[v];
            visited[s] = true;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            int[] prev = new int[v];
            for (int i = 0; i < v; ++i) {
                prev[i] = -1;
            }
            while (queue.Count != 0) {
                int w = queue.Dequeue();
                for (int i = 0; i < adjs[w].Count; i++) {
                    int q = adjs[w][i];
                    if (!visited[q]) { //判断 q 顶点是否被访问过
                        prev[q] = w; //没有访问过，说明 q 结点是通过顶点 w 遍历过来的
                        if (q == t) {
                            Print(prev, s, t);
                            return;
                        }
                        visited[q] = true;
                        queue.Enqueue(q);
                    }
                }
            }
        }

        private void Print(int[] prev, int s, int t) {
            if (prev[t] != -1 && t != s) {
                Print(prev, s, t);
            }
            Console.WriteLine(t + " ");
        }
    }
}