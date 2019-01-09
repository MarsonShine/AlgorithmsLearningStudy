# 字符串查找匹配算法

在平常的开发中，我们经常用到的功能就包含字符串查找、根据提供的子字符串来在源字符串中匹配。实现的算法也有很多中，我们这节主要介绍两种实现方法：BF 算法和 PK 算法。

## BF（暴力匹配） 查找匹配

BF 全称是 “Brute Force”，暴力匹配算法，顾名思义，这种算法匹配方式很暴力，因为它很简单易懂，但伴随着是性能的不高。

BF 算法：**我们在主串中，检查位置从起始位置分别从0，1，2…n-m 且长度为 m 的 n-m+1 个子串，看有没有根模式串匹配的。**

举个例子，这里有主串 “aaaaa....aaaa” ，模式串是 “aaaab”（m=5）。那么我们匹配模式串，都对比 m 个字符，要对比 n-m+1 次，所以，这种算法的的最坏时间复杂度为 O(m*n)。

虽然时间复杂度非常高，但是在实际开发中，它却是一个比较常用的字符串匹配算法。

第一，因为在实际开发中，基本上不会碰到主串非常长。并且在匹配到不能匹配的字符的时候，就可以停止了，而不会把 m 个字符都比对一次。

第二，BF 算法代码实现非常简单。不容易出错，如果有 bug 也容易修复。

## RK 算法

我们通过哈希算法对主串中的 n-m+1 个子串分别求哈希值，然后逐个与模式串的哈希值比较大小。如果某个子串的哈希值与模式串相等，那就说明对应的子串和模式串匹配了。因为哈希是一个数字，数字之间的比较是很快的，所以 RK 算法要比 BF 算法快很多。

在 通过哈希算法求子串的哈希值时，我们还是需要遍历子串中的每个字符。尽管在模式串与子串比较的效率提高了，但是整体效率并没有提高。有没有办法可以提高哈希算法子串哈希值的效率呢？

这里引用极客时间专栏中的描述

> 假设要匹配的字符集只包含 K 个字符，我们可以用一个 K 进制来表示一个子串，这个K 进制树转化成十进制，作为子串的哈希值。
>
> 比如要处理的字符只包含 a-z 26 个小写字母，那我们就用二十六进制来表示一个字符串。我们把 a-z 这26个字母映射到 0-25个数字，就是 0 表示a，1 表示b以此类推，z 表示 25。

![](https://static001.geekbang.org/resource/image/d5/04/d5c1cb11d9fc97d0b28513ba7495ab04.jpg)

上面的 “657” 通过右边十进制算出来的，“cba” 则同样依次规则根据十六进制计算得知。

这种哈希算法有一个特点，在主串中，相邻的两个子串的哈希值的计算公式有一定的规律。

![](https://static001.geekbang.org/resource/image/f9/f5/f99c16f2f899d19935567102c59661f5.jpg)

由此我们可以得出这样的规律：相邻的两个子串 s[i-i] 和 s[i] （i 表示子串在主串中的起始位置，子串的长度为 m），对应的哈希值计算公式有交集，即我们可以根据 s[i-1] 的哈希值很快的计算出 s[i] 的哈希值。用公式就是：

```
h[i] = 26*(h[i-1]-26^(m-1)*(s[i-1]-'a')) + (s[i+m-1] - 'a');
其中，h[i]、h[i-1] 分别对应 s[i] 和 s[i-i] 两个子串的哈希值
```

其中 26^(m-1) 这部分的计算，我们可以通过查表的方式来提高效率。我们事先将计算好的 26^0、26^1、26^2...26^(m-1)，并且存储在长度为 m 的数组中。其中次方就代表数组下标。这样，当我们计算 26 的 x 次方的时候，可以直接在数组对应的下标 x 取值从而免去计算。

![](https://static001.geekbang.org/resource/image/22/2f/224b899c6e82ec54594e2683acc4552f.jpg)

### 时间复杂度分析

整个 RX 算法包括两个部分，计算子串哈希值和子串哈希值与模式串哈希值的比较。

第一部分，通过设计一个特殊的哈希算法，只需要扫描一遍主串就能计算出所有的子串的哈希值，所以这部分的时间复杂度是 O(n)。

模式串哈希值与每个子串哈希值的比较的时间复杂度是 O(1)。共需要比较 n-m+1 个子串的哈希值，所以，这部分的时间复杂度也是 O(n)。所以整体的 RX 算法整体时间复杂度就是 O(n)。

那么，当主串长度非常长的时候（子串长度也是如此），通过上述哈希算法就会得到非常大的数组，可能会超过整数的最大数值，这时候该如何解决呢？

刚刚我们设计的哈希算法也是没有哈希冲突的，也就是说一个字符串与一个二十六进制数是对应的，不同的字符串哈希值肯定不一样。实际上，我们为了能讲哈希值能在整数范围内，我们可以选择牺牲一下，允许哈希冲突。这种情况该如何解决呢？

哈希算法的方式有很多，比如把 a-z 对应自然数 1-26 个数字。一个字符串，我们可以把对应的数字相加之和作为最后的哈希值进行比较。当然这种方式发生的哈希冲突概率比较大，那么我们可以选择素数或者质数相加之和作为哈希值，这样哈希值冲突的概率就会相对小些。

那么当哈希冲突的时候该怎么办呢，很简单，当判断哈希值一样的情况下，接着判断字符串本身是否一样即可。如果哈希值不一样，自然字符串本身不同。