package main

import "math"

// https://leetcode.cn/problems/wiggle-subsequence/
func wiggleMaxLength(nums []int) int {
	l := len(nums)
	if l < 2 {
		return l
	}
	var up, down int = 1, 1
	for i := 1; i < l; i++ {
		// 波谷到波峰
		if nums[i] > nums[i-1] {
			up = down + 1
		}
		// 波峰到波谷
		if nums[i] < nums[i-1] {
			down = up + 1
		}
	}
	return int(math.Max(float64(up), float64(down)))
}
