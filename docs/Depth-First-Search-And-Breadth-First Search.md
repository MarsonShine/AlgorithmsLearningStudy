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

深度优先算法（Depth-First-Search），简称 DFS。最直观的例子就是 “走迷宫”。

假设你站在迷宫的某个岔路口，然后想找到出口。你愿意选择一个岔路口来走，走着走着发现走不通的时候，就会往回走到上一个路口并重新选择另一个路口，直到最终找到出口。这种走法就是深度优先搜索。

我们用图来表示从其实顶点 s 深度优先算法搜寻到 终点 t。其中实线箭头表示遍历，虚线箭头表示回退。

![](https://static001.geekbang.org/resource/image/87/85/8778201ce6ff7037c0b3f26b83efba85.jpg)

从图中可以看出，深度优先遍历出来的从起始点 s 到终点 t 出来的路径不是最短路径。实际上深度优先搜索算法用到的是 “回溯思想”。这种思想非常适合用递归来实现。深度优先搜索代码实现也用到了 prev，visited，以及 print() 函数，还有个比较特殊变量 found，它的作用是，当我们已经找到终点 t 之后，我们就不再递归继续查找了。

```c#
public bool found = false; //全局变量或者类成员变量，true 就说明找到顶点 t，不在继续遍历
/// <summary>
/// 深度优先算法
/// 求从 s 顶点到 t 顶点的路径（不是最短路径）
/// </summary>
/// <param name="s">起始地点</param>
/// <param name="t">终止顶点</param>
public void DFS(int s, int t) {
    found = false;
    bool[] visited = new bool[v]; //表示该顶点是否被访问
    int[] prev = new int[v]; //表示遍历到的顶点路径
    for (var i = 0; i < v; i++)
        prev[i] = -1; //初始化
    RecurisonDfs(s, t, visited, prev);
}

private void RecurisonDfs(int w, int t, bool[] visited, int[] prev) {
    if (found == true) return;
    visited[w] = true;
    if (w == t) {
        found = true;
        return;
    }
    for (int i = 0; i < adjs[w].Count; i++) {
        int q = adjs[w][i];
        if (!visited[q]) {
            prev[q] = w;
            RecurisonDfs(q, t, visited, prev);
        }
    }
}
```

时间复杂度分析：

时间复杂度：从之前的分析可知，每条边最多会被访问两次，一次是遍历，一次是回退。所以根据广度优先搜索算法的分析可知，深度优先算法的时间复杂度就是 O(E)，E 是边的个数。

空间复杂度：由刚刚的分析可知，空间消耗主要在三个变量以及递归。visited 以及 prev 数组的大小与顶点个数成正比，递归调用栈递归的最大深度不会超过顶点的个数，所以总的空间复杂度是 O(V)。

## 补充

> 深度优先搜索就好像走迷宫，往一条路走到底，路走死了就返回上一个交叉点往另一个方位走知道所有节点都走完找到迷宫出口。
>
> 而广度优先搜索则就好像有一组人一起在朝各个岔路口走，每个人都有自己的绳子。当出现心的交叉路，可以假设一个人可以分裂成多人朝着不同的岔路口继续走，当两个人相遇时，就会合并成一个。
>
> 基于上面的思路，我们也就能理解深度优先搜索实现是通过“栈”这种“后进先出”结构，广度优先算法则是通过“队列”这种“先进先出”结构。在广度优先搜索中，我们希望按照**与起点的距离的顺序来遍历所有节点**。



