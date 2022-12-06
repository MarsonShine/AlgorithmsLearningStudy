package main

import (
	"strings"
)

func reverseWords(s string) string {
	s = strings.Trim(s, " ")
	ar := strings.Fields(s)
	for i := 0; i < len(ar)/2; i++ {
		ar[i], ar[len(ar)-1-i] = ar[len(ar)-1-i], ar[i]
	}
	return strings.Join(ar, " ")
}

func reverseWords2(s string) string {
	buf := []byte(s)
	// 删除空格
	left := 0
	right := 0
	length := len(s)
	for length > 0 && left < length && buf[left] == ' ' {
		left++
	}
	for ; left < length; left++ {
		if left-1 > 0 && buf[left-1] == buf[left] && buf[left] == ' ' {
			continue
		}
		buf[right] = buf[left]
		right++
	}
	// 去掉最后的空格
	if left-1 > 0 && buf[right-1] == ' ' {
		buf = buf[:right-1]
	} else {
		buf = buf[:right]
	}
	// 全部倒叙
	reverseString2(buf)
	// 每个单词再反转一遍
	left, right = 0, 0
	for i := 0; i < len(buf); i++ {
		if buf[i] == ' ' {
			reverse(buf[left:right])
			left = right + 1
			right++
		} else {
			right++
		}
	}
	if right > left {
		reverse(buf[left:right])
	}
	return string(buf)
}
