package main

import (
	"sort"
)

// https://leetcode.cn/problems/minimum-number-of-arrows-to-burst-balloons
func findMinArrowShots(points [][]int) int {
	sort.Slice(points, func(i, j int) bool {
		return points[i][0] < points[j][0]
	})
	var result int = 1
	for i := 1; i < len(points); i++ {
		xend := points[i-1][1]
		xstart := points[i][0]
		if xend < xstart {
			result++
		} else {
			//重合
			points[i][1] = min(points[i-1][1], points[i][1]) // 更新在重叠的中选择最小的右边界
		}
	}
	return result
}
