package main

import "math"

// https://leetcode.cn/problems/maximum-subarray/
// 暴力
func maxSubArray(nums []int) int {
	ret := math.MinInt
	sum := 0
	for i := 0; i < len(nums); i++ {
		sum = 0
		for j := i; j < len(nums); j++ {
			sum += nums[j]
			if sum > ret {
				ret = sum
			}
		}
	}
	return ret
}

// 贪心
func maxSubArray2(nums []int) int {
	ret := math.MinInt
	sum := 0
	for i := 0; i < len(nums); i++ {
		sum += nums[i]
		if sum > ret {
			ret = sum
		}
		// 和为负数，则将下一个作为计数起点
		if sum < 0 {
			sum = 0
		}
	}
	return ret
}
