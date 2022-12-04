package main

func replaceSpace(s string) string {
	buf := make([]byte, 0, len(s))
	target := ' '
	for _, c := range s {
		if c == target {
			buf = append(buf, '%', '2', '0')
		} else {
			buf = append(buf, byte(c))
		}
	}
	return string(buf)
}

func replaceSpace2(s string) string {
	var count int = 0
	for _, c := range s {
		if c == ' ' {
			count++
		}
	}
	oldLength := len(s)
	// resize
	buf := []byte(s)
	resizeCount := count*3 - count
	for i := 0; i < resizeCount; i++ {
		buf = append(buf, 'a')
	}
	newLength := len(buf)
	left := oldLength - 1
	right := newLength - 1
	for left < right {
		if buf[left] == ' ' {
			buf[right] = '0'
			right -= 1
			buf[right] = '2'
			right -= 1
			buf[right] = '%'
			right -= 1
			left--
		} else {
			buf[right] = buf[left]
			left--
			right--
		}
	}
	return string(buf)
}
