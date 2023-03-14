package main

import "sort"

// https://leetcode.cn/problems/assign-cookies/
func findContentChildren(g []int, s []int) int {
	sort.Ints(g)
	sort.Ints(s)
	ret := 0
	for i := 0; i < len(g); i++ {
		min := g[i]
		for j := 0; j < len(s); j++ {
			if s[j] >= min {
				ret += 1
				s = s[j+1:]
				break
			}
		}
	}
	return ret
}

// 只需要一层循环
func findContentChildren2(g []int, s []int) int {
	sort.Ints(g)
	sort.Ints(s)
	index := 0
	// 通过遍历饼干尺寸，找到满足的index+1，g自增
	// 这样就只用一个for循环即可
	for i := 0; i < len(s); i++ {
		if index < len(g) && s[i] >= g[index] {
			index += 1
		}
	}
	return index
}
