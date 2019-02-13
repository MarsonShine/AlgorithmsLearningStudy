# 动态规划（一）

> 带着问题思考动态规划的应用场景，我们在淘宝中购物就知道，在我们选择商品到购物车时，会有一个购物提示：选择其他商品，会刚好满足某优惠活动，假设是 “满100减5元” 。系统会选择一个最佳商品（所有商品加在一起刚好 100 元，或者只超出一点），这种功能是如何实现的呢？其实就是就用到了动态规划

我们先从简单的案例开始

## 0-1 背包问题

这个问题在贪心算法，回溯算法中有讲到。

关于这个问题，我们用回溯算法求最佳解我们知道实际上就是穷举法，时间复杂度不是很好，成指数级别。那我们先来优化一下回溯算法：

```c#
private int _maxWeight = int.MinValue;//结果放到 maxWeight 中。
private int[] _weights = new int[] { 2, 2, 4, 6, 3 };	//物品重量
private int _count = 5;	//物品个数
private int _maxWeightInpackage = 9;//背包承受的最大重量
//i:物品个数
//cw:当前物品的重量
public void BackTracking(int i, int cw) {
    if(cw == _maxWeightInpackage && i == __count)
    {
        if(cw > _maxWeight) _maxWeight = cw;
        return;
    }
    BackTracking(i+1, cw);//选择不装第i个商品
    if(cw + _weightx[i] <= _maxWeightInpackage)
    {
        BackTracking(i+1,cw + _weights[i]);//装第i个商品
    }
}
```

> 这里的倒数第六行与倒数第三行是很重要的，利用了回溯递归算法，把每一种选择的可能都考虑进去了（要么选中（选中之后影响后面的所有的结果），要么不选中（同理影响后面的每一个选择），后续的每一步皆是如此）

我们直接从代码中看是很难看出其中的规律的，所以我们抽象成一个递归树形结构：

![img](https://static001.geekbang.org/resource/image/42/ea/42ca6cec4ad034fc3e5c0605fbacecea.jpg)

f(0,0) = f(i,cw)，i 代表第 i 个物品，cw 代表当前背包中所承受的物品总重量。每一个物品都有两种选择，要么装入背包（背包重量对应增加），要么选择不装入（背包重量为上个选择的背包重量）。

从上面的递归树我们可以看出 f(2,2)，f(3,4)，f(3,2) 等重复计算了，那么我们在递归的时候，增加备忘录模式来减少计算冗余来提高整体算法的性能。

```c#
private bool[,] _states = new bool[5, 10];//备忘录模式，用来记录已经计算过的值
public void BackTrackingVoidRepeateCalculatation(int i, int cw)
{
    if(cw == _maxWeightInpackage && i == _count){
        if(cw > _maxWeight) _maxWeight = cw;
        return;
    }
    if(_states[i][cw]) return;//避免重复计算
    _states[i][cw] = true;
    BackTrackingVoidRepeateCalculatation(i+1, cw);
    if(cw + _weights[i] <= _maxWeightInpackage)
    {
        BackTrackingVoidRepeateCalculatation(i+1, cw + _weights[i]);
    }
}
```

这样的优化，整体算法的性能就很好了，实际上这种改进方法已经跟动态规划的时间复杂度差不多了。我们来看下动态规划是如何做的。

我们把整个求解过程分解成 n 个阶段，每个阶段相当于一次选择，背包选择是否把物品装入背包，每次选择之后，背包中的重量会相应的变化。也就是说，每个阶段对应不同的状态，对应到上图的结构树，就是每层的结点。

我们可以把每层的重复的状态结点合并，只记录每层的不同的状态结点，然后基于上一层的状态结点来推到下一层的状态节点，这样就能保证每层不同状态的个数不会超过 w 个（w 为背包容纳的最大重量），也就是次例中的 9。这样我们就避免了每层状态个数承指数级增长。

我们用一个二维数组 state[n,w+1] 来记录每层可以达到的不同状态。

第 0 个物品，有两种选择。选择装入背包，对应的背包重量就是 2；不选择装入背包，重量则是 0。那么我们用 state 来表示就是 state[0,0] = true 以及 state[0,2] = true 这两种状态。

第 1 个物品，同样有两种选择，但是是否选择装入背包对应的背包重量取决上一次选择之后的背包重量。所以当第 0 个物品没有装入时，第 1 个物品的两种选择状态就是 state[1,0]、state[1,2]；那么当第 0 个物品选择装入时，对应的第 1 个物品的状态则是 state[1,4]，state[1,2]；所以把重复的合并，则 state[1,0] = true、state[1,2] = true、state[1,4] = true 来表示这三种状态。

依次类推，直到最后一个物品选择完毕，整个 state 数组就都计算好了，然后在最后一层选择值为 true 的 w 最大的值就是我们的最佳解。

![img](https://static001.geekbang.org/resource/image/aa/b5/aaf51df520ea6b8056f4e62aed81a5b5.jpg)

![img](https://static001.geekbang.org/resource/image/bb/7e/bbbb934247219db8299bd46dba9dd47e.jpg)

```c#
public void DynamicPrograming()
{
	bool[,] states = new bool[_count, _weightInpackage + 1];
    //设置哨兵
    bool[0,0] = true;
    states[0, _weights[0]] = true;
    for(int i = 1; i < _count; ++i)	//动态规划设置状态转移
    {
        for(int j = 0; j <= _maxWeightInPackage; +=j){//不把第i个物品装入背包
            if(states[i-1,j] == true) states[i,j] = states[i-1,j];
        }
        for(int j = 0; j <= _maxWeightInPackage - _weights[i]; ++j){//把第i个物品装入背包
            //第i个物品装入背包时，背包重量为上i-1个物品装入时背包的重量
            //即是由上一个装入的状态决定
            if(states[i-1,j] == true) states[i][j+_weights[i]] = true;        
        }
    }
    for(int i = _maxWeightInPackage; i >= 0; i--){
        if(states[_count-1,i] == true ) return i;
    }
    return 0;
}
```

这就是用动态规划的思路。把每个问题分解为多个阶段，每个阶段对应一个选择。**我们记录每一个状态可达的状态集合（去掉重复的），然后通过当前的状态集合来推到下一个阶段的状态集合，动态地前进**。

时间复杂度分析：从代码中可以看出时间主要都消耗在三个 for 循环（主要是内部的两个 for 循环），所以时间复杂度就是 O(n*w) 其中 n 为物品个数，w 为背包承受最大重量。

尽管动态规划的时间复杂度很高，但是它额外申请了一个 n，w+1 的二维数组的空间内存。所以我们有时候说动态规划是一种用空间换时间的算法。那么有没有办法在内存上进一步优化呢？

实际上我们可以直接用 w+1 的一维数组来解决这个问题。在上面的动态规划的状态转移过程中，我们都可以通过一维数组来解决。

```c#
public void DynamicProgramingStrongly(){
    bool[] states = new bool[_maxWeightInPackage+1];
    states[0] = true;//第一行数组特殊处理，哨兵优化
    states[_weights[0]] = true;
    for(int i = 1; i < _count; i++){
        //把第i个物品装入背包
        //必须从大到小处理，否则会出现重复计算的问题
        for(int j = _maxWeightInPackage - _weights[i]; j >= 0; --j){
            if(states[j] == true) states[j+_weights[i]] = true;
        }
    }
    for(int i = _maxWeightInPackage; i >= 0; i++){
        if(states[i] == true) return i;
    }
    return 0;
}
```

## 0-1 背包升级问题

 我们接着上面的背包问题改造升级，我们之前讲的背包只是涉及到背包重量和物品重量。现在我们引入物品价值这一变量。要求要在满足最大背包容量重量的情况下，如何才能装的价值最大么？

这种问题我们依然可以用回溯算法解决。这个不难实现，我们马上就有了下面的代码

```c#
private int _maxWeight = int.MinValue;//结果放到 maxWeight 中。
private int _maxValue = int.MinValue; //背包物品最大价值
private int[] _weights = new int[] { 2, 2, 4, 6, 3 };	//物品重量
private int[] _values = new int[] { 3, 4, 8, 9, 6 };	//物品价值
private int _count = 5;	//物品个数
private int _maxWeightInpackage = 9;//背包承受的最大重量
public void BackTrackingUpegrade(int i, int cw, int cv)
{
    //装满了，或者处理最后一个物品
    if(_maxWeightInpackage == cw || i == _count){
        if(cv > _maxValue) _maxValue = cv;
        if(vw > _maxWeight) _maxWeight = cw;
        return;
    }
    BackTrackingUpegrade(i+1, cw, cv);	//选择不装第i个物品
    if(cw + _weights[i] <= _maxWeightInpackage){
        //装第i个物品
        BackTrackingUpegrade(i, cw+_weights[i], cv+_values[i]);
    }
}
```

同样，我们把各个层级的状态转换成树结构就跟上图类似，存在大量的重复状态（重复计算）

![img](https://static001.geekbang.org/resource/image/bf/3f/bf0aa18f367db1b8dfd392906cb5693f.jpg)

我们从上图可以发现，有很多其中 i，cw 是相同的，只是对应的物品总价值 cv 不一样。所以我们只需要取 i，cw相同的情况下 cv 值最大的即可。然后继续递归。

因为这里面存在 i，cw，cv 三种变量，并且 cv 是不同的，所以我们上面提到的备忘录优化方法在这里是不可用的。所以我们就解决动态规划来解决这个问题。

我们还是将这个问题分解成 n 个阶段，每个阶段同样对应着每个物品的相应选择，都决定着其背包容纳的最大重量以及背包容纳的最大价值。所以也对应着不同的状态结点。

我们用一个二维数组 states[n,cw+1] 来记录每层达到的状态。不过与上面不同的是，它的值不再是布尔类型，而是代表它的总价值。我们把每层中 i，cw 相同的状态合并，并取值最大的，然后基于这个状态来推到下一层的状态。

```c#
public void BackTrackingUpegradeStrongly(int i, int cw, int cv)
{
    int[,] states = new int[_count, _maxWeightInpackage + 1];
    //初始化 states
    for(int i=0;i < _count; i++){
        for(int j=0; j < _maxWeightInpackage+1; j++){
            states[i,j] = -1;
        }
    }
    //哨兵
    states[0,0] = 0;
    states[0,_weights[0]] = _values[0];
    for(int i = 1; i < _count; i++){
        //不选择第i个物品装入背包
        for(int j = 0; j <= _maxWeightInpackage; j++){
           if(states[i-1,j] >= 0) states[i,j] = states[i-1,j];
        }
        //选择第i个物品装入背包
        for(int j = 0; j <= _maxWeightInpackage - weights[i]; ++j){
            if(states[i-1,j] >= 0){
                int v = states[i-1,j] + _values[i];
                if(v > states[i,j+_weights[i]]){
                    states[i,j+weights[i]] = v;
                }
            }
        }
    }
    //找出总价值最大值
    int maxValue = -1;
    for(int j = 0; j <= _maxWeightInpackage; ++j){
        if(states[_count-1,j] > maxValue) maxValue = states[_count-1,j]；
    }
    return maxValue;
}
```

时间复杂度与空间复杂度的分析方法与上述一直，都是 O(n*w)。

## 解答问题--淘宝凑单问题

对于开篇提出来的问题，如何做到最佳推荐凑单 “满100减5” 的功能，有了上面的理解，是不是觉得这个问题可以用动态规划做出来了。

当然我们也可以用回溯递归算法来求得最后的解，但是时间复杂度是指数级别，效率不高，当商品数量 n 很多的时候，计算量恐怕活动结束了这个凑单功能都还没算出来。

这样，我们可以把上述的重量换成开篇中的价格。假设购物车有 n 个商品，用户可以自己选择每个商品是否购买。每次选择都决定后面的价格总数（状态集合）。我们还是用二维数组来记录这些状态 states[n,p]。n 的值我们知道商品的数量，那么 p 的值怎么确定呢？

我们回到 0-1 背包问题，其中的 w 是背包容纳的最大重量，那么切换到这里的问题，我们要求要满足金额要大于等于 100（即最小值为 100），所以这里就不能像之前设置的 w +1 一样。这个问题就实际而言，如果购买太多商品对于这个活动来说反而意义不大，所以我们可以限定这个值为 201。

```c#
/// <summary>
/// 
/// </summary>
/// <param name="prices">商品价格</param>
/// <param name="n">商品个数</param>
/// <param name="condition">满减条件</param>
public void DynamicProgrammingKnock(int[] prices, int n, int condition) {
    //设置状态
    bool[, ] states = new bool[n, 2 * condition + 1];
    states[0, 0] = true;
    states[0, prices[0]] = true;
    for (int i = 1; i < n; ++i) {
        for (int j = 0; j <= 2 * condition; ++j) {
            //不购买第i个商品
            if (states[i - 1, j] == true) states[i, j] = states[i - 1, j];
        }
        //购买第i个商品
        for (int j = 0; j <= 2 * condition - prices[i]; ++j) {
            if (states[i - 1, j] == true) states[i, j + prices[i]] = true;
        }
    }

    int j;
    //输出结果大于等于condition的最小值
    for (j = condition; j < 2 + condition + 1; j++) {
        if (states[n - 1, j] == true) break;
    }
    if (j == -1) return; //没有可行解
    //i表示二维数组中的行，j表示列
    for (int i = n - 1; i >= 1; --i) {
        if (j - prices[i] >= 0 && states[i - 1, j - prices[i]] == true) {
            Console.WriteLine(prices[i]+" ");//购买了这个商品
            j = j-prices[i];
        }//否则没有购买这个商品，j不变
    }
    if(j!=0) Console.WriteLine(prices[0]);
}
```

代码前半部分跟之前没什么区别，主要看后半部分

状态 (i,j) 只有可能从 (i-1,j) 或者 (i-1,j-values[i]) 两个状态推导而来。所以我们就检查这两个状态是否可达，即判断 states[i-1,j] 与 states[i-1,j-values[i]] 是否等于 true。

如果 states[i-1,j] = true，说明我们没有购买第 i 个商品。如果 states[i-1,j-values[i]] 可达，说明我们购买了第 i 个商品。我们从中选择一个可达的状态（如果两个都可达，任意选择一个），然后继续递归其他商品。