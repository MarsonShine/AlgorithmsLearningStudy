package main

import (
	"container/heap"
	"sort"
)

var a []int

type hp struct {
	sort.IntSlice
}

func (h hp) Less(i, j int) bool {
	return a[h.IntSlice[i]] > a[h.IntSlice[j]]
}
func (h *hp) Push(v interface{}) {
	h.IntSlice = append(h.IntSlice, v.(int))
}
func (h *hp) Pop() interface{} {
	a := h.IntSlice
	v := a[len(a)-1]
	h.IntSlice = a[:len(a)-1]
	return v
}

func maxSlidingWindow3(nums []int, k int) []int {
	a = nums
	queue := &hp{make(sort.IntSlice, k)}
	for i := 0; i < k; i++ {
		queue.IntSlice[i] = i
	}
	heap.Init(queue)

	n := len(nums)
	ans := make([]int, 1, n-k+1)
	ans[0] = nums[queue.IntSlice[0]]
	for i := k; i < n; i++ {
		// 滑动窗口插入数据，意味着最左边的就会消失在窗口内
		heap.Push(queue, i)
		for queue.IntSlice[0] <= i-k {
			heap.Pop(queue)
		}
		ans = append(ans, nums[queue.IntSlice[0]])
	}
	return ans
}

func maxSlidingWindow(nums []int, k int) []int {
	maxNums := []int{}
	if k == 1 {
		return nums
	}
	count := len(nums) - k + 1
	for i := 0; i < count; i++ {
		var max int = nums[i]

		for j := i; j < k+i; j++ {
			if nums[j] > max {
				max = nums[j]
			}
		}
		maxNums = append(maxNums, max)
	}
	return maxNums
}

func maxSlidingWindow2(nums []int, k int) []int {
	maxNums := []int{}
	if k == 1 {
		return nums
	}
	count := len(nums) - k + 1
	var maxIdx int = 0
	var max int = nums[0]
	for i := 0; i < count; i++ {
		maxIdx--
		if maxIdx >= 0 {
			if max > nums[k-1+i] {
				maxIdx--
			} else {
				max = nums[k-1+i]
				maxIdx = k - 1
			}
			maxNums = append(maxNums, max)
			continue
		}
		l := 0
		max = nums[i]
		// 这里需要优化，可以采用排序算法
		for j := i; j < k+i; j++ {
			if nums[j] > max {
				max = nums[j]
				maxIdx = l
			}
			l++
		}
		maxNums = append(maxNums, max)
	}
	return maxNums
}
