## 归并排序(Merge Sort)

归并排序的核心思想就是 “**分治思想**” 。如果要排序一个数组，我们先把数组一分为二，然后对前后两个部分分别进行排序，再将排序好的部分合并成一个数组，这样整个数组就是有序了。

![](https://static001.geekbang.org/resource/image/db/2b/db7f892d3355ef74da9cd64aa926dc2b.jpg)

从图片描述的过程来看，这根递归思想很像。其实归并排序一半都是用递归实现的。**分治思想是一种解决问题的思想，递归是解决问题的手段**。

那么如何用递归代码来实现归并排序呢？

首先要推算出递推公式，我们可以上图中演算出

```
merge_sort(p...r) = merge_sort(merge_sor(p...q),merge_sort(q-1...r))
p >= r 终止循环，不在继续分解
```

上面的递推公式是说有 p到r的数组 在排序的时候可以 划分为两个数组，分别是 p到q，q+1到r。其中q是等于是p，r的中点位置。当下表p,q以及q+1,r两个数组排序好在合并在一起，组成了merge_sort(p...r)

我们根据递推公式来的出具体代码实现也就不难了：

```C#
public void MergeSort(int[] array, int start, int end){
    if(start >= end) return;
    //中间节点
    int mid = (start + end) / 2;
    MergeSort(array, start, mid);
    MergeSort(array, mid + 1, end);
    MergeSortSub(array, start, mid, end);
}
```

```C#
//这个方法主要执行比较与交换
public void MergeSortSub(int[] array, int start, int mid, int end){
    int[] temp = new int[end - start + 1];//临时存放比较排序之后的值
    int m = start, n = mid + 1, k = 0;
    while(n <= end && m <= mid){
        if(array[m] <= array[n])
            temp[k++] = array[m++];
        else
            temp[k++] = array[n++];
    }
    
    int nstart = m;
    int nend = mid;
    if(n <= end){
        nstart = n;
        nend = end;
    }
    while(nstart <= nend){
        temp[k++] = array[nstart++];
    }
    //将temp值拷贝回array
    for(int i = 0; i < end - start; i++){
        array[start + i] = temp[i];
    }
    
}
```

### 性能分析

1. 归并排序是否是稳定排序算法？

归并排序算法可以用递归实现的，那么由上面的排序过程分解图以及代码。那我们假设我们有 [p,r] 数组，首先分解成 [p,q]，[q+1,r]，这两个子数组里面有两个相同的元素，那么我们如何保证这两个元素在排序合并之后的数组元素顺序不变呢？正如上面的代码一样，我们只要先把 [p,q] 里面排序放到临时数组变量，这样在合并的时候就能保持顺序不变。因此归并排序算法是稳定排序算法。

2. 归并排序的时间复杂度是多少？

根据递归思想，分解成两个子数组并在排序之后的合并。时间主要分布在两个方面：数组分解成子数组并排序以及合并数组。假设数据规模为n的数组，我们有

```c#
T(1) = C;
T(c) = T(a) + T(b) + n;	c > 1 		//n为两子数组合并的时间
```

我们假设 n规模的数组消耗的时间为T(n)，那么带入上述公式得

```c#
T(n) = T(n/2) + T(n/2) + n;
	 = 2 * T(n/2) + n;
```

这么看还不直观，那么T(n/2)的时间算出来又是多少呢？我们继续递归知道

```c#
T(n) = T(n/2) + T(n/2) + n;
	 = 2 * (T(n/4) + T(n/4) + n/2) + n => 2 * 2(T(n/4)) + 2n => 4 * T(n/4) + 2 * n;
	 = 2 * 2*(T(n/8) + T(n/8) + n/4) + 2n => 2 * 2 * 2 * T(n/8) + 3n => 8 * T(n/8) + 3 * n;
	 ...
	 = 2 ^ k * (n/(2^k)) + k*n;
```

有根据对数公式可知

```
T(n) = 2^k * log2n + kn;
	 = Klog2N;
时间复杂度为：O(nlogn);
```

从代码和过程分析图可知，归并排序在排序过程无关是否已经是满排序还是蛮逆排序，都会分解并比较，所以最好，最坏，平均时间复杂都为O(nlogn)；

3. 归并排序的空间复杂度是多少？

归并排序算法任何情况下都是O(nlogn)，那么快速排序的应用程度要比归并排序广泛呢？哪怕是快速排序，在最坏情况下时间复杂度是O(n^2)；原因是因为归并算法不是原地排序算法。我们从代码可以看出，每次分解过程中，都要借助临时变量来存储分解后的数组，直到合并结束，那么按照前分析时间复杂度一样，控件复杂度也是O(nlogn)呢？其实这里面有一个知识点很重要，虽然在分解排序过程，会分配这么多临时变量，但是在合并期间，那些临时变量都会垃圾回收（释放），因为CPU同一时刻只会有一个函数会被调用，所以在合并时，之前那些变量都没用了，所以被释放了，也就只有合并函数里面的n个临时数组的空间，即空间复杂度为O(n)；

## 快速排序(Quick Sort)

快速排序同归并排序一样是用的“分治思想”，但排序过程与递归完全不同。

快速排序的排序过程是：如果我们要从 [p,q] 数组排序，我们会从这里面选取一个相对适合的任意元素作为pivot（分区点）。

我们遍历去q到r的数据，将比pivot小的数据放左边，大的放右边。经过这一步，原数组就被分为三个部分——左边小于pivot的部分，右边大于pivot的部分，以及pivot。

![](https://static001.geekbang.org/resource/image/4d/81/4d892c3a2e08a17f16097d07ea088a81.jpg)

根据分治，递归处理思想，我们可以用递归排序下标从p到q-1到r之间的数据，直到区间缩小为1，就说明所有数据都排序完了。

用递推公式就是

```
quick_sort(p...r) = quick_sort(p...q-1) + quick_sort(q+1...r)	p>=r
```

我们讲递推公式用伪代码表示

```c
//n为数组的大小
quick_sort(array,n){
  quick_sort_recurise(array,0,n-1);
}
quick_sort_recurise(array,p,r){
  if(p>=r) return;
  q = partition(array,p,r);	//获取分区点pivot
  quick_sort_recurise(array,p,q-1);
  quick_sort_recurise(array,q+1,r);
}
```

这里partition函数其实就是之前提到的随机选取array中的一个元素作为pivot（一般情况下，选取array的最后一个数），然后对array分区，返回pivot在数组中的下标。

如果我们不考虑空间消耗的话，partition函数的思路很简单。我们临时申请两个数组啊a,b，遍历array[p,r]，讲小于pivot的赋值给a，大于pivot给b，最后将a和b数组的数据顺序拷贝到array[p...r]；