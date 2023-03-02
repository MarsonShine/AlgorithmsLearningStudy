package main

import "sort"

// https://leetcode.cn/problems/reconstruct-itinerary/
// 重新安排行程
func findItinerary(tickets [][]string) []string {
	results := []string{}
	paths := []string{}
	used := make([]int, len(tickets), len(tickets))

	var getItinerary func([][]string, []int) bool
	getItinerary = func(ts [][]string, used []int) bool {
		if len(ts)+1 == len(paths) {
			tmp := make([]string, len(paths))
			copy(tmp, paths)
			results = tmp
			return true
		}
		for i := 0; i < len(ts); i++ {
			// ts[i] 第一个 = paths 最后一个
			if used[i] == 0 && ts[i][0] == paths[len(paths)-1] {
				paths = append(paths, ts[i][1])
				used[i] = 1
				if getItinerary(ts, used) {
					return true
				}
				used[i] = 0
				paths = paths[:len(paths)-1]
			}
		}
		return false
	}
	paths = append(paths, "JFK")
	// 二维矩阵排序
	sort.Slice(tickets, func(i, j int) bool {
		if tickets[i][0] == tickets[j][0] {
			return tickets[i][1] < tickets[j][1]
		}
		return tickets[i][0] < tickets[j][0]
	})
	getItinerary(tickets, used)
	return results
}
