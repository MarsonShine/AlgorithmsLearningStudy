# AC自动机：用多模式串匹配实现敏感词过滤功能

我们经常在一些社交 app 当中有脏、淫秽、赌毒等词汇，系统检测到了之后会自动实现过滤用 “***” 来代替，实现的方式有很多种，比如正则式匹配等。但是当一个系统用户量非常巨大的时候，如果没有一个高性能的字符串过滤算法来支撑的话，那么当用户发送信息的时候，检测出来在发出来都要好几秒了。

# 基于单模式串和 Trie 树实现的敏感词功能

我们前面有提到很多种字符串匹配算法，有 BF 算法、RK 算法、KMP 算法以及 Trie 树。前四种都是单模式串匹配算法，只有 Trie 树式多模式匹配。

单模式串是，一个模式串和一个主串进行匹配，即在主串中查找一个模式串。

多模式串是，多个模式串和一个主串进行匹配，即在主串中查找多个模式串。

要知道，单模式串也可以完成多模式串匹配，我们可以针对敏感词汇，通过单模式匹配算法（KMP 算法）与用户输入的文字内容进行匹配。但是这样的做的话，每次匹配过程就得重新扫描一次用户输入的内容。特别是当敏感词汇很多的时候，用户输入的内容非常长时比如上千个字符，我们就要需要扫描上千个这样的字符，这样时非常低效的。

这个时候，多模式串匹配就显得比单模式要适合的多。**它只需要扫描一遍用户输入的内容（主串），就能在主串中一次性查找多个模式串是否匹配，这样就大大提高了匹配效率**。我们知道 Trie 树就是一种多模式匹配算法。那么如何用 Trie 树实现过滤呢？

我们可以对敏感词字典进行预处理，构建成 Trie 树结构。这个预处理的操作只需做一次，如果敏感词更新了，例如删除、添加了一个敏感词，我们就动态的更新 Trie 树就可以了。

当用户输入一个文本内容时，我们把用户输入的内容作为主串，从第一个字符（假设是字符 C）开始，在 Trie 树中匹配。当匹配到 Trie 树的叶子结点，或途中遇到不匹配的字符时，我们就将主串的的开始位置后移一位重新在 Trie 树中匹配。

基于 Trie 树的这种匹配方式就好像 BP 匹配算法，我们知道 KMP 算法在 BP 算法基础之上加了一个 next 数组来控制往后移的位数，来提高效率。那么基于 Trie 树的这种匹配算法有没有优化的方法呢？这就是 AC 自动机算法了。

# 经典的多模式串匹配算法：AC 自动机

AC 自动机算法，全称是 “Aho-Corasick” 算法。其实 Trie 树和 AC 自动机之间的关系，就像是 BP 算法与 KMP 的算法关系一样。**AC 自动机算法就跟 KMP 算法一样，加了 next 数组，只不过此处的 next 数组是构建在树上罢了。**代码如下：

```c#
public class AcNode {
    public char Data { get; set; }
    public AcNode[] children = new AcNode[26]; //假设字符串只包含这26个字母
    public bool IsEndingChar = false; //结尾字符为 true
    public int length = -1; //当IsEndingChar=true时，记录模式串长度
    public AcNode(char data) {
        this.Data = data;
    }
}
```

AC 自动机算法的构建包括两个步骤：

- 将多个模式串构建成 Trie 树；
- 在 Trie 树上构建失败指针（相当于 KMP 中的失效函数 next 数组）。

我们来看下，**如何构建 Trie 树上的失败指针？**

这里有四个串，分别是 c，bc，bcd，abcd；主串是 abcd。

![](https://static001.geekbang.org/resource/image/f8/f1/f80487051d8f44cabf488195de8db1f1.jpg)

Trie 树中的每一个结点都有一个 next 数组。假设我们沿 Trie 树走到 p 点，也就是下图中的紫色结点，那么 p 的失败指针就是从 root 走到紫色结点形成的字符串 abc，跟所有模式串前缀匹配的最长可匹配后缀子串，就是箭头指向的 bc 模式串。

这里解释一下**最长可匹配后缀子串**。字符串 abc 的后缀子串有 bc，c 两个，**我们拿他们与其他模式串匹配**，如果某个后缀子串可以匹配模式串的前缀子串，我们就把这个后缀子串称为 “**可匹配的后缀子串**”。

我们从可匹配的后缀子串中找出一个最长的，那就是**最长可匹配后缀子串**。我们将 p 点的失败指针指向最长可匹配后缀子串对应的模式串的前缀子串的尾子结点，也就是下图所示：

![img](https://static001.geekbang.org/resource/image/58/ca/582ec4651948b4cdc1e1b49235e4f8ca.jpg)

计算每个失败结点就很复杂了。**其实我们把树中相同深度的结点放到同一层，那么某个结点的失败的指针就只能出现在它所在结点层的上一层。**所以我们可以逐层来求解每个节点的失败指针。**所以失败指针的构建过程，是按层遍历树的过程。**

我们假设 p 的失败指针指向结点 q，我们看结点 p 的子结点 pc 对应的字符，是否也可以在 q 的子结点找到。如果找到了结点 q 的子结点 qc，对应的字符跟结点 pc 对应的字符相同，则将结点 pc 的失败指针指向 qc。

![img](https://static001.geekbang.org/resource/image/da/1f/da685b7ac5f7dc41b2db6cf5d9a35a1f.jpg)

如果结点 q 的子结点中没有找到与 pc 包含的字符，则令 q=q->fail（fail 表示失败指针），继续上面查找，直到 q 是root 为止，如果还没有找到相同字符的子结点，就让结点 pc 的失败指针指向 root。

![img](https://static001.geekbang.org/resource/image/91/61/91123d8c38a050d32ca730a93c7aa061.jpg)

我将 Trie 树的失败指针的代码贴在这里：

```c#
public void BuildFailurePointer() {
    Queue<AcNode> queue = new Queue<AcNode>();
    root.fail = null;
    queue.Enqueue(queue);
    while (!queue.Count == 0) {
        AcNode p = queue.Dequeue();
        for (int i = 0; i < 26; ++i) {
            AcNode pc = p.children[i];
            if (pc == null) continue;
            if (p == null) {
                pc.fail = root;
            } else {
                AcNode q = p.fail;
                while (q != null) {
                    AcNode qc = q.children[pc.Data - 'a'];
                    if (qc != null) {
                        pc.fail = qc;
                        break;
                    }
                    q = q.fail;
                }
                if (q == null) {
                    pc.fail = root;
                }
            }
            queue.Enqueue(pc);
        }
    }
}
```

通过按层来计算每个结点的子节点的失效指针，通过之前的距离，最后构建出来的 AC 自动机就是下面这个样子：

![img](https://static001.geekbang.org/resource/image/51/3c/5150d176502dda4adfc63e9b2915b23c.jpg)

我们现在来看下，**如何在 AC 自动机上匹配主串？**

我们还是拿之前的例子举例，在匹配过程中，主串从 0 开始，AC 自动机从指针 p=root 开始，假设模式串是 b，主串是 a。

- 如果 p 指向的结点有一个等于 b[i] 的子结点 x，我们就更新 p 指向 x，这个时候我们需要通过失败指针，检测一系列失败指针为结尾的路径是否是模式串。处理完之后，我们将 i+1，继续这两个过程；
- 如果 p 指向的结点没有等于 b[i] 的子结点，那么失败指针就派上用场了，我们让 p=p->fail，然后继续这两个过程。

```c#
/// <summary>
/// 
/// </summary>
/// <param name="text">主串</param>
public void Match(string text) {
    int n = text.Length;
    AcNode p = root;
    for (int i = 0; i < n; i++) {
        int idx = text[i] - 'a';
        while (p.children[idx] == null && p != root) {
            p = p.fail; //失败指针发挥作用的地方
        }
        p = p.children[idx];
        if (p == null) p = root; //没有匹配，从 root 开始重新匹配
        AcNode tmp = p;
        while (tmp != root) {
            if (tmp.IsEndingChar == true) {
                int pos = i - tmp.length + 1;
                Console.WriteLine(" 匹配起始下标 " + pos + "; 长度 " + tmp.length);
            }
            tmp = tmp.fail;
        }
    }
}
```

# 总结

AC 自动机算法是多模式字符匹配算法，是基于 Trie 树的一种改进。AC 自动机算法包括两个操作：1. Trie 树的建立；2. 失败指针的构建。

整个 AC 自动机算法包括两个部分的算法，第一部分是将多个模式串构建成一个 AC 自动机。第二部分在 AC 自动机中匹配主串。第一部分就包括刚刚之前讲的两个操作。

