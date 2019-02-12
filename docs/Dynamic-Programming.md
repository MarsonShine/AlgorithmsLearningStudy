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

