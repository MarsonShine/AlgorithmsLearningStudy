package main

// https://leetcode.cn/problems/reverse-string-ii/
func reverseStr(s string, k int) string {
	length := len(s)
	buf := []byte(s)
	for i := 0; i < length; i += 2 * k {
		if i+k <= length {
			reverse(buf[i:(i + k)])
		} else {
			// 剩下的全部反转
			reverse(buf[i:length])
		}
	}
	return string(buf)
}

func reverse(s []byte) {
	l := len(s)
	if l < 2 {
		return
	}
	for i := 0; i < l/2; i++ {
		s[i], s[l-i-1] = s[l-i-1], s[i]
	}
}
