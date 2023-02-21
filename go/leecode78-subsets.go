package main

// https://leetcode.cn/problems/subsets/
// 子集
func subsets(nums []int) [][]int {
	results := [][]int{}
	paths := []int{}
	var getSubsets func(ns []int, index int)
	getSubsets = func(ns []int, index int) {
		tmp := make([]int, len(paths))
		copy(tmp, paths)
		results = append(results, tmp)
		for i := index; i < len(ns); i++ {
			paths = append(paths, ns[i])
			getSubsets(ns, i+1)
			paths = paths[:len(paths)-1]
		}
	}
	getSubsets(nums, 0)
	return results
}
