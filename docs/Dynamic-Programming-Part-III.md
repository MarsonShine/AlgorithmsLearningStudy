# 动态规划实践

如何用动态规划来分辨出两个字符串的相似程度？有一个很好的测量算法，那就是编辑距离（Edit Distance）。

## 编辑距离

就是将一个字符串转换到另一个字符串最少编辑的次数（增加一个字符，修改一个字符或者替换一个字符）。编辑距离越大，说明两个字符串的相似程度越小；相反，编辑距离越小，相似程度越大。两个完全一样的字符串的编辑距离是 0。

编辑距离主要有两种计算方式

1. 莱温斯坦距离（Levenshtein distance）

   允许增，删，替换指定字符三种编辑操作，值的大小代表字符串之间的差异程度

2. 最长公共字串长度（longest common substring length）

   允许增，删指定字符两种编辑操作，值的大小代表字符串之间的相似程度

例如有两个字符串 mitcmu 和 mtacnu 的莱恩斯坦距离为 3，最长公共字串长度为 4。

## 计算莱恩斯坦距离

我们从上面得知莱恩斯坦距离操作字符串有三种操作。我们先用回溯算法来分析整个过程，如果 a[i] 与 b[j] 匹配，我们就递归 a[i+1] 与 b[j+1]。如果不匹配，我们可以有一下几种编辑操作：

- 可以删除 a[i]，这样就要比较 a[i+1] 与 b[j]。
- 可以删除 b[j]，这样就比较 a[i] 与 b[j+1]。
- 可以在 a[i] 之前加与 b[j] 相同的字符，然后比较 a[i] 与 b[j+1]。
- 可以在 b[i] 之前加与 a[i] 相同的字符，然后递归比较 a[i+1] 与 b[j]。
- 可以将 a[i] 替换成 b[i] 的值，或者把 b[j] 的值替换成 a[i] 的值，然后递归比较 a[i+1] 与 b[j+1]。

```c#
private string a = "mitcmu";
private string b = "mtacnu";
private int minValue = int.MinValue;
public void LenDistance(int i, int j, int editDistance){
    var n = a.Length;
    var m = b.Length;
    if(i == n || j == m){
        if(i < n) editDistance += (n-i);
        if(j < m) editDistance += (m-j);
        if(minValue > editDistance) minValue = editDistance;
        return;
    }
    if(a[i] == b[j]){
        //匹配
        LenDistance(i+1, b+1, editDistance);
    }else{
        //不匹配，有三种编辑操作
        LenDistance(i+1, j, editDistance+1);
        LenDistance(i, j+i, editDistance+1);
        LenDistance(i+1, j+1, editDistance+1);
    }
}
```

然后根据回溯来推导出状态树结构，并查找是否有重复的状态结点，否过有就可以动态规划优化，也可以用备忘录 模式（如果问题情况允许使用备忘录模式）。

![img](https://static001.geekbang.org/resource/image/86/89/864f25506eb3db427377bde7bb4c9589.jpg)

我们可以到（n，m，editDistance）我们在图中可以明显看出，有多个 n，m一致的状态结点，而对于 n，m 相同的，我们要取 editDistance 最小的值作为推导下一层级的状态（也就是说当前状态 (i,j) 的编辑距离是由上一个状态推导而来，而上一个状态结点有三种情况，要取其中最小的）。因为当不匹配时，我们有三种编辑操作，但是不管这三种操作（增，删还是替换）状态结点值都是 （a[i]，b[j-1]，edit+1）、（a[i-1]，b[j]，edit+1）、（a[i-1]，b[j-1]，edit+1）推导的。然后在这三种操作情况下合并取最小的，所以就有推导公式，状态转移方程为：

```
如果：a[i] = b[i] 匹配，那么最小的编辑距离 min_edist(i,j) 就为：
min(min_editdistance(i-1,j)+1,min_editdistance(i,j-1)+1,min_editdistance(i-1,j-1))
如果：a[i] != b[i] 不匹配，那么最小的编辑距离 min_edist(i,j) 就为：
min(min_editdistance(i-1,j)+1,min_editdistance(i,j-1)+1,min_editdistance(i-1,j-1)+1)
```

所以动态规划优化的代码如下：

```c#
public int LDistance(string main, string target) {
    var n = main.Length;
    int m = target.Length;
    int[, ] minDistance = new int[n, m];
    //初始化
    for (int i = 0; i < n; i++) {
        if (main[i] == target[0]) minDistance[i, 0] = i;
        else if (i != 0) minDistance[i, 0] = minDistance[i - 1, 0] + 1;
        else minDistance[i, 0] = 1;
    }
    for (int j = 0; j < m; ++j) {
        if (target[j] == main[0]) minDistance[0, j] = j;
        else if (j != 0) minDistance[0, j] = minDistance[0, j - 1] + 1;
        else minDistance[0, j] = 1;
    }
    for (int i = 1; i < n; ++i) {
        for (int j = 1; j < m; ++j) {
            if (main[i] == target[j]) minDistance[i, j] = MinDistance(minDistance[i - 1, j] + 1, minDistance[i, j - 1] + 1, minDistance[i - 1, j - 1]);
            else minDistance[i, j] = MinDistance(minDistance[i - 1, j] + 1, minDistance[i, j - 1] + 1, minDistance[i - 1, j - 1] + 1);
        }
    }
    return minDistance[n - 1, m - 1];
}

private int MinDistance(int x, int y, int z) {
    int minValue = int.MinValue;
    if (x < minValue) minValue = x;
    if (y < minValue) minValue = y;
    if (z < minValue) minValue = z;
    return minValue;
}
```

## 计算最长公共字串长度

与莱恩斯坦距离不同，最长公共字串长度只有两种操作，增加和删除，递归推导思路与莱恩斯坦距离非常相似。每个状态还是由三个变量决定（i，j，max_lcs）其中 max_lcs 表示 a[0...i] 和 b[0...j] 的最长公共字串长度。那么状态（i,j）是如何推导来的呢？

还是先回溯过程：

- 如果 a[i] 与 b[j] 匹配，则递归比较 a[i+1]，b[j+1]，最长公共字串长度加1
- 如果 a[i] 与 b[j] 不匹配，最长公共字串长度不变，这个时候，有两个决策操作路线：
- 删除 a[i] 或在 b[i] 前面加上字符串 a[i]，继续递归比较 a[i] 与 b[j+1]
- 删除 b[j] 或在 a[j] 前面加上字符串 b[j]，继续递归比较 a[i+1] 与 b[j]

也就是说状态 (i,j) 只由下面三种状态推导而来

- （i-1,j,max_lcs）其中 max_cls 标识 a[0...i-1] 和 b[0...j] 的最长公共字串长度
- （i,j-1,max_lcs）其中 max_cls 标识 a[0...i] 和 b[0...j-1] 的最长公共字串长度
- （i-1,j-1,max_lcs）其中 max_cls 标识 a[0...i-1] 和 b[0...j-1] 的最长公共字串长度

可得递推公式，状态转移方程：

```
如果：a[i] == b[j]，那么：max_lcs(i,j) 就为：
max(max_lcs(i-1,j),max_lcs(i,j-1)，max_lcs(i-1,j-1)+1)
如果：a[i] != b[j]，那么：max_lcs(i,j) 就为：
max(max_lcs(i-1,j),max_lcs(i,j-1),max_lcs(i-1,j-1))
```

```c#
public int Lcs() {
    int[, ] maxlcs = new int[n, m];
    //初始化
    for (int i = 0; i < n; ++i) {
        if (a[i] == b[0]) maxlcs[i, 0] = 1;
        else if (i != 0) maxlcs[i, 0] = maxlcs[i - 1, 0];
        else maxlcs[i, 0] = 0;
    }
    for (int j = 0; j < m; ++j) {
        if (a[0] == b[j]) maxlcs[0, j] = 1;
        else if (j != 0) maxlcs[0, j] = maxlcs[0, j - 1];
        else maxlcs[0, j] = 0;
    }
    for (int i = 1; i < n; ++i) {
        for (int j = 1; j < m; ++j) {
            if (a[i] == b[j]) maxlcs[i, j] = Max(maxlcs[i - 1, j], maxlcs[i, j - 1], maxlcs[i - 1, j - 1] + 1);
            else maxlcs[i, j] = Max(maxlcs[i - 1, j], maxlcs[i, j - 1], maxlcs[i - 1, j - 1]);
        }
    }
    return maxlcs[n - 1, m - 1];
}

private int Max(int x, int y, int z) {
    var max = int.MinValue;
    if (x > max) max = x;
    if (y > max) max = y;
    if (z > max) max = z;
    return max;
}
```

