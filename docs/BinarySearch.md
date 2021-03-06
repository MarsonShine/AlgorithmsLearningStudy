

## 二分查找（Binary Search）

二分查找是对有序数据集合的查询算法，又叫 “折半查找算法”。二分查找的思想特别简单，就是把原始区间一分为二，然后判断这个要查找的数在哪个区间，然后继续折半查找，以此类推到查到这个数据。

这里有一个问题需要思考：一个有1000万个整数数据，每个数据占8个字节，如何设计数据结构和算法，快速判断某个整数是否出现在这组数据中。我们希望这个功能不能太占内存，最多不要超过100M，那么如何做呢？

### 生活中的二分思想

二分查找在我们生活是到处可见的。比如我在KTV常玩的猜数字游戏，裁判在给一定范围内的一组数字1到100，我们每人说一个数，没有中裁判说的那个数字，比如55。如果裁判提示我们说的这个字小于55，那么我们就继续从往小的区间继续说出一个数字，直到那个说出55的人，就接受惩罚。

从上面说出一个数字那里，我们假设我们每次喊的数据都是从这个区间的数据一半的那个数喊起，直到说出55。我们把上述过程描述成下面的样子

```
第一次：
【1，100】 中间数是50	50对比55  <
第二次
【51，100】中间数是75  75对比55  >
第三次
【51，74】 中间数是62  62对比55  >
第四次
【51，61】 中间数是56	56对比55	>
第五次
【51，55】 中间数是53	52对比55	<
第六次
【54，55】	中间数是54	54对比55	<
第七次
【55，55】	
```

只需要7次就能知道，时间是不是很快。这都是二分查找的思想。按照这个思想，就算时【0，999】一组数据，我们也只需要猜10次就能找到。

让我们回到实际应用开发当中，我们有1000个订单，已经按照订单金额的大小排序好了，我们现在要从这些订单中是否有金额19的订单。

按照我们平常的思维，直接遍历这1000个订单，找到金额19即停止循环。这样做没有问题，但还是比较慢的，最差情况查找要1000次。那么是否用二分法查找会更快呢？

为了更好的说明情况，我们假设只有10个订单，金额分别为8，10，19，23，24，39，42，55，60，88。

利用二分法思想，每次都与区间的中点对比大小，为此来缩小查找范围。为了能直观体检，我用上述过程在分析一遍

```
第一次：
【8，10，19，23，24，39，42，55，60，88】 
其中最小的 min=8，最大 max=88，中间数 mid=24，待比较数 val=19。
中间数与最小数（ max也行）比较 val < mid
第二次
【8，10，19，23】
其中最小的 min=8，最大 max=23，中间数 mid=10，待比较数 val=19
中间数与最小数比较 val > mid
第三次
【19，23】
其中 min=19，max=23，mid=19，val=19
val = min
最终得出结果
```

我们每一次比较之后，范围都会缩小至一半，直到范围缩小到0。

### 时间复杂度

二分法是一个非常高效的查找算法。高效到什么程度呢？我们来分析一下：

我们假设数据规模为 n，每次查找比较之后范围都会缩小一半，也就是除以2。最坏情况下，直到被缩小范围为空的时候才停止。

```
被查找区间变化
n，n/2，n/4，n/8,...,n/2^k,...
```

可以看出这是个等比数列。其中 n/2^k = 1，我们可以求得 k=log2n，所以时间复杂度就是O(logn)。除此之外，还有堆，二叉树操作算法复杂度都是O(logn)。这种**对数时间复杂度**效率是非常高的，有的时候甚至比 O(1) 的算法都要高效。

因为对数，logn中你的数据规模越大即 n 越大，对应的logn就越小。比如2的32次方，约等于42亿，足够大了吧，也就是我们在42亿数据中查找某个值最多只需要查找32次即可。

我们前面有讲过，大O表示法常常会忽略系数，低阶，常数。对于常数级时间复杂度来说，O(1) 又可能表示一个非常大的一个常量值，比如O(100000)。所以常量级的 O(1) 时间复杂度的算法还没有 O(logn) 时间复杂度高效。

### 二分法查找的递归与非递归实现

简单的二分法查找实现方法并不难，注意我们这里说的 “简单” 二次，因为还有很复杂的二分法的变体问题，这里我们只讨论简单二分法查找。

最简单的情况就是没有重复的数据，我们用二分法查找给定值，我们用C#代码实现

```C#
public int BinarySearch (int[] array, int targetValue) {
    int low = 0;
    int high = array.Length - 1;
    while (low <= high) {
        int mid = (low + high) / 2;
        if (array[mid] == targetValue) {
            return mid;
        } else if (array[mid] < targetValue) {
            low = mid + 1;
        } else {
            high = mid - 1;
        }
    }
    return -1;
}
```

这里其实有一点可以优化的地方，那就是 `int mid = (low + high) / 2` 这里有潜在的bug ，如果这个数据有非常大，那么`low + high` 就极有可能出错（溢出），改进方法是 `int mid = low + (high - low) / 2` 。当然，我们如果要追求极致性能，除法运算也可以改成位运算，即 `int mid = low + (high - low) >> 1` 。

下面是递归版本的实现

```c#
public int SearchRecursively (int[] array, int targetValue) =>
            SearchRecursivelyInternal (array, 0, array[array.Length - 1], targetValue);

private int SearchRecursivelyInternal (int[] array, int low, int high, int targetValue) {
    if (low > high) return -1;
    int mid = low + ((high - low) >> 1); //括号不能少，位运算符的优先级与 +，- 一样
    if (array[mid] == targetValue) {
        return mid;
    } else if (array[mid] < targetValue) {
        low = mid + 1;
        return SearchRecursivelyInternal (array, low, high, targetValue);
    } else {
        high = mid - 1;
        return SearchRecursivelyInternal (array, low, high, targetValue);
    }
}
```

### 二分法的局限性

1. 首先数据结构必须依赖于顺序表结构，直白一点就是数组结构。
2. 二分法排序只能用于已经排过序的数据。
3. 最后，数据太大也不适用于二分法。这个就根之前说的，数据规模越大，时间复杂度越小有点背道而驰，这里其实指得是内存方面，因为二分法实际上用到的数据结构是数组，假设这里有 1G 大小的数组要查询的话，那么二分法查询就要先申请 1G 的连续内存。

### 最后

关于开篇提到的问题，如何用二分法解决呢？

我们的内存限制是 100M，每个数据大小是 8个字节。最简单的就是把这些数据全部加载到数组中，这样内存消耗就是 8 * 1000 * 1000 * 10 约等于 80M ，是符合内存限制要求的，从小到大排序，然后进行二分法排序。其实用后面会讲到的散列表和二叉树都是可以的，但是这两个算法都需要额外的内存空间，是一种拿 “空间换时间” 的算法。

## 拓展——二分法的变形问题

前面讲的二分法是很简单的二分法算法，是在数据有一定限制条件下，并且数组数据一定不含重复元素的算法。

那么当我们在一组含有重复元素的集合中如何正确使用二分法呢，这就是我们的二分法的变形问题。

二分法的变形问题有很多，但主要遇到的有四种

1. 查找第一个值等于给定值的元素
2. 查找最后一个值等于给定值的元素
3. 查找第一个大于等于给定值的元素
4. 查找最后一个小于等于给定值的元素

这里面算法都有一个前提：集合中的元素已经经过排序了

### 变形1：查找第一个值等于给定值的元素

假设我们有这样一组数据：a[10] = [1,3,4,5,6,8,8,8,11,18]。我们用上面分析的二分法来查找 8 这个值。

首先拿 8 与区间的中间值 a[4] 进行比较，8 比 6 大，于是在下标 5 到 9 的区间继续查找。下标 5 和 9 的中间位置是下标 7，a[7] = 8 ，至此结束？但是下标为 7 的元素它并不是第一个等于给定值的元素，所以还要继续往下走，当找到这个等于给定值的元素时，还要判断这个值是否为第一个值，怎么判断？很简单，我们已经得到下标为 7 的元素，那么只要前一个值不等于给定值 8 就意味着下标 7 的就是第一个，否则继续往前判断。用代码描述：

```c#
public int BinarySearch(int[] array,int targetValue){
    int low = 0;
    int high = array.Length - 1;
    while(low <= high){
        int mid = low + ((high - low) >> 1);
        if(array[mid] > targetValue)
            high = mid - 1;
        else if(array[mid] < targetValue)
            low = mid + 1;
        else{
            //查找到相等的元素之后要判断是否为第一个
            if((mid == 0) || array[mid - 1] != targetValue)
                return mid;
            else
                high = mid - 1;
        }         
    }
    return -1;
}
```

上面的写法经过一些处理就是下面网上的二分法版本样子

```c#
public int BinarySearchBeautiful(int[] array,int targetValue){
	int low = 0;
    int high = array.Length -1;
    while(low <= high){
        int mid = low + ((high - low) >> 1);
        if(array[mid] >= value){
            high = mid - 1;
        }else{
            low = mid + 1;
        }
    }
    if(array[low] == value) return low;
    else return -1;
}
```

### 变形2：查找最后一个值等于给定值的元素

这其实就是把判断条件换了一下：我们根据一般的二分法得出一个相等值，那么我们紧接着就要判断这个下标的值是否是最后一个，所以我们就得判断这个下标值 7 的后一位的值是否等于给定值，如果不等则下标 7 就是最后一位，如相等，我们就更新 low = mid + 1，因为要找的元素肯定在 [mid + 1, high] 之间。

```c#
public int BinarySearch(int[] array, int targetValue){
    int low = 0;
    int high = array.Length - 1;
    while(low <= high){
        int mid = low + ((high - low) >> 1);
        if(array[mid] > targetValue)
            high = mid - 1;
        else if(array[mid] < targetValue)
            low = mid + 1;
        else{
            //查找到相等的元素之后要判断是否为第一个
            if((mid == array.Length - 1) || array[mid + 1] != targetValue)
                return mid;
            else
                low = mid + 1;
        }         
    }
    return -1;
}
```

### 变体3：查找第一个值大于等于给定值的元素

比如有这样一组数组：3，4，6，7，10。如果查找第一个大于等于 5 的元素，那就是 6。

实际上，实现的思路跟上面类似：

```c#
public int BinarySearch(int[] array, int targetValue){
    int low = 0;
    int high = array.Length - 1;
    while(low <= high){
        int mid = low + ((high - low) >> 1);
        if(array[mid] >= targetValue){
            if(mid == 0 || array[mid - 1] < targetValue) return mid;
            else high = mid - 1;
        }else{
            low = mid + 1;
        }
    }
    return -1;
}
```

### 变体4：查找最后一个值小于等于给定值的元素

给定数组：3，5，6，8，9，10，最后一个小于等于 7 的就是 6。

```c#
public int BinarySearch(int[] array, int targetValue){
    int low = 0;
    int high = array.Length - 1;
    while(low <= high){
        int mid = low + ((high - low) >> 1);
        if(array[mid] <= targetValue){
            if(mid == array.Length - 1 || array[mid + 1] > targetValue){
                return mid;
            }else{
                low = mid + 1;
            }
        }else{
            high = mid - 1;
        }
    }
}
```

### 如何从12万 IP 数组集合中查找指定 IP 的归属地

首先我们直到 IP 地址每个区间对应不同的归属地，**那么我们会要给 12W IP 排序，然后将这 12W IP 地址转换成 32 位的整数**。所以我们将起始地址，按照对应的整数值的大小关系，从小到大排序。然后这就归纳到 “从数组中查找最后一个值小于等于给定值的元素了。” 

变体的二分查找算法写起来很复杂，稍微一个小细节没有处理就会有 bug。这些容易出错的细节主要包括以下几点：**终止条件、区间上下界更新方法、返回值选择**。