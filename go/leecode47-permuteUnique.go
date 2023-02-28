package main

import "sort"

// https://leetcode.cn/problems/permutations-ii/
// 全排列
func permuteUnique(nums []int) [][]int {
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
			if (i != 0 && ns[i-1] == ns[i]) && !m[i-1] { // 同层次（前后）一样则说明重复
				continue
			}
			if !m[i] {
				m[i] = true
				paths = append(paths, ns[i])
				getPermute(ns, count+1)
				m[i] = false
				paths = paths[:len(paths)-1]
			}
		}
	}
	sort.Ints(nums)
	getPermute(nums, 0)
	return results
}

func permuteUnique2(nums []int) [][]int {
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
		used := make(map[int]bool, len(ns))
		for i := 0; i < len(ns); i++ {
			// 去重
			if used[ns[i]] {
				continue
			}
			if !m[i] {
				used[ns[i]] = true
				m[i] = true
				paths = append(paths, ns[i])
				getPermute(ns, count+1)
				m[i] = false
				paths = paths[:len(paths)-1]
			}
		}
	}
	sort.Ints(nums)
	getPermute(nums, 0)
	return results
}
