# 摩尔投票法

摩尔投票法完整的名称是 “Boyer-Moore majority vote algorithm”；这是由 Boyer 和 Moore 两位于 1981 发表的算法。此算法是利用线性时间和常数空间**寻找大多数元素序列**的算法。

在最简单的形式中，如果有一个元素，算法会找到一个多数元素（即一个元素在输入元素的一半以上重复出现就叫多数元素）。第二次遍历数据的算法版本可用于验证第一次遍历中找到的元素是否真的是多数元素。

完整的摩尔投票法详见 wiki [Boyer-Moore majority vote algorithm](https://en.wikipedia.org/wiki/Boyer–Moore_majority_vote_algorithm)。

这里实际上有一个假设需要成立：**那就是多数元素一定存在，因为这个元素重复的个数超过总数的一半**。那么每次从这个数组中俩俩比较，如果不相同则把元素删除。那么剩下没法删除的肯定就是多数元素。

其实在[知乎](https://www.zhihu.com/question/49973163)就有答主对此进行了浅显易懂的解释：“**抵消**：玩一个诸侯争霸的游戏，假设你方人口超过总人口一半以上，并且能保证每个人口出去干仗都能一对一同归于尽。最后还有人活下来的国家就是胜利。”

那么根据解释再回到摩尔投票法的解释上看，给定一个数组，要找出其中的多数元素。首先**假设第一个数就是多数元素**，那么只需要从第一个开始循环比较，如果相等那么计数器就+1，如果不同，这个时候就得判断此时计数器是否大于0，**大于 0 那我就可以抵消这一个**。否则就把这个数替换成多数元素。那么最后剩下的数的计数器是大于 0 的，那么这个数就是多数元素。

起初，我压根不知道这个算法。在一次尝试解决[leecode-求众数](https://leetcode-cn.com/problems/majority-element/)中的解析才知道可以通过摩尔投票法巧妙的解决这个问题。

```go
// 哈希
func majorityElement(nums []int) int {
    countmap := map[int]int{}
		count := len(nums) / 2
    for i := 0; i < len(nums); i++ {
        n := nums[i]
        countmap[n] += 1
        if countmap[n] > count {
          return n
        }
		}
		return 0
}
```

这里用到哈希，所以我们额外用到了 O(n) 空间复杂度。而如果换摩尔投票法的话，直接俩俩比较即可。

```go
func majorityElement(nums []int) int {
    many := nums[0]
    r := 1
    for i := 1; i < len(nums); i++ {
      if nums[i] == many {
        r++
      } else {
        r--
        if r == 0 {
          	many = nums[i]
            r = 1
        }
      }
    }
		return many
}
```

当然我们还可以用排序的方式，更加直接了当的通过索引得出结果

```go
func majorityElement(nums []int) int {
    sort.Ints(nums)
		return nums[len(nums) / 2]
}
```

这个就不过多解释了。