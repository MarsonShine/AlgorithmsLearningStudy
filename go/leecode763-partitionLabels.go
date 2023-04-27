package main

// https://leetcode.cn/problems/partition-labels/
func partitionLabels(s string) []int {
	result := []int{}
	// 统计每个字母出现的最大位置
	ls := [26]int{}
	left, right := 0, 0
	for i := 0; i < len(s); i++ {
		ls[s[i]-'a'] = i
	}
	for i := 0; i < len(s); i++ {
		right = max(right, ls[s[i]-'a'])
		if i == right {
			result = append(result, right-left+1)
			left = i + 1
		}
	}
	return result
}
