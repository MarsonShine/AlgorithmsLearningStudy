package main

func longestValidParentheses(s string) int {
	// 遍历
	l := len(s)
	notmatch := []int{}
	notmatch = append(notmatch, -1)
	maxLength := 0

	arr := []byte{}
	left := byte('(')  // (
	right := byte(')') // )
	for i := 0; i < l; i++ {
		arrl := len(arr)
		if s[i] == right {
			if arrl != 0 && arr[arrl-1] == left {
				arr = arr[:arrl-1]
				notmatch = notmatch[:len(notmatch)-1]
				longLength := i - notmatch[len(notmatch)-1]
				if maxLength < longLength {
					maxLength = longLength
				}
			} else {
				arr = append(arr, s[i])
				notmatch = append(notmatch, i)
			}
		} else {
			arr = append(arr, s[i])
			notmatch = append(notmatch, i)
		}
	}
	return maxLength
}
