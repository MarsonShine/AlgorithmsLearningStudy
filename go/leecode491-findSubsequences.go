package main

// https://leetcode.cn/problems/non-decreasing-subsequences/
// 递增子序列
func findSubsequences(nums []int) [][]int {
	results := [][]int{}
	paths := []int{}

	var getSubsequences func(ns []int, index int)
	getSubsequences = func(ns []int, index int) {
		n := len(paths)
		if n > 1 {
			tmp := make([]int, len(paths))
			copy(tmp, paths)
			results = append(results, tmp)
		}
		m := map[int]bool{} // 同层次元素去重
		for i := index; i < len(ns); i++ {
			if len(paths) > 0 && ns[i] < paths[len(paths)-1] {
				continue
			}
			// 去重
			if m[ns[i]] {
				continue
			}
			m[ns[i]] = true
			paths = append(paths, ns[i])
			getSubsequences(ns, i+1)
			paths = paths[:len(paths)-1]
		}
	}
	getSubsequences(nums, 0)
	return results
}
