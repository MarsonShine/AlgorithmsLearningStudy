package main

func reverseString(s []byte) {
	l := len(s)
	if l < 2 {
		return
	}
	for i := 0; i < l/2; i++ {
		s[i], s[l-i-1] = s[l-i-1], s[i]
	}
}
