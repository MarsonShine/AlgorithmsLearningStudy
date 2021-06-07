package main

// https://leetcode-cn.com/problems/valid-parentheses/
func isValid(s string) bool {
	// 遍历
	l := len(s)
	if l%2 != 0 {
		return false
	}
	arr := []byte{}
	m := map[byte]byte{
		')': '(',
		'}': '{',
		']': '[',
	}
	for i := 0; i < l; i++ {
		if m[s[i]] > 0 {
			if len(arr) == 0 || arr[len(arr)-1] == m[s[i]] {
				return false
			}
			arr = arr[:len(arr)-1]
		} else {
			arr = append(arr, s[i])
		}
	}
	return len(arr) == 0
}
