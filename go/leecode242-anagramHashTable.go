package main

// https://leetcode.cn/problems/valid-anagram/
// 判断给定的两个字符串中出现的字母次数都是相同的
func isAnagram(s string, t string) bool {
	cn := map[rune]int{}
	for _, c := range s {
		cn[c]++
	}
	for _, c := range t {
		cn[c]--
	}
	for _, n := range cn {
		if n != 0 {
			return false
		}
	}
	return true
}

func isAnagram2(s string, t string) bool {
	cn := [26]int{}
	for _, c := range s {
		cn[c-rune('a')]++
	}
	for _, c := range t {
		cn[c-rune('a')]--
	}
	return cn == [26]int{}
}
