package main

import "sort"

// https://leetcode.cn/problems/non-overlapping-intervals/
func eraseOverlapIntervals(intervals [][]int) int {
	// 按左边界排序，算出想叫最多的个数
	sort.Slice(intervals, func(i, j int) bool {
		return intervals[i][0] < intervals[j][0]
	})
	result := 0 // 相交的个数
	for i := 1; i < len(intervals); i++ {
		if intervals[i-1][1] > intervals[i][0] {
			// 相交的两部分，用最小的右边界与后续节点比较是否相交
			intervals[i][1] = min(intervals[i-1][1], intervals[i][1])
			result++
		}
	}
	return result
}

// 按右边界排序，算出不相交的个数
func eraseOverlapIntervals2(intervals [][]int) int {
	sort.Slice(intervals, func(i, j int) bool {
		return intervals[i][1] < intervals[j][1]
	})
	result := 0 // 非重叠的个数
	end := intervals[0][1]
	for i := 1; i < len(intervals); i++ {
		if end <= intervals[i][0] { // 不相交
			result++
			end = intervals[i][0]
		}
	}
	return len(intervals) - result
}
