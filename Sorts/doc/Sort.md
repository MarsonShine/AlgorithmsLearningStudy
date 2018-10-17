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

