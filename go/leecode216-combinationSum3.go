package main

// https://leetcode.cn/problems/combination-sum-iii/
var (
	results = [][]int{}
	paths   = []int{}
)

func combinationSum3(k int, n int) [][]int {
	if n < 6 || n > 24 {
		return results
	}
	results = [][]int{}
	paths = []int{}
	combinationSum3_backTracking(k, n, 1)
	return results
}

func combinationSum3_backTracking(k, n, index int) {
	if len(paths) == k {
		if n == 0 {
			tmp := make([]int, k)
			copy(tmp, paths)
			results = append(results, tmp)
		}
		return
	}
	for i := index; i <= 9; i++ {
		n -= i
		paths = append(paths, i)
		combinationSum3_backTracking(k, n, i+1)
		n += i
		paths = paths[:len(paths)-1]
	}
}

// 循环范围优化：因为for循环是升序遍历的，所以当检测到满足一个要求时，同级别的循环后续循环次数可以直接舍弃
// 比如 1+2+4=7，那么第三个循环4以后的次数都可以舍弃，因为必定都大于7
func combinationSum3_backTracking2(k, n, index int) {
	if n < 0 {
		return
	}
	if len(paths) == k {
		if n == 0 {
			tmp := make([]int, k)
			copy(tmp, paths)
			results = append(results, tmp)
		}
		return
	}
	for i := index; i <= 9; i++ {
		n -= i
		paths = append(paths, i)
		combinationSum3_backTracking(k, n, i+1)
		n += i
		paths = paths[:len(paths)-1]
	}
}
