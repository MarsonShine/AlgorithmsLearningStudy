package main

import (
	"container/heap"
	"sort"
)

func topKFrequent(nums []int, k int) []int {
	m := make(map[int]int, 0)
	for i := 0; i < len(nums); i++ {
		m[nums[i]]++
	}
	res := []int{}
	for key := range m {
		res = append(res, key)
	}
	sort.Slice(res, func(i, j int) bool {
		return m[res[i]] > m[res[j]]
	})
	return res[:k]
}

// 小顶堆
func topKFrequent2(nums []int, k int) []int {
	m := make(map[int]int, 0)
	for i := 0; i < len(nums); i++ {
		m[nums[i]]++
	}
	h := &Heap{}
	heap.Init(h)
	// 堆化
	for key, val := range m {
		heap.Push(h, [2]int{key, val})
		if h.Len() > k {
			heap.Pop(h)
		}
	}
	res := make([]int, k)
	for i := 0; i < k; i++ {
		res[k-i-1] = heap.Pop(h).([2]int)[0]
	}
	return res
}

type Heap [][2]int // 值、频次

func (h Heap) Len() int {
	return len(h)
}

func (h Heap) Swap(i, j int) {
	h[i], h[j] = h[j], h[i]
}

func (h Heap) Less(i, j int) bool {
	return h[i][1] < h[j][1]
}

func (h *Heap) Push(x interface{}) {
	*h = append(*h, x.([2]int))
}

func (h *Heap) Pop() interface{} {
	n := len(*h)
	r := (*h)[n-1]
	*h = (*h)[0 : n-1]
	return r
}
