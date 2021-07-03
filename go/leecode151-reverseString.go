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
