package main

// https://leetcode.cn/problems/combinations/
// 给定两个整数 n 和 k，返回范围 [1, n] 中所有可能的 k 个数的组合。
func combine(n int, k int) [][]int {
	res := [][]int{}
	path := []int{}

	var backTracking func(a, b, index int)
	backTracking = func(a, b, index int) {
		if len(path) == b {
			// 防止同一个引用问题
			tmp := make([]int, b)
			copy(tmp, path)
			res = append(res, tmp)
			return
		}
		for i := index; i <= a; i++ {
			path = append(path, i)
			backTracking(a, b, i+1)
			path = path[:len(path)-1]
		}
	}
	backTracking(n, k, 1)
	return res
}

func combine2(n int, k int) [][]int {
	res := [][]int{}
	path := []int{}

	var backTracking func(a, b, index int)
	backTracking = func(a, b, index int) {
		if len(path) == b {
			// 防止同一个引用问题
			tmp := make([]int, b)
			copy(tmp, path)
			res = append(res, tmp)
			return
		}
		// 优化搜索范围
		for i := index; i <= a-(b-len(path))+1; i++ {
			path = append(path, i)
			backTracking(a, b, i+1)
			path = path[:len(path)-1]
		}
	}
	backTracking(n, k, 1)
	return res
}
