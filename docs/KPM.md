在字符串匹配算法中，KPM算法是最知名也是最常用的算法，但是在开发中，几乎是不会自己手写一个 KMP 算法的，但是 KPM 算法原理，思想是要知道的。

实际上，KMP 算法与 BM 算法本质上是一样的，也要借助好后缀的思路。

# KMP 算法基本原理

KPM 算法是根据三位作者（D.E.Knuth，J.H.Morris，V.R.Pratt）的名字来命名的，算法的全称是 Knuth Morris Pratt 算法，简称 KMP 算法。

KMP 算法核心思想与 BM 算法非常相似。我们假设主串是 a，模式串是 b。在模式串与主串匹配的过程中，当遇到不匹配的字符时，我们要找出一些规律来将模式串能往后多移几位，以跳过那些肯定不能匹配的字符。

与 BM 一样，在模式串与主串匹配的过程中，我们把不匹配的字符叫**坏字符**，把已经匹配的那段子串叫**好前缀**。

![](https://static001.geekbang.org/resource/image/17/be/17ae3d55cf140285d1f34481e173aebe.jpg)

当遇到坏字符时，我们要把模式串往后移，我们从 BM 分析知道，我们不能简单的直接移到坏字符的右边，这样会导致过度滑动的情况。我们要在好前缀中找出与模式串的前缀子串相重合的位置，然后将模式串移到相对应的位置。

那么如何高效的在好前缀中查找与模式串的前缀子串呢？

![](https://static001.geekbang.org/resource/image/f4/69/f4ef2c1e6ce5915e1c6460c2e26c9469.jpg)

通过找规律我们发现，要拿好前缀本身中的后缀子串中找一个最长的可匹配的前缀子串。

假设最长可匹配的那部分的前缀子串时 {v}，长度为 k。我们把模式串一次性往后移动 j-k 位，相当于每次遇到坏字符时，我们就把 j 更新位 k，i 不变，然后继续比较。

![](https://static001.geekbang.org/resource/image/da/8f/da99c0349f8fac27e193af8d801dbb8f.jpg)

这里有两个概念，我们把好前缀的所有后缀子串中，最长的可匹配前缀子串的那个后缀子串称为**最长可匹配后缀子串**；对应的前缀子串称为**最长可匹配前缀子串**。通过图更好的解释

![](https://static001.geekbang.org/resource/image/9e/ad/9e59c0973ffb965abdd3be5eafb492ad.jpg)

> [阮一峰的此篇文章](http://www.ruanyifeng.com/blog/2013/05/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm.html)，简洁清晰的介绍了好前缀匹配的最长后缀子串与最长前缀子串的过程，建议阅读。

我们发现，求好前缀的最长可匹配前缀子串与最长可匹配后缀子串可以直接通过模式串的好前缀求得，不涉及主串。所以我们可以预先计算好模式串，等再模式串和主串匹配时可以直接拿来用。

类似 BM 算法的 bc、suffix、preffix 数组，KMP 算法也维护一个数组，**用来存储模式串中每个前缀（这些前缀都有可能是好前缀）的最长可匹配前缀子串的结尾字符下标**，我们把这个数组称为 **next 数组**，这个数组有个书面用语，叫**失效函数（failure function）**。

数组下标是每个前缀子串结尾的字符下标，**next 数组的值是这个前缀的最长可匹配的前缀子串结尾字符的下标**。看如下图

![img](https://static001.geekbang.org/resource/image/16/a8/1661d37cb190cb83d713749ff9feaea8.jpg)

有了 next 数组我们就可以很容易的实现 KMP 算法。然后我们先假设 next 失效函数已经计算好了，那么就有如下代码：

```c#
public static int Kmp(string mainString, string matchString) {
    int[] next = GetNexts(matchString);
    int j = 0;
    for (int i = 0; i < mainString.Length; i++) {
        while (j > 0 && mainString[i] != matchString[j]) { //一直找到主串[i]和模式串[j]
            j = next[j - 1] + 1;
        }
        if (mainString[i] == matchString[j])
            ++j;
        if (j == matchString.Length) //找到匹配模式串
            return i - matchString.Length + 1;
    }
    return -1;
}
```

接下来就是重点——失效函数的计算

# 失效函数计算方法

next 数组的计算，我们可以用遍历的方法，比如要计算下面这个模式串 b 的 next[4]，我们就把 b[0,4] 的所有后缀子串从长到短找出来，依次看看是否能跟模式串的前缀子串匹配。很显然这个方法比较笨，效率不高。那么有没有更高效的方法呢？

![img](https://static001.geekbang.org/resource/image/1e/ec/1ee5bea573abd033a6aa35d15ef0baec.jpg)

这里的处理非常巧妙，类似于动态规划。

我们按照下标从小到大，依次计算 next 数组的值。当我们要计算 next[i] 的时候，前面的 next[0]、next[1]... next[i-1] 都已经计算过了。那么我们能否根据已经计算出来的 next 的值，快速推导出 next[i] 的值呢？

如果 next[i-1]=k-1，也就是说，模式串中的子串 b[0,k-1] 是 b[0,i-1] 的最长可匹配前缀子串。如果子串 b[0,k-1] 的下一个字符 b[k]，与 b[0,i-1] 的下一个字符 b[i] 匹配，那么子串 b[0,k] 是 b[0,i] 的最长可匹配前缀子串。即 next[i]=k。但是如果 b[0,k-1] 的下一个字符串b[k] 与 b[0,i-1] 的下一个字符 b[i] 不想等呢？这个时候就不能简单的通过 next[i-1] 得到 next[i] 了。

![img](https://static001.geekbang.org/resource/image/4c/19/4caa532d03d3b455ca834245935e2819.jpg)

我们假设 b[0, i] 的最长可匹配后缀子串是 b[r, i]。如果我们把最后一个字符去掉，那 b[r, i-1] 肯定是 b[0, i-1] 的可匹配后缀子串，但不一定是最长可匹配后缀子串。所以，既然 b[0, i-1] 最长可匹配后缀子串对应的模式串的前缀子串的下一个字符并不等于 b[i]，那么我们就可以考察 b[0, i-1] 的次长可匹配后缀子串 b[x, i-1] 对应的可匹配前缀子串 b[0, i-1-x] 的下一个字符 b[i-x] 是否等于 b[i]。如果等于，那 b[x, i] 就是 b[0, i] 的最长可匹配后缀子串。

![](https://static001.geekbang.org/resource/image/2a/e1/2a1845b494127c7244c82c7c59f2bfe1.jpg)

那么如何求得 b[0,i-1] 的次长可匹配后缀子串呢？次长可匹配后缀子串肯定被包含在最长匹配后缀子串中，而最长可匹配后缀子串又对应最长可匹配前缀子串 b[0,y]。于是，查找 b[0,i-1] 的次长可匹配后缀子串，这个问题就变成了，查找 b[0,y] 的最长可匹配后缀子串的问题了。

![](https://static001.geekbang.org/resource/image/13/13/1311d9026cb6e0fd51b7afa47255b813.jpg)

按照这个思路，我们可以考察完所有的 b[0,i-1] 的可匹配后缀子串 b[y,i-1]，直到找到一个可匹配的后缀子串，它对应的前缀子串的下一个字符串等于 b[i]，那这个 b[y,i] 就是 b[0,i] 的最长可匹配后缀子串。

把这部分的代码写出来，与之前写的方法结合在一起就是完整的计算 next 数组的算法实现

```c#
private static int[] GetNexts(string matchString) {
    int[] next = new int[matchString.Length];
    next[0] = -1;
    int k = -1;
    for (int i = 1; i < matchString.Length; i++) {
        while (k != -1 && matchString[k + 1] != matchString[i]) {
            k = next[k];
        }
        if (matchString[k + 1] == matchString[i]) {
            ++k;
        }
        next[i] = k;
    }
    return next;
}
```

# KMP 算法复杂度分析

空间复杂度很容易分析，KMP 算法只需要一个额外的 nexts 数组，数组大小跟模式串一样。所以空间复杂度是 O(m)，m 是模式串的长度，下同。

时间复杂度主要消耗在两个部分，一是构建 next 数组，第二是借助 next 数组完成匹配。

先来分析第一部分的时间复杂度。

计算 next 数组的代码中，第一层 for 循环中 i 从 1 到 m-1，也就是说代码被执行了 m-1 次。for 循环内部还有一个 while 循环，我们每次循环 for、while 循环平均执行的次数假设是 k，那时间复杂度就是 O(m*k)。但是，while 循环之行的次数也不好统计，所以这种分析方法我们放弃。

我们可以找一些参照变量，i 和 k。i 是从 1 开始自增长到 m，而 k 并不是每次 for 循环增加就增加的，所以 k 积累的值肯定是小于 i。而 while 循环里 k=next[k] 总的执行次数也不会超过 m。因此 next 数组计算的时间复杂度就是 O(m)。

接着是第二部分的时间复杂度分析。

i 从 0 循环增长到 n-1（n 为主串的长度），j 的增长量不可能超过 i，所以肯定小于 n。而while 循环中的语句 j=next[j-1]+1，不会让 j 增长的，那有没有可能让 j 不变呢？也没可能。因为 next[j-1] 的值肯定小于 j-1，所以 while 循环中的这条语句实际上也是在让 j 的值减少。而 j 总共增长的量都不会超过 n，那减少的量也不可能超过 n，所以 while 循环中的这条语句执行的总次数也不会超过 n，所以这部分的时间复杂度就是 O(n)。

所以，综合两部分的时间复杂度，KMP 算法的时间复杂度就是 O(m+n)。

