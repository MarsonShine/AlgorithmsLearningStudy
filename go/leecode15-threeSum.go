package main

import (
	"sort"
)

// 双指针
func threeSum(nums []int) [][]int {
	sort.Ints(nums)
	res := [][]int{}
	for i := 0; i < len(nums)-2; i++ {
		first := nums[i]
		if first > 0 {
			break
		}
		// 去重first
		if i > 0 && first == nums[i-1] {
			continue
		}
		left, right := i+1, len(nums)-1
		if left < right {
			second, third := nums[left], nums[right]
			if first+second+third == 0 {
				res = append(res, []int{first, second, third})
				// 去重second
				for left < right && nums[left] == second {
					left++
				}
				// 去重third
				for left < right && nums[right] == third {
					right--
				}
			} else if first+second+third > 0 {
				right--
			} else {
				left++
			}

		}
	}

	return res
}

/*
a + b + c = 0
a = - (b + c)
*/
