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

如果我们不考虑空间消耗的话，partition函数的思路很简单。我们临时申请两个数组啊a,b，遍历array[p,r]，讲小于pivot的赋值给a，大于pivot给b，最后将a和b数组的数据顺序拷贝到array[p...r]。

但是这样实现的话，申请的内存空间太大了，不是原地排序算法。并且我们希望快速排序是原地算法排序的，所以空间复杂度要为O(1)，那么partition函数就不能太占空间，这时我们就需要在array[p...r]的原地完成区分操作。

原地分区函数的实现思路很巧妙，先来一段伪代码

```C
partition(array,p,r){
    pivot = array[r];
    i = p;
    for j = p to r-1 do{
        if array[j] < pivot{
            swap array[i] with array[j]
            i = i+1
        }
    }
    swap array[i] with array[r]
    return i;
}
```

这个处理有点类似选择排序。我们通过游标 i 把数组 array 分为[p...r-1] 两部分。[p...i-1] 是小于 pivot 区间的，我们暂称为 “已排序区间” ，array[i...r-1] 是 “未排序区间”。我们每次都从未排序区间 [i...r-1] 中选取一个元素 array[j]   与pivot比较，如果小于pivot，则加到以排序区间的尾部，就是array[i]的位置。

数组的插入操作，就是在数组某个位置插入，并且其后的元素都往后挪一位，但这样非常耗时。我们可以用交换的处理方式，时间复杂度是O(1)就能完成插入操作。在这里我们只需要将array[j]与array[i]值互换，就可以在O(1)的时间复杂度内将array[j]的值放到下标i的位置。下图就能很好说明

![](https://static001.geekbang.org/resource/image/08/e7/086002d67995e4769473b3f50dd96de7.jpg)

因为分区过程中涉及交换，如果当数组里面有两个8时，恰巧其中一个被当作pivot，那么另外一个在分区处理后，可能顺序发生变化，所以快速排序不是一个稳定排序算法。

讲到这里我们发现，貌似这跟归并排序算法很像，其实归并排序的处理过程是自下而上的，先分解为子问题，然后合并。而快速排序相反，它是自顶向下的，先分区，然后在处理字问题。我们前面提到过，归并排序在任何情况下都是O(nlogn)的，但是它不是原地排序的，因此每次排序都会占用很多内存，而快速排序却可以通过原地分区函数，可以实现原地排序，从而节省了很多内存。

我们先把上面的伪代码翻译成C#代码在分析快排的性能。

```c#
public void QuickSort(int[] array) => SortInternal(array, 0, array.Length - 1);

public void SortInternal(int[] array, int p, int r){
    if(p >= r) return;
    //分区点
    int pivot = Portititon(array, p, r);
    SortInternal(array, p, pivot - 1);
    SortInternal(array, pivot + 1, r);
}

public int Portition(int[] array, int p, int r){
    int pivot = array[r];
    int i = p;
    for(int j = p; j < r; j++){
        if(array[j] < pivot){
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
            i++;
        }
    }
    int tmp = array[i];
    array[i] = array[r];
    array[r] = tmp;
    return i;
}
```

### 快速排序的性能分析

因为之前已经讨论过了快排的稳定性和是否原地排序，所以我们着重理一下快排的时间复杂度是多少

我们之前分析过递归的时间复杂度，我们假设每次区分操作，都能正好把数据分成大小接近的两个小区间，所以快拍的时间复杂度根递归是一样O(nlogn)；

```
T(1) = C; 
T(n) = 2 * T(n/2) + n; n > 1;
```

刚好分成这种事很少的，那么当选择1，3，5，6，8选择最后一个数作为pivot，每次分区得到的两个区间都是不平等的。我们需要大量的n次分区操作，才能完成整个排序过程。每个分区我们平均要扫描O(n/2)，这种情况，我们的时间复杂度退回到0(n^2)；快排的平均复杂度为d多少呢？

我们假设每次分区操作会将数组分成 9:1两个小区间。我们继续用递推公式

```
T(1) = C			//n=1,只需要常量级别的执行时间
T(n) = T(n/10) + T(9*n/10) + n; n > 1;
```

