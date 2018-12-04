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
    }
}