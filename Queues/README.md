# 队列y

我们知道CPU资源是有限的，任务的处理速度与线程个数并不是线性正相关。相反，过多的线程会产生线程上下文切换，导致处理性能下降。

> 注意：线程上下文切换为什么会导致性能显著降低呢？从《CLR via C#》中线程部分就涉及到。首先是线程的开销：1. **线程内核对象**，OS给每个线程分配并初始化这种数据结构。其中包含一组对线程进行描述的属性，还包括线程上下文（thread context）；2. **线程环境块**（TEB），它在用户模式（应用程序能快速访问的内存地址空间）中分配和初始化内存块。TEB还包含“线程本地存储”数据。3. **用户模式栈**，存储方法的局部变量和实参，还包括当前方法返回的时，线程应该从什么地方继续执行。4. **内核模式栈**，当调用系统级别的函数与功能时，就会用用户模式切换到内核模式，这时windows会把它们从线程的用户模式栈复制到内核模式栈。5. DLL线程连接和线程分离；
>
> 上下文切换概念：单CPU计算机一次只能做一件事情，所以windows必须在系统中共享物理CPU。当CPU给一个线程分配一个CPU。那个线程能运行一个“时间片”的长度。时间片到期，windows就会上下文切换到另一个线程
>
> 上下文切换过程：1. 将CPU寄存器和值保存到当前正在运行的***线程内核对象***内部的一个上下文结构中。2. 从现有线程集合中选出一个线程提供调度。如果该线程由另一个进程拥有，windows在执行任何代码或接触任何数据之前，CPU必须切换虚拟地址空间。3. 将线程上下文结构中的值加载到CPU寄存器中。

## 队列的理解

队列就好比是正在排队上车，队列中最先进来的总是第一个上车，排在最后的是最后一个上车的。

这是一种典型的**先进先出（FIFO）**场景。

由前面说道的Stack知道，栈只有入栈和出栈两个操作。队列也是如此，只有两个操作：**入队(Enqueue)**，数据进来放到队列尾部。**出队(Dequeque)**,从队列头取出一个元素。

队列有很多种特性，比如：环形队列，循环队列，阻塞队列，并发队列等

### 顺序队列和链式队列

队列跟栈是一样的，顺序队列（由数组结构构成）；链式队列（用链表构成）

对于栈来说，我们只需要一个【栈顶】指针就可以了。但是队列需要两个指针：head指针，指向头部；tail指针，指向尾部。

入队时，head指针不变，tail指针递增（知道队列空间满为止）；

![](https://static001.geekbang.org/resource/image/5c/cb/5c0ec42eb797e8a7d48c9dbe89dc93cb.jpg)

出队列时，tail指针不变，head指针递增；

![](https://static001.geekbang.org/resource/image/de/0d/dea27f2c505dd8d0b6b86e262d03430d.jpg)

这样我们就会发现当不停的入队列，出队列操作，head和tail都会往后移，当tail移到最右边时，不管该数组队列中还有没有空余空间（tail虽然会移到最右边，但是head会因递增指针导致左边被以为的内存释放）都不能进行入对操作。那么这种情况该如何解决？

有两种解决方法：1. 数组的数据迁移，每次进行出队操作都相当于删除数组下标为0的数据，这样要搬移整个队列中的数据，其时间复杂度为O(n);那么如何优化？

```c#
public bool Enqueue (string value) {
    //队列尾部没有空间
    if (tail == n) {
        //并且占满整个队列空间
        if (head == 0) return false;
        //数据搬移
        for (int i = head; i < tail; ++i) {
            items[i - head] = items[i];
        }
        //重新设置head，tail
        tail -= head;
        head = 0;
     }
     items[tail] = value;
     ++tail;
     return true;
}
```

实际上，我们在出队列的时候可以不用搬移数据。如果此时我们没有多余的空间存储数据，这时才一次性发生一次数据搬移。这样出对时间复杂度依旧为O(1),通过前面分析过的摊还分析法，入栈也是O(1);

### 链式队列

基于链表实现的队列，我们需要两个指针：1.tail指针(Node)；2.head指针；分别指向链表的最后一个结点和第一个结点。入队时：`tail->next = newNode;tail=tail->next`；出对时：`head=head->next`

![](https://static001.geekbang.org/resource/image/c9/93/c916fe2212f8f543ddf539296444d393.jpg)

具体入队，出队代码体现为

```C#
public void Enqueue (string item) {
    //队列空
    if (tail == null) {
        Node newNode = new Node (item, null);
        head = newNode;
        tail = newNode;
    } else {
       	tail.Next = new Node (item, null);
       	tail = tail.Next;
    }
}
public string Dequeue () {
    if (head == null) return null;
    string value = head.Data;
    head = head.Next;
    if (head == null) {
        tail = null;
    }
    return value;
}
```

### 环形队列

前面的数组队列，当tail=n时会发生数据迁移，那么有什么方法可以避免数据搬移呢？

其实就是环形队列，跟环一样，首尾相连

![](https://static001.geekbang.org/resource/image/58/90/58ba37bb4102b87d66dffe7148b0f990.jpg)

我们根据上图可以看到，当我们添加队列数据时，tail递增，当递增到最大n时，继续入队列（假设head在4出，说明还有空间没有存满（因为存在出队列操作）），这是tail不应该递增为8，而是到下标为0的位置。我们还可以继续入队列，tail就指向下标为1的位置。

![](https://static001.geekbang.org/resource/image/71/80/71a41effb54ccea9dd463bde1b6abe80.jpg)

要实现这种数据结构队列，其实满足核心两要素：**1.判断队列是否为空 2.判断队列是否满**

在非循环的数组队列中，队满的条件是：tail==n，对空的条件是：head == tail；

那么循环队列的队满以及对空的判断条件是什么？

我们多写一些边界情况和常规的值就知道其中的规律：

当：n=8，head=4，tail=3；n=8，head=1，tail=0；n=8，head=0，tail=7；

所以就找到了**队满时(tail+1)%n=head；队空时还是tail==head**

其入队，出队代码

```c#
		public bool Enqueue (string item) {
            //判断队列满
            if (IsFull) return false;
            items[tail] = item;
            tail = (head + 1) % n;
            return true;
        }
        //出队
        public string Dequeue () {
            //判断队列是否空的
            if (head == tail) return default (string);
            string ret = items[head];
            head = (tail + 1) % n;
            return ret;
        }
        public bool IsFull => (tail + 1) % n == head;
```

### 阻塞队列与并发队列

阻塞队列：就是在队列的基础上加了锁的概念，跟“生产者-消费者模式”一样。当生产不足消费速度时（体现在，队列为空时）继续从队列取数据会阻塞，因为没有数据可取，知道队列有数据才会返回。如果队列已经满了，继续入队操作，那么则会阻塞，等待队列出队操作，有空闲位置才允许插入数据。在这个基础之上，我们还可以继续向上拓展，当消费者操作过快时，我们可以设置多个生产者来应付一个消费者。这就意味着会有多个线程的生产者同时操作对队列操作，这个时候就会有线程安全上的问题。

那么这就引入了我们如何做线程安全的队列，也叫并发队列。最简单的做法就是在enqueue和dequeue操作直接加lock，但是由于粒度太大，导致并发度很低。而CAS原子性操作则可以非常高效的实现队列。后面会有详细介绍

## 总结

队列最大的特点就是FIFO，主要的两个操作是入队，出队。跟栈一样，可以用数组和链表的形式实现队列。以及一些数据的搬移工作。