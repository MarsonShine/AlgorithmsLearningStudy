package main

import "sort"

// https://leetcode.cn/problems/squares-of-a-sorted-array/
func sortedSquares(nums []int) []int {
	// 计算
	newArray := make([]int, len(nums), len(nums))
	for i, n := range nums {
		newArray[i] = n * n
	}
	// 排序
	sort.Ints(newArray)
	return newArray
}

func sortedSquares2(nums []int) []int {
	var l = len(nums)
	newArray := make([]int, l, l)
	// 找到小于0的最后一个数
	left := 0
	right := l - 1
	k := right
	for left <= right {
		if nums[left]*nums[left] < nums[right]*nums[right] {
			newArray[k] = nums[right] * nums[right]
			right--
		} else {
			newArray[k] = nums[left] * nums[left]
			left++
		}
		k--
	}
	return newArray
}

/*
绝对值
[4,1,0,3,10]

[-4,-1,0,3,10]
[49]
[1,49] min 0
[1,49,0] -> [0,1,49]
*/
