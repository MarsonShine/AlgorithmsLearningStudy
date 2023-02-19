package main

// https://leetcode.cn/problems/palindrome-partitioning/
// 分割回文串
func partition(s string) [][]string {
	results := [][]string{}
	paths := []string{}
	var partitionBackTracking func(str string, index int)
	partitionBackTracking = func(str string, index int) {
		if index >= len(str) {
			tmp := make([]string, len(paths))
			copy(tmp, paths)
			results = append(results, tmp)
			return
		}
		for i := index; i < len(str); i++ {
			ss := str[index : i+1]
			if isPalindrome(ss) {
				paths = append(paths, ss)
				partitionBackTracking(str, i+1)
				paths = paths[:len(paths)-1]
			}
		}
	}
	partitionBackTracking(s, 0)
	return results
}

func isPalindrome(s string) bool {
	for i, j := 0, len(s)-1; i < j; i, j = i+1, j-1 {
		if s[i] != s[j] {
			return false
		}
	}
	return true
}
