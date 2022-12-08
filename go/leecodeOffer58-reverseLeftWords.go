package main

func reverseLeftWords(s string, n int) string {
	buf := []byte(s)
	length := len(buf)
	leftWords := make([]byte, 0, n)
	newBuf := make([]byte, 0, length)
	for i := 0; i < length; i++ {
		if i < n {
			leftWords = append(leftWords, buf[i])
			continue
		}
		newBuf = append(newBuf, buf[i])
	}
	newBuf = append(newBuf, leftWords...)
	return string(newBuf)
}

// 采用全部反转，按尾部 n 个局部反转
func reverseLeftWords2(s string, n int) string {
	buf := []byte(s)
	// 全部反转
	reverseString2(buf)
	// 局部反转
	reverseString2(buf[:len(buf)-n])
	reverseString2(buf[len(buf)-n:])
	return string(buf)
}
