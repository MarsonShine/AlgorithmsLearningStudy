package main

import "sort"

func fourSum(nums []int, target int) [][]int {
	sort.Ints(nums)
	var res [][]int
	for i := 0; i < len(nums)-3; i++ {
		first := nums[i]
		// nums[i] 去重
		if i > 0 && first == nums[i-1] {
			continue
		}
		for j := i + 1; j < len(nums)-2; j++ {
			second := nums[j]
			// nums[j] 去重
			if j > i+1 && second == nums[j-1] {
				continue
			}
			left := j + 1
			right := len(nums) - 1
			for left < right {
				third := nums[left]
				fourth := nums[right]
				sum := first + second + third + fourth
				if sum == target {
					res = append(res, []int{first, second, third, fourth})
					for left < right && third == nums[left] {
						left++
					}
					for left < right && fourth == nums[right] {
						right--
					}
				} else if sum > target {
					right--
				} else {
					left++
				}
			}
		}
	}
	return res
}
