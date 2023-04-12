package main

import "sort"

// https://leetcode.cn/problems/queue-reconstruction-by-height
func reconstructQueue(people [][]int) [][]int {
	// 排序,身高从大到小排
	sort.Slice(people, func(i, j int) bool {
		if people[i][0] == people[j][0] {
			return people[i][1] < people[j][1] // 身高相同时，按数量从小到大
		}
		return people[i][0] > people[j][0]
	})
	length := len(people)
	for i := 0; i < length; i++ {
		p := people[i]
		// 比 height 大的有 number 人
		// 把 i 后面 number 插入到 i 的前面位置。
		copy(people[p[1]+1:i+1], people[p[1]:i+1])
		people[p[1]] = p
	}
	return people
}

// 用链表在插入方面会更简单清晰
