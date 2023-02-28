package main

import (
	"fmt"
	"sort"
)

// https://leetcode.cn/problems/combination-sum-ii/
func combinationSum2(candidates []int, target int) [][]int {
	results := [][]int{}
	paths := []int{}

	var combinationSum2BackTracking func(cs []int, sum, t, index int)
	combinationSum2BackTracking = func(cs []int, sum, t, index int) {
		if sum > t {
			return
		}
		if sum == t {
			tmp := make([]int, len(paths))
			copy(tmp, paths)
			results = append(results, tmp)
			return
		}
		for i := index; i < len(cs); i++ {
			if i > index && cs[i] == cs[i-1] { // 去重逻辑
				continue
			}
			sum += cs[i]
			paths = append(paths, cs[i])
			combinationSum2BackTracking(cs, sum, t, i+1)
			paths = paths[:len(paths)-1]
			sum -= cs[i]
		}
	}
	sort.Ints(candidates)
	combinationSum2BackTracking(candidates, 0, target, 0)
	return results
}

// 超时
func combinationSum4(candidates []int, target int) [][]int {
	results := [][]int{}
	paths := []int{}
	m := make(map[string]bool)
	var combinationSum2BackTracking func(cs []int, sum, t, index int)
	combinationSum2BackTracking = func(cs []int, sum, t, index int) {
		if sum > t {
			return
		}
		if sum == t {
			if ok, _ := m[fmt.Sprintf("%v", paths)]; ok {
				return
			}
			tmp := make([]int, len(paths))
			copy(tmp, paths)
			results = append(results, tmp)
			return
		}
		for i := index; i < len(cs); i++ {
			sum += cs[i]
			paths = append(paths, cs[i])
			combinationSum2BackTracking(cs, sum, t, i+1)
			paths = paths[:len(paths)-1]
			sum -= cs[i]
		}
	}
	sort.Ints(candidates)
	combinationSum2BackTracking(candidates, 0, target, 0)
	return results
}

// hash 去重
func combinationSum5(candidates []int, target int) [][]int {
	results := [][]int{}
	paths := []int{}
	var combinationSum2BackTracking func(cs []int, sum, t, index int)
	combinationSum2BackTracking = func(cs []int, sum, t, index int) {
		if sum > t {
			return
		}
		if sum == t {
			tmp := make([]int, len(paths))
			copy(tmp, paths)
			results = append(results, tmp)
			return
		}
		m := make(map[int]bool, len(cs))
		for i := index; i < len(cs); i++ {
			if m[cs[i]] {
				continue
			}
			m[cs[i]] = true
			sum += cs[i]
			paths = append(paths, cs[i])
			combinationSum2BackTracking(cs, sum, t, i+1)
			paths = paths[:len(paths)-1]
			sum -= cs[i]
		}
	}
	sort.Ints(candidates)
	combinationSum2BackTracking(candidates, 0, target, 0)
	return results
}
