using System;
using System.Collections.Generic;

namespace Sorts {
    /// <summary>
    /// 拓扑排序
    /// </summary>
    public class TopologySort {
        public class Graph {
            private readonly int v; //顶点个数
            private readonly List<int>[] adjs; //邻接表
            public Graph(int v) {
                this.v = v;
                adjs = new List<int>[v];
                for (int i = 0; i < v; ++i) {
                    adjs[i] = new List<int>();
                }
            }
            public void AddEdge(int s, int t) => adjs[s].Add(t);

            public List<int>[] Adjs => adjs;
        }

        //Kahn算法
        public void TopologySortByKahn() {
            //构建有向无环图
            int v = 4; //顶点个数
            var g = new Graph(v);
            g.AddEdge(0, 2);
            g.AddEdge(2, 3);
            g.AddEdge(2, 1);
            g.AddEdge(3, 1);

            int[] indegree = new int[v]; //存储每个顶点的入度
            for (int i = 0; i < v; i++) {
                for (int j = 0; j < g.Adjs[i].Count; ++j) {
                    int w = g.Adjs[i][j]; //边：i->w
                    indegree[w]++;
                }
            }
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < v; ++i) {
                if (indegree[i] == 0) queue.Enqueue(i);
            }
            while (queue.Count != 0) {
                int i = queue.Dequeue();
                Console.Write("->" + i);
                for (int j = 0; j < g.Adjs[i].Count; j++) {
                    int k = g.Adjs[i][j];
                    indegree[k]--;
                    if (indegree[k] == 0) queue.Enqueue(k);
                }
            }
        }
        //DFS算法
        public void TopologySortByDFS() {
            int v = 4;
            var g = new Graph(v);
            g.AddEdge(0, 2);
            g.AddEdge(2, 3);
            g.AddEdge(2, 1);
            g.AddEdge(3, 1);
            List<int>[] adjs = g.Adjs;
            //构建逆邻接表
            List<int>[] inverseAdjs = new List<int>[v];
            //初始化
            for (int i = 0; i < v; ++i) {
                inverseAdjs[v] = new List<int>();
            }
            //由邻接表生成逆邻接表
            for (int i = 0; i < v; i++) {
                for (int j = 0; j < adjs[i].Count; j++) {
                    int w = adjs[i][j]; //i->w
                    inverseAdjs[w].Add(i); //w->i
                }
            }
            bool[] visited = new bool[v];
            for (int i = 0; i < v; i++) {
                if (visited[i] == false) {
                    visited[i] = true;
                    DFS(i, inverseAdjs, visited);
                }
            }
        }

        private void DFS(int vertex, List<int>[] inverseAdjs, bool[] visited) {
            for (int i = 0; i < inverseAdjs[vertex].Count; i++) {
                int w = inverseAdjs[vertex][i];
                if (visited[w] == true) continue;
                visited[w] = true;
                DFS(i, inverseAdjs, visited);
            } //把 vertex 这个顶点的所有可达顶点全部打印出来，然后在打印自己
            Console.Write(" -> ", vertex);
            Console.WriteLine();
        }
    }
}