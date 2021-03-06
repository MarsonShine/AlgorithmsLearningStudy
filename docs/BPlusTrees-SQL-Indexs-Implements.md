# B+树：数据库索引是如何实现的

我们在大数据存储都是放到关系型数据库和文档性数据库中，其中很大一部分原因是，比直接把数据存放到文本文件中对数据的操作是要快很多的。

我们现在考虑关系型数据库，如 Mysql，对数据的查询是很快的，特别是在建立索引之后，效率更加显著，考虑下面的查询操作

```mysql
/*根据条件id查询指定的用户*/
select name from users where id = 100000;
/*根据指定的范围查询所有符合的用户*/
select name from users where id > 100000 and id < 520000;
```

我们在考虑这个查询的时候，如何提高它的效率性能呢？提到性能，我们就能想到时间复杂度和空间复杂度。

用什么数据结构或算法能让时间复杂度低的情况下且空间消耗也少。

## 用已学的数据结构思考问题

我们目前学到的数据结构主要分三种，看符不符合上面的查询情况

- 散列表：散列表的查询，添加，删除操作都是O(1)，支持单点条件操作，这一点没问题。但是如果要查询某个区域范围的数据快速查询，散列表明显效率不高。
- 平衡二叉查找树：这个数据结构的数据操作时间复杂度是 O(nlogn)，查询性能是很高的，而且通过中序遍历可以得到从小到大的数据序列，但是它仍然不支持范围区域快速查询（因为存在横跨结点）。
- 跳表：跳表是在链表的基础之上增加了多层索引构成的。它支持快速查询，插入，删除数据，时间复杂度是 O(logn)。并且链表本身是有序的，在基础之上加了索引，所以也是支持范围区域查询的。我们只需要在链表上确定索引的范围，找到对应的范围节点，从这个节点开始，顺序遍历直到终点对应的节点即可。

![img](https://static001.geekbang.org/resource/image/49/65/492206afe5e2fef9f683c7cff83afa65.jpg)

这样看，看似能解决之前我们提出来的问题，那么数据库索引所用到的数据结构是不是跳表结构呢？其实不是，而是跟跳表很像的 B+树结构。它是通过二叉树演变过来的。所以我们来看下，B+ 树是如何通过二叉树演变为现在的数据结构的。

## 改造二叉树

为了让二叉树支持范围查询，我们抽离一下数据结构：结构树中的结点并不存储数据本身，只是作为索引，所有的数据都存在叶子节点上。在把叶子节点横向关联在一起，形成一个链表结构。改造后的结构如下图所示：

![img](https://static001.geekbang.org/resource/image/25/f4/25700c1dc28ce094eed3ffac394531f4.jpg)

如果我们要查询指定范围的数据的话，我们在树中查找符合起始条件的叶子节点之后，在往后继续遍历这个叶子节点形成的链表直到终点即可，这样所遍历的数据就是符合范围的数据。

![img](https://static001.geekbang.org/resource/image/1c/cc/1cf179c03c702a6ef5b9336f5b1eaecc.jpg)

但是如果我们像这样的结构要构成成千上万乃至亿计量数据时，尽管数据访问非常搞笑，但是所占的内存却非常高。

比如我们给一亿个数据构建二叉查找树索引，索引会包含大约 1 亿个节点，那么我假设每个节点所占字节是 16 字节，那么一亿个数据就要占据 1GB 的内存空间。给一张表简历索引就需要 1GB 内存，这样明显太占内存。有什么办法来解决内存占用过多的问题呢？

我们可以用时间换空间的概念来解决这个问题，我们尝试把索引建在主存上（硬盘），因为硬盘在计算机内存当中是很慢的，不比 CPU 三道高速缓存以及 DRAM，同样的数据，我们从硬盘访问这些索引，要比内存慢上万倍甚至是几十万倍。

这种把索引存在硬盘这种解决方案，虽然解决了内存的问题，但是查询的效率会下降很多。

二叉查找树经过改造之后，支持区间查找，并且把索引存到硬盘上来节省主存上的空间，并且每次取索引节点，都意味着一次 IO（CPU -> IO总线 -> 硬盘）操作。树的高度就等于每次查询数据时 IO 操作数的次数。

我们知道，比起内存存取数据，磁盘 IO 操作非常耗时，所以我们要尽可能的减少 IO 的操作数，所以就要减小树的高度。16 个数据构建二叉查找树，树的高度为4，我们要查一次索引要经过 4 次 IO 操作（假设只有顶点存在内存，其他节点存在磁盘中）。如果对这 16 个数据构建的是一个 5 叉树，那么树的高度就只有 2，也就是只需要  次 IO 操作。那么如果是 m 叉树，其中 m 为 100，1 亿个数据就只需要 3 次，最多 3 次磁盘 IO 就能获取到数据。IO 操作数减少，自然查询效率就提高了。

![img](https://static001.geekbang.org/resource/image/69/59/69d4c48c1257dcb7dd6077d961b86259.jpg)

对于相同的数据库，还是磁盘中的数据，m 叉树索引，m 越大，树的高度就越小，速度就越快，那么是不是 m 就越大越好呢？m 的值到底要多少才能合适？

**不管是在内存中的数据，还是磁盘中的数据，操作系统都是按页（以页通常是 4kb，sqlserver 一页是 8 kb）来读取的，一次读一页数据。如果查询超过一页的数据，那么就会触发多次 IO 操作。所以我们选择 m 大小的时候，要尽量让每个节点的大小等于一页的大小。读取一个节点只要一次 IO 操作。**

![img](https://static001.geekbang.org/resource/image/ea/30/ea4472fd7bb7fa948532c8c8ba334430.jpg)

索引虽然会提高查询数据的效率，但是随着而来也会带来另外一个问题，那就是数据写入操作变慢。因为这些操作会引起索引的更新。

对于一个 m 叉树来说，m 是事先计算好的值，也就是说每个节点最多只有 m 个子节点。当往里插入数据时，就会势必拆过 m 个节点数，这样节点的大小就超过了一页的数据，这样就会触发多次 IO 操作。那么如何解决这个问题呢？

当插入一个数据时，我们只需要将这个节点裂变成两个节点。但是这样裂变之后，上层的父节点的子节点个数就会超过 m 个。不过没关系，我们继续裂变父节点为两个节点。这种级联的往上，一直到根节点。如下图所示：

![img](https://static001.geekbang.org/resource/image/18/e0/1800bc80e1e05b32a042ff6873e6c2e0.jpg)

同样，我们在删除节点数据时，也要更新索引，速度也会相应减慢。这个处理类似于跳表的删除操作，删除的数月多，就会导致某个节点下的子节点数量越来越少，长此以往，如果每个节点的子节点的数量都越来越少，势必会影响索引的效率。

我们可以设置一个阈值，假设这个阈值是 m/2，当删除的节点数量少于阈值时，我们就将这个子节点相邻的兄弟节点合并，这样的话就可能会导致子节点的数目超过 m 个，那么我们就只需要根据插入数据一样裂变就好了。

![img](https://static001.geekbang.org/resource/image/17/18/1730e34450dad29f062e76536622c918.jpg)

## 总结

B+ 树，它通过存储在磁盘的多叉树结构，既能高效的查询数据，又能平衡空间节省内存。

B+ 树有以下几个特点：

- 每个节点中子节点的个数不能超过 m，也不能小于 m/2。
- 根节点的子节点的个数不能超过 m/2，这是例外
- m 叉树子节点只存储索引，不存储数据值。
- 通过链表将叶子节点链接起来，这样可以方便查询
- 一般情况下，根节点会存储在内存中，其他子节点存储在文件系统中。



相关参考：

[红黑树](BlackRedTree.md)

[散列表](HashSet.md)

