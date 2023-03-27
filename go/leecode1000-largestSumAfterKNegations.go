package main

import (
	"math"
	"sort"
)

// https://leetcode.cn/problems/maximize-sum-of-array-after-k-negations/
// K 次取反后最大化的数组和
func largestSumAfterKNegations(nums []int, k int) int {
	// 排序
	sort.Ints(nums)
	// 取反
	minIndex := -1
	for i := 0; i < k; i++ {
		if i > len(nums)-1 {
			nums[minIndex] = -nums[minIndex]
			continue
		}
		if i > 0 && nums[i] > 0 {
			if nums[i] < nums[i-1] {
				minIndex = i
			} else {
				if minIndex > -1 {
					nums[minIndex] = -nums[minIndex]
					continue
				} else {
					minIndex = i - 1
					nums[minIndex] = -nums[minIndex]
				}
			}
		} else {
			nums[i] = -nums[i]
			minIndex = i
		}
	}
	// 求和
	sum := 0
	for _, v := range nums {
		sum += v
	}
	return sum
}

func largestSumAfterKNegations2(nums []int, k int) int {
	// 按数组项的绝对值从大到小排序
	sort.Slice(nums, func(i, j int) bool {
		return math.Abs(float64(nums[i])) > math.Abs(float64(nums[j]))
	})
	for i := 0; i < len(nums); i++ {
		if nums[i] < 0 && k > 0 {
			nums[i] = -nums[i]
			k--
		}
	}
	// 奇数只做一次相反数
	if k%2 == 1 {
		nums[len(nums)-1] = -nums[len(nums)-1]
	}
	// 求和
	sum := 0
	for _, v := range nums {
		sum += v
	}
	return sum
}
