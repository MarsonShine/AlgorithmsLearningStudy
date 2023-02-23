package main

// https://leetcode.cn/problems/permutations/
// 全排列组合
func permute(nums []int) [][]int {
	results := [][]int{}
	paths := []int{}
	m := map[int]bool{}
	var getPermute func(nums []int, count int)
	getPermute = func(ns []int, count int) {
		if count == len(ns) {
			tmp := make([]int, len(paths))
			copy(tmp, paths)
			results = append(results, tmp)
			return
		}
		for i := 0; i < len(ns); i++ {
			if m[ns[i]] {
				continue
			}
			m[ns[i]] = true
			paths = append(paths, ns[i])
			getPermute(ns, count+1)
			m[ns[i]] = false
			paths = paths[:len(paths)-1]
		}
	}
	getPermute(nums, 0)
	return results
}
