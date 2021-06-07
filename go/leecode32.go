package main

func longestValidParentheses(s string) int {
	// 遍历
	l := len(s)
	c := 0
	arr := []byte{}
	// left := byte('(')  // (
	right := byte(')') // )
	for i := 0; i < l; i++ {
		if s[i] == byte(right) {
			if len(arr) == 0 {
				continue
			}
			arr = arr[:len(arr)-1]
			c += 2
		} else {
			arr = append(arr, s[i])
		}
	}
	return c
}

// func longestValidParentheses(s string) int {
// 	maxAns := 0
// 	dp := make([]int, len(s))
// 	for i := 1; i < len(s); i++ {
// 		if s[i] == ')' {
// 			if s[i-1] == '(' {
// 				if i >= 2 {
// 					dp[i] = dp[i-2] + 2
// 				} else {
// 					dp[i] = 2
// 				}
// 			} else if i-dp[i-1] > 0 && s[i-dp[i-1]-1] == '(' {
// 				if i-dp[i-1] >= 2 {
// 					dp[i] = dp[i-1] + dp[i-dp[i-1]-2] + 2
// 				} else {
// 					dp[i] = dp[i-1] + 2
// 				}
// 			}
// 			maxAns = max(maxAns, dp[i])
// 		}
// 	}
// 	return maxAns
// }

// func max(x, y int) int {
// 	if x > y {
// 		return x
// 	}
// 	return y
// }
