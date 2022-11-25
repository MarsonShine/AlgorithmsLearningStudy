package main

import (
	"math"
	"sort"
)

// https://leetcode.cn/problems/minimum-size-subarray-sum/
// 找出数组中满足小于目标值的最短连续子数组
const MaxUint = ^uint(0)
const MaxInt = int(MaxUint >> 1)

func minSubArrayLen(target int, nums []int) int {
	// 暴力
	sum := 0
	subLen := 0
	result := math.MaxInt
	for i := 0; i < len(nums); i++ {
		sum = 0
		for j := i; j < len(nums); j++ {
			sum += nums[j]
			if sum >= target {
				subLen = j - i + 1
				if result > subLen {
					result = subLen
				}
				break
			}
		}
	}
	if result == math.MaxInt {
		return 0
	}
	return result
}

/*
[2,3,1,2,4,3]  7
2+3+1+2 >= 7 result = 4
3+1+2+4 >= 7 result = 4
1+2+4 >= 7 result = 3
2+4+3 >= 7 result = 3
4+3 >= 7 result = 2
*/

func minSubArrayLen2(target int, nums []int) int {
	// 滑动窗口，也即是双指针控制首尾移动
	sum := 0
	subLen := 0
	result := math.MaxInt
	i := 0
	for j := 0; j < len(nums); j++ {
		sum += nums[j]
		for sum >= target {
			subLen = j - i + 1
			if result > subLen {
				result = subLen
			}
			sum -= nums[i] // 舍去起始位置（缩小窗口，所以要减去对应的值）
			i += 1         // 开始位置往后挪一个位置参与后续计算（即缩小窗口范围）
		}
	}
	if result == math.MaxInt {
		return 0
	}
	return result
}

/*
[2,3,1,2,4,3]  7
2+3+1+2=sum=8 >=  7  subLen=3-0+1=4  sum=8-nums[i]=8-2=6 i+=1 	1
3+1+2+4=sum=10 >= 7 subLen=4-1+1=4  sum=10-nums[i]=10-3=7 i+=1	2
1+2+4=sum=7 >= 7 subLen=4-2+1=3 sum=7-nums[i]=7-1=6 i+=1		3
2+4+3=sum=9 >= 7 subLen=5-3+1=3 sum=9-nums[i]=9-2=7 i+=1		4
4+3=sum=7 >= 7 subLen=5-4+1=2	sum=7-nums[i]=7-4=3 i+=1		5
3=sum ...


*/

// 前缀和+二分法
func minSubArrayLen3(s int, nums []int) int {
	n := len(nums)
	if n == 0 {
		return 0
	}
	ans := math.MaxInt32
	sums := make([]int, n+1)
	for i := 1; i <= n; i++ {
		sums[i] = sums[i-1] + nums[i-1]
	}
	for i := 1; i <= n; i++ {
		target := s + sums[i-1]
		bound := sort.SearchInts(sums, target)
		if bound < 0 {
			bound = -bound - 1
		}
		if bound <= n {
			ans = min(ans, bound-(i-1))
		}
	}
	if ans == math.MaxInt32 {
		return 0
	}
	return ans
}

func min(x, y int) int {
	if x < y {
		return x
	}
	return y
}

/*
[2,3,1,2,4,3]  7
[0,2,5,6,8,12,15]
*/
