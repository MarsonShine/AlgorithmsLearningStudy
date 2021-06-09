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

func longestValidParenthesesByDynamicProgram(s string) int {
	maxAns := 0
	dp := make([]int, len(s))
	for i := 0; i < len(s); i++ {
		if s[i] == ')' {
			if s[i-1] == '(' {
				if i >= 2 {
					dp[i] = dp[i-2] + 2
				} else {
					dp[i] = 2
				}
			} else if i-dp[i-1] > 0 && s[i-dp[i-1]-1] == '(' {
				if i-dp[i-1] >= 2 {
					dp[i] = dp[i-1] + dp[i-dp[i-1]-2] + 2
				} else {
					dp[i] = dp[i-1] + 2
				}
			}
			maxAns = max(maxAns, dp[i])
		}
	}
	return maxAns
}

func max(x, y int) int {
	if x > y {
		return x
	}
	return y
}
