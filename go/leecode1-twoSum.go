package main

func twoSum(nums []int, target int) []int {
	m := make(map[int]int, 0)
	for i, n := range nums {
		other := target - n
		var j int
		var ok bool
		if j, ok = m[other]; !ok {
			m[n] = i
			continue
		}
		return []int{i, j}
	}
	return []int{}
}
