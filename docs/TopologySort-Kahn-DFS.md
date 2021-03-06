# 拓扑排序——找出依赖顺序

我们如何从大量的彼此的依赖关系？比如 A 依赖于 B，B 依赖于 C，C 依赖于 D，又或者类比到我们 IDE 编译文件，如何确定源代码文件的编译依赖关系？编译器在编译文件的时候，会按照依赖关系一个个编译。比如 A.cpp 依赖于 B.cpp，那么编译器就需先遍历 B.cpp 然后是 A.cpp。

编译器通过分析源文件或者是程序员写的编译配置文件，来获取这种局部的依赖关系。那编译器是如何通过源文件两两之间的依赖关系，确定一个全局的编译顺序的？

这就需要用到拓扑排序了，并且在很多时候，依赖顺序不是唯一的。比如上面的例子，B 可以依赖 C 或者是 D。我们如何实现拓扑排序？

首先要确定数据结构，我们可以把文件与文件的依赖关系抽象成一个有向图。每个文件对应有向图的每个顶点，源文件之间的依赖关系就是有向图的边与边的依赖关系，如 A 依赖于 B，则是 B 要比 A 先执行，也就是 B -> A。所以这就要求依赖关系不能存在环（即有向无环图）。

```c#
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
```

拓扑排序有两种算法：Kahn 算法，DPS 算法

## Kahn 算法

先给出代码，之后在分析：

```c#
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
```

首先我们得明确一个概念，如果 A 依赖于 B 的，那就是说 B 要比 A先行，那么有向边为 A -> B，我们称 B 的入度 为 1，所以我们把入度为 0 的就是没有依赖关系，可以先执行。

在代码中，我们先构建一个有向无环图，把每个边的依赖关系记录下来（每个顶点的入度），然后我们在打印出其顶点的依赖关系，并且每输出一个顶点，对应的入度就要减 1，直到输出结束。

## DFS深度遍历算法

```c#
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
```

DFS则是先遍历图中所有的顶点，而不是搜索一个顶掉到另一个的路径。

首先我们还是构造一个有向无环图，用于测试。

第一部分我们是构建一个逆临接表。逆邻接表中不同于邻接表，邻接表边 A -> B 是表示 A 先于 B 执行，也就是要 B 依赖 A。而在逆邻接表中，边 A -> B 表示 A 依赖 B，A 后于 B 执行。

第二部分就是核心部分，**递归处理每个顶点**。对于每个顶点来说，我们先输出它可达的所有顶点，也就是说，先把它依赖的所有的顶点全部输出出来，最后才输出这个顶点。