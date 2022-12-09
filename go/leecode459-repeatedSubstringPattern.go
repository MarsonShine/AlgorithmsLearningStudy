package main

func repeatedSubstringPattern(s string) bool {
	n := len(s)
	for i := 0; i*2 <= n/2; i++ {
		if n%i == 0 {
			b := true
			for j := i; j < n; j++ {
				if s[j] != s[j-1] {
					b = false
					break
				}
			}
			if b {
				return true
			}
		}
	}
	return false
}
