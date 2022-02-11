package dp

// 参考链接：https://juejin.cn/post/6844903993429196813
// 1. 跳台阶，一次只能跳一个或两个台阶，问跳 n 个台阶总给有多少种跳法
func DP1(n int) int {
	if n <= 2 {
		return n
	}

	dp := make([]int, n+1)
	dp[0] = 0
	dp[1] = 1
	dp[2] = 2

	// 递推公式
	for i := 3; i <= n; i++ {
		dp[i] = dp[i-2] + dp[i-1]
	}
	return dp[n]
}

// 不同路径
func DP2(n int, m int, dp [][]int) int {
	if n <= 0 || m <= 0 {
		return 0
	}
	// 初始化
	for i := 0; i < n; i++ {
		dp[i][0] = 1
	}
	for i := 0; i < m; i++ {
		dp[0][i] = 1
	}
	// 递推公式
	for i := 1; i < n; i++ {
		for j := 1; j < m; j++ {
			dp[i][j] = dp[i-1][j] + dp[i][j-1]
		}
	}
	return dp[n-1][m-1]
}
