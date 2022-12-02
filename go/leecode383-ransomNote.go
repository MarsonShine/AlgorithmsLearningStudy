package main

// https://leetcode.cn/problems/ransom-note/
// ransomNote 和 magazine ，判断 ransomNote 能不能由 magazine 里面的字符构成。
// magazine 中的每个字符只能在 ransomNote 中使用一次。
func canConstruct(ransomNote string, magazine string) bool {
	m := make(map[int]int, 26)
	for _, c := range magazine {
		m[int(c-'a')]++
	}
	for _, c := range ransomNote {
		m[int(c-'a')]--
		if m[int(c-'a')] < 0 {
			return false
		}
	}
	return true
}

func canConstruct2(ransomNote string, magazine string) bool {
	m := make([]int, 26)
	for _, c := range magazine {
		m[c-'a']++
	}
	for _, c := range ransomNote {
		m[c-'a']--
		if m[c-'a'] < 0 {
			return false
		}
	}
	return true
}
