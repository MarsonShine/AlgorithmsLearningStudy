package main

func intersection(nums1 []int, nums2 []int) []int {
	len1, len2 := len(nums1), len(nums2)
	m := make(map[int]bool)
	intersects := []int{}
	if len1 >= len2 {
		for _, n := range nums2 {
			m[n] = true
		}
		for _, n := range nums1 {
			if m[n] {
				intersects = append(intersects, n)
				m[n] = false
			}
		}
	} else {
		for _, n := range nums1 {
			m[n] = true
		}
		for _, n := range nums2 {
			if m[n] {
				intersects = append(intersects, n)
				m[n] = false
			}
		}
	}
	return intersects
}

func intersection2(nums1 []int, nums2 []int) []int {
	intersects := []int{}
	m := make(map[int]bool, 0)
	for _, v := range nums1 {
		m[v] = true
	}
	for _, v := range nums2 {
		if m[v] {
			intersects = append(intersects, v)
			m[v] = false
		}
	}
	return intersects
}
