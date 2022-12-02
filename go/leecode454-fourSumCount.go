package main

// https://leetcode.cn/problems/4sum-ii/description/
func fourSumCount(nums1 []int, nums2 []int, nums3 []int, nums4 []int) int {
	m := make(map[int]int, 0)
	for _, a := range nums1 {
		for _, b := range nums2 {
			m[a+b]++
		}
	}
	var count int = 0
	for _, c := range nums3 {
		for _, d := range nums4 {
			if _, ok := m[0-(c+d)]; ok { // 符合条件
				count += m[0-(c+d)]
			}
		}
	}
	return count
}

/*
nums1 + nums2 + nums3 + nums4 = 0
  a   +   b   +   c   +   d = 0
  a + b = - (c + d)
*/
