package main

// https://leetcode.cn/problems/combination-sum/
// 组合总和
func combinationSum(candidates []int, target int) [][]int {
	res := [][]int{}
	path := []int{}

	var getCombinationSum func(cs []int, sum int, tar int, index int)
	getCombinationSum = func(cs []int, sum, tar, index int) {
		if sum > tar {
			return
		}
		if sum == tar {
			tmp := make([]int, len(path))
			copy(tmp, path)
			res = append(res, tmp)
			return
		}
		for i := index; i < len(cs); i++ {
			sum += cs[i]
			path = append(path, cs[i])
			getCombinationSum(cs, sum, tar, i)
			sum -= cs[i]
			path = path[:len(path)-1]
		}
	}
	getCombinationSum(candidates, 0, target, 0)
	return res
}
