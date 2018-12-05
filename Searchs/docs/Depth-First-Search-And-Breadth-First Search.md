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

![](https://static001.geekbang.org/resource/image/00/ea/002e9e54fb0d4dbf5462226d946fa1ea.jpg)

```c#
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
            if (!visited[q]) {	//判断 q 顶点是否被访问过
                prev[q] = w;	//没有访问过，说明 q 结点是通过顶点 w 遍历过来的
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
```

这里有三个重要的辅助变量 visited、queue、prev 很重要。

1. visited 是用来记录已经被访问的顶点，用来避免顶点被重复访问。一旦被访问，设置visited[q] 为true
2. queue 是一个队列，存储已经被访问，但相连的顶点还没有被访问的顶点。广度优先搜索是逐层访问的。也就是说，只有当访问顶点 k 时，才能继续遍历访问顶点 k+1。当我们访问顶点 k 时需要把访问的顶点记录下来，这样才能通过第 k 层顶点来找 k+1 层的顶点
3. prev 用来记录搜索路径，从顶点 s 开始，广度优先搜索到顶点 t 的路径。只不过顺序时反向存储的。prev[w] 存储的是，顶点 w 是从那个前驱顶点遍历访问过来的。比如，我们通过顶点 2 的邻接表访问到的顶点 3，那么 prev[3]=2。

流程图如下：

![](https://static001.geekbang.org/resource/image/ea/23/ea00f376d445225a304de4531dd82723.jpg)

![](https://static001.geekbang.org/resource/image/ea/23/ea00f376d445225a304de4531dd82723.jpg)

![](https://static001.geekbang.org/resource/image/4c/39/4cd192d4c220cc9ac8049fd3547dba39.jpg)

时间复杂度分析：

最坏情况下，终点 t 离起始点 s 很远，要遍历所有顶点才能找得到。这个时候，每个顶点都要进出一遍队列，每个边也都会被访问一次，所以，广度优先搜索的时间复杂度为 O(V+E)，其中 V 表示顶点个数，E 表示边的个数。对于一个连通图来看，一个图中的所有顶点都是相通的，E 肯定要大于等于 V-1，所以广度优先搜索时间复杂度就为 O(E)。

而空间复杂度主要消耗前面所讲的三个辅助变量。这三个存储空间的大小都不会超过顶点的个数，所以空间复杂度为O(E)。

## 深度优先搜索（DFS）