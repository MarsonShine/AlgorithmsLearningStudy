# Boyer-Moore 算法

在查找字符串的算法当中，BM（Boyer-Moore ）算法是目前被认为最高效的字符串搜索算法，比 KMP 算法性能高 3，4 倍。今天我们就来学习一下 BM 算法

## BM 核心思想

我们把模式串 P 和 主串 T 的匹配过程，看作模式串在主串中不停的往后移。当遇到不匹配的字符串时，BF 算法和 RK 算法的做法是，模式串往后移一位，然后从模式串的第一个字符串重新匹配。

![](https://static001.geekbang.org/resource/image/43/f9/4316dd98eac500a01a0fd632bb5e77f9.jpg)

从图中可以看出，字符 d 与主串对应的字符 c 是不匹配的，所以 BF 和 RK 算法的做法就是往后移一位，然后继续判断。但是我们知道，前面两个字符是公共的，往后移一位肯定还是无法匹配。我们可以直接一次性往后多移 3 位，把模式串移到主串字符 c 的后面。

那么由这种现象，我们是否可以推算出某种规律。根据这种规律，在模式串与主串匹配的过程中，当遇到不匹配的字符时，肯定能多跳过几位的情况。

## BM 算法分析

BM 算法包括 “**好字符规则**” 和 “**坏字符规则**”

### 坏字符规则（bad-character-rule）

BM 算法在匹配模式串时不同于之前的都是从前往后匹配，而是从后往前匹配（从结尾到起始点）。如果碰到不能匹配的字符串，我们就把它称之为**坏字符**（主串中的字符） 如图所示

![](https://static001.geekbang.org/resource/image/22/da/220daef736418df84367215647bca5da.jpg)

我们拿坏字符 c 在模式串中查找，发现在模式串不存在这个字符，即与模式串中的所有字符都不匹配。这个时候我们就把模式串在主串中往后移 3（模式串长度） 位的位置，然后接着从模式串尾字符开始比较。

![](https://static001.geekbang.org/resource/image/4e/64/4e36c4d48d1b6c3b499fb021f03c7f64.jpg)

接着我们发现模式串尾字符 d 在主串对应的位置 a 无法匹配，那么我们是不是继续往后移 3 位呢？答案是不行。因为字符 a 在模式串时存在的，存在于位置 0。这种情况下，我们可以让字符 a 对齐——往后移动 2 位。然后从模式串尾部继续匹配主串。

我们继续来看，第一次移位移动了 3 位，第二次移动了 2 位。具体移动多少位，是不是有规律呢？

当发生不匹配的情况时，我们把坏字符对应的模式串中的字符下标记作 si。如果坏字符串在模式串中存在，我们把存在对应的模式串的字符下标记作 xi。如果不存在，就把 xi 记为 -1。那模式串移动的位数就是 si - xi 位。

![](https://static001.geekbang.org/resource/image/8f/2e/8f520fb9d9cec0f6ea641d4181eb432e.jpg)

如果存在的坏字符串含有多个，我们就记录最靠后的字符下标为 xi。

利用坏字符规则，时间复杂度在最好情况下时很低的，只有 O(n/m)。比如，aaabaaabaaab 的主串，aaaa 的模式串，这个时候就可以直接往后移 4 位。算法非常高效。

但是，单纯使用坏字符规则时不够的。因为根据给出来的移动位数公式，有可能计算出来的是个负数。比如主串是 aaaaaaaaaaaaa，模式串是 baaa。不但不会往后移动模式串，还有可能倒退。所以 BM还需要借助 “好字符规则”。

### 好字符规则（good-character-rule）

思路与坏字符规则类似，如图

![](https://static001.geekbang.org/resource/image/d7/8a/d78990dbcb794d1aa2cf4a3c646ae58a.jpg)

当模式串滑倒图中的位置时，模式串与主串有两个字符时匹配的，倒数第三个字符不匹配的情况。

这个时候，我们把已经匹配的 b，c 字符叫作**好后缀**，记为 {u}。然后我们在模式串找出另一个与好后缀匹配的字符串记作 {u*}，那我们就把模式串移动到子串 {u\*} 与主串中的 {u} 相同的位置。

![](https://static001.geekbang.org/resource/image/b9/63/b9785be3e91e34bbc23961f67c234b63.jpg)

 如果在模式串中找不到与 {u} 的子串，我们就直接将模式串移动到主串好后缀 {u} 的后面，因为之前的任何一次滑动都不会匹配到 {u}。

但是，这样直接将模式串滑动到主串好后缀的后面，这样会不会滑动过度了呢？来看下面这个图

![](https://static001.geekbang.org/resource/image/9b/70/9b3fa3d1cd9c0d0f914a9b1f518ad070.jpg)

如果好后缀在模式串找不到匹配的子串，那么我们一步步往后滑动，只要与好后缀有重叠，就肯定无法匹配。但是如果模式串滑动到前缀与主串中的 {u} 有部分重叠，并重叠的部分有相等的时候，就会存在可能完全匹配的时候。

![](https://static001.geekbang.org/resource/image/05/23/0544d2997d8bb57c10e13ccac4015e23.jpg)

所以针对这种情况，我们不仅要看好后缀在模式串中，是否有另一个匹配的子串，我们还要考虑好后缀的后缀子串是否存在跟模式串的前缀子串有匹配的。

所谓某个字符串 s 的后缀子串，就是该串 s 除去首字符的各个字符的顺序组合，比如字符串 “abc” 的后缀子串就是 “b,bc”。而所谓前缀子串则是反过来，出去最后一个字符的不相同的顺序组合，比如字符串 “abc” 的前缀子串就是 “a,ab”。

我们从好后缀的后缀子串中，找一个最长的能与模式串的前缀子串匹配的，假设时 {v}，然后将模式串滑动到如图所示位置

![](https://static001.geekbang.org/resource/image/6c/f9/6caa0f61387fd2b3109fe03d803192f9.jpg)

好字符规则和坏字符规则的原理分析已经讲的差不多了，那么我们该如何计算出我们到底该滑动多少位呢？在不匹配的情况下到底是用好字符规则还是坏字符规则？

我们可以从分别计算好后缀和坏字符往后滑动的位数，取其中最大值，作为模式串滑动的位数。

## BM 算法的代码实现

“坏字符串” 不难理解，当遇到坏字符串时候，我们如何快速从模式串中找到坏字符串的位置呢？如果是循环模式串的话，那太低效了。有什么办法能提高查找对应字符的速度吗？其实我们能用一个哈希表来把模式串中的字符出现的位置都记下来，这样遇到坏字符串查对应的位置算法是 O(1) 的。

关于这个散列表我们只实现一种最简单的情况，假设字符串的字符集不大，每个字符长度都只有 1 字节，我们用 256 的数组来记录每个字符在模式串出现的位置。数组的下标对应字符的 ASCII 码值，数组存储的值就是字符在模式串出现的位置。

![](https://static001.geekbang.org/resource/image/bf/02/bf78f8a0506e069fa318f36c42a95e02.jpg)

对应的代码就是下面的代码片段。其中，变量 b 是模式串，bc 就是散列表

```c#
private const int SIZE = 256；
private void GenerateBC(string b, int[] bc) {
    for (int i = 0; i < SIZE; i++) {
        bc[i] = -1; //初始化散列表
    }
    for (int i = 0; i < b.Length; i++) {
        int ascii = (int) b[i];
        bc[ascii] = i; //出现的字符转成对应的ASCII码作为下标记录在模式串出现的位置
    }
}
```

散列表写好之后，我们先把 BM 算法大框架搭好，先只考虑坏字符规则，不考虑 si-xi 计算出来的移动位置为负的情况。

```c#
public void BoyerMoore(string a, string b) {
    int[] bc = new int[SIZE]; //记录模式串中每个字符最后出现的位置
    GenerateBC(b, bc); //构建坏字符串哈希表
    int i = 0; //表示主串与模式串对齐的第一个字符
    while (i <= a.Length - b.Length) {
        int j;
        for (int j = b.Length - 1; j >= 0; j--) { //模式串从后往前匹配
            if (a[i + j] != b[j]) break; //坏字符串对应模式串中的下标是 j
        }
        if (j < 0)
            return i;
        //将模式串后移动j-bc[(int)a[i+j]]位
        i = i + (j - bc[(int) a[i + j]]);
    }
    return -1;
}
```

用图更好的体现上面的滑动位数计算过程

![](https://static001.geekbang.org/resource/image/53/c6/5380b6ef906a5210f782fccd044b36c6.jpg)

因为之前说到了在坏字符规则下可能存在滑动位数为负数的情况，所以我们还需要好后缀规则。

我们回顾一下之前提到的好后缀规则的主要思想：

- 在模式串中寻找跟好后缀匹配的字符串；
- 在好后缀的后缀子串中，查找最长的，能跟模式串前缀子串匹配的后缀子串；

这部分都可以用暴力算法来匹配，但是这种方式就很低效，那么如何改进呢？

**因为好后缀本就是模式串本身的后缀子串，所以我们在匹配模式串和主串时，可以预处理模式串的所有的后缀子串，计算好后缀子串匹配的位置。**

后缀子串的最后一个字符时确定的，就是字符串长度 m - 1。

![](https://static001.geekbang.org/resource/image/77/c8/7742f1d02d0940a1ef3760faf4929ec8.jpg)

现在我们要引入一个**suffix关键数组**。suffix 数组的下标 k 表示后缀子串的长度，下标对应数组存储的值是在模式串中与好后缀相匹配的子串的起始下标值。看图比较容易理解

![](https://static001.geekbang.org/resource/image/99/c2/99a6cfadf2f9a713401ba8feac2484c2.jpg)

如果在模式串有多个子串与后缀子串 {u} 匹配，那 suffix 数组存储的是下标最大的那个起始位置。这样还不够，我们不仅在模式串要找出跟好后缀匹配的另一个子串，还要在好后缀的后缀子串中，找出一个最长的能跟模式串前缀子串相匹配的后缀子串。

之前的关键数组 suffix 只是处理规则的前半部分，也就是在模式串中找到与好后缀匹配的另一个子串。所以除了 suffix 之外，还要有一个 boolean prefix 数组来表示在好后缀的后缀子串中有一个最长的能跟模式串的前缀子串相匹配。

![](https://static001.geekbang.org/resource/image/27/83/279be7d64e6254dac1a32d2f6d1a2383.jpg)

注意：上图的模式串的前缀子串分别是：c,ca,cab,cabc,cabca。对应到上面表格 prefix 具体的值。

那么如何来填充这两个数组呢？

我们先拿 o 到 i（i 可以是 0 到 m-2 ）的子串与模式串匹配找出公共后缀子串。如果公共后缀子串的长度为 k，我们就记录 suffix[k] = j（j 就是公共后缀子串的起始下标）。如果 j 等于 0，也就是说，公共后缀子串也就是模式串的前缀子串，我们就记录 prefix[k] = true。

![](https://static001.geekbang.org/resource/image/57/7c/5723be3c77cdbddb64b1f8d6473cea7c.jpg)

我们把上面 suffix 和 prefix 数组的计算过程用代码表示就是

```c#
private void GenerateGC(string mainString, string matchString, int[] suffix, bool[] prefix) {
    for (int i = 0; i < mainString.Length; i++) {
        //初始化
        suffix[i] = -1;
        prefix[i] = false;
    }
    for (int i = 0; i < mainString.Length - 1; ++i) {
        int j = i;
        int k = 0; //公共后缀子串长度
        while (j >= 0 && mainString[j] == mainString[mainString.Length - 1 - k]) {
            --j;
            ++k;
            suffix[k] = j + 1; //j+1 表示公共后缀子串在 b[0,i] 中的起始下标
        }
        if (j == -1) prefix[k] = true; //如果公共后缀也是模式串中的前缀子串
    }
}
```

有这两个数组之后，剩下的关键就是如何计算，在遇到不匹配的字符时，我们如何根据好后缀规则移动模式串的位数？

假设好后缀的长度为 k，我们先用好后缀查得在 suffix 数组中对应匹配的子串位置。如果 suffix[k]=-1 就表示不存在匹配的好后缀的字符串，如果不等于 -1，就说明存在匹配的子串，就要将模式串往后移动 j-suffix[k]+1 位（j 是坏字符对应模式串中的下标位置）。

就可以用下面的规则来计算滑动的位数

![](https://static001.geekbang.org/resource/image/1d/72/1d046df5cc40bc57d3f92ff7c51afb72.jpg)

好后缀的后缀子串 [r,m-1] 的长度为 k=m-r，如果 prefix[k]=true，就说明长度为 k 的后缀子串是模式串的前缀子串，我们可以把模式串往后移动 r 位。

![](https://static001.geekbang.org/resource/image/63/0d/63a357abc9766393a77a9a006a31b10d.jpg)

如果两条规则都找不到可匹配的好后缀及其后缀子串的子串，我们就整个后移 m 位。

完整代码实现如下：

```c#

```

## 延伸资料

http://www.ruanyifeng.com/blog/2013/05/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm.html

http://www.ruanyifeng.com/blog/2013/05/boyer-moore_string_search_algorithm.html

[BM算法资料1]: http://www.cs.jhu.edu/~langmea/resources/lecture_notes/boyer_moore.pdf





