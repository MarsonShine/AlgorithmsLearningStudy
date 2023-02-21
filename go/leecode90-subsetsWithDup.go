package main

import "sort"

// https://leetcode.cn/problems/subsets-ii/
// 子集，不能重复
func subsetsWithDup(nums []int) [][]int {
	results := [][]int{}
	paths := []int{}
	var getSubsets func(ns []int, index int)
	getSubsets = func(ns []int, index int) {
		tmp := make([]int, len(paths))
		copy(tmp, paths)
		results = append(results, tmp)
		for i := index; i < len(ns); i++ {
			if i > index && ns[i] == ns[i-1] { // 去重逻辑
				continue
			}
			paths = append(paths, ns[i])
			getSubsets(ns, i+1)
			paths = paths[:len(paths)-1]
		}
	}
	sort.Ints(nums)
	getSubsets(nums, 0)
	return results
}
