using System;
using System.Collections;
using System.Collections.Generic;
using MS.Util.Collections;

namespace Maps {
    /// <summary>
    /// 有向权图
    /// </summary>
    public class Graph {
        private readonly List<Edge>[] _adjs; //邻接表
        private readonly int _v; //顶点

        public Graph(int v) {
            _v = v;
            _adjs = new List<Edge>[v];
            for (int i = 0; i < v; i++) {
                this._adjs[i] = new List<Edge>();
            }
        }
        //添加一条边
        public void AddEdge(int s, int d, int w) {
            _adjs[s].Add(new Edge(s, d, w));
        }
        //从顶点s到顶点t的边（最短路径）
        public void Dijkstra(int s, int t) {
            int[] predecessor = new int[_v]; //用来还原最短路径
            Vertex[] vertices = new Vertex[_v];
            for (int i = 0; i < _v; i++) {
                vertices[i] = new Vertex(i, int.MaxValue);
            }
            PriorityQueue<Vertex> queue = new PriorityQueue<Vertex>(_v);
            bool[] inqueue = new bool[_v];
            vertices[s].Dist = 0;
            queue.Push(vertices[s]);
            inqueue[s] = true;
            while (queue.Count > 0) {
                Vertex minVertex = queue.Pop(); //取堆顶元素并删除
                if (minVertex.ID == t) break; //最短路径产生
                for (int i = 0; i < _adjs[minVertex.ID].Count; ++i) {
                    Edge edge = _adjs[minVertex.ID][i]; //取出于顶点相连的边
                    Vertex nextVertex = vertices[edge.End]; //minVertex->nextVertex 边
                    if (minVertex.Dist + edge.W < nextVertex.Dist) { //更新 nextVertex 的 dist
                        nextVertex.Dist = minVertex.Dist + edge.W;
                        predecessor[nextVertex.ID] = minVertex.ID;
                        if (inqueue[nextVertex.ID] == true) {
                            queue.Update(nextVertex); //更新队列中的 dist 值
                        } else {
                            queue.Push(nextVertex);
                            inqueue[nextVertex.ID] = true;
                        }
                    }
                }
            }
            //输出最短路径
            Console.WriteLine(s);
            Print(s, t, predecessor);
        }

        private void Print(int s, int t, int[] predecessor) {
            if (s == t) return;
            Print(s, predecessor[t], predecessor);
            Console.WriteLine("->" + t);
        }

        private class Edge {
            private readonly int _start; //边的起始顶点
            private readonly int _end; //边的终止顶点
            private readonly int _w; //权重

            public Edge(int start, int end, int w) {
                _start = start;
                _end = end;
                _w = w;
            }

            public int Start => _start;
            public int End => _end;
            public int W => _w;
        }

        private class Vertex : IComparer<Vertex> {
            public Vertex(int id, int dist) {
                ID = id;
                Dist = dist;
            }
            public int Compare(Vertex x, Vertex y) {
                return x.Dist - y.Dist;
            }
            public int Dist { get; set; } //从起点到这个顶点的距离
            public int ID { get; set; } //顶点
        }
    }
}