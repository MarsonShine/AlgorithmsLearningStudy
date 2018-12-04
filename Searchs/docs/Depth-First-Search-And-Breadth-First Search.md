# 深度优先搜索和广度优先搜索

深度优先搜索和广度优先搜索都是基于 “图” 这种数据结构。图这种数据结构的表达能力很强，大部分涉及搜索的场景都可以抽象成 “图”。

图的搜索算法，最容易理解的就是，从图中找出从一个顶点出发，到另一个顶点的路径。

图的存储方式主要有两种：邻接表和邻接矩阵。

我们先给出图的代码实现方式

```c#
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
```



> 注意：BFS 以及 DFS 即可以用在无向图也可以用在有向图，下面提到的都是以 “无向图” 为场景分析的。

## 广度优先搜索（BFS）

其实就像是 “地毯式” 搜索的层层递进的搜索方式，先查离起始点最近的，然后是次近的，依次往外搜索。



## 深度优先搜索（DFS）