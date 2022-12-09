package main

func strStr(haystack string, needle string) int {
	m := len(needle)
	// 主串
	n := len(haystack)
	// 失效数组
	var next []int = getNext(needle, m)
	j := 0
	for i := 0; i < n; i++ {
		for j > 0 && haystack[i] != needle[j] {
			j = next[j-1] + 1
		}
		if haystack[i] == needle[j] {
			j += 1
		}
		if j == m {
			return i - m + 1
		}
	}
	return -1
}

// https://www.zhihu.com/question/21923021
func getNext(needle string, length int) []int {
	next := make([]int, length, length)
	next[0] = -1
	k := -1
	// 后缀
	for i := 1; i < length; i++ {
		for k != -1 && needle[k+1] != needle[i] { // 前后缀不同
			k = next[k]
		}
		if needle[k+1] == needle[i] {
			k += 1
		}
		next[i] = k
	}
	return next
}
