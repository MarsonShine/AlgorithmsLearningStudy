package main

// https://leetcode.cn/problems/remove-all-adjacent-duplicates-in-string/
// 删除字符串中的所有相邻重复项

func removeDuplicates(s string) string {
	if len(s) <= 1 {
		return s
	}
	buf := []byte(s)
	newBuf := make([]byte, 0)
	var nl int = 0 // 表示 newBuf 的长度
	for i := 0; i < len(buf); i++ {
		if nl == 0 {
			newBuf = append(newBuf, buf[i])
			nl++
			continue
		}
		if buf[i] == newBuf[nl-1] {
			newBuf = newBuf[:nl-1]
			nl--
		} else {
			newBuf = append(newBuf, buf[i])
			nl++
		}
	}
	return string(newBuf)
}
