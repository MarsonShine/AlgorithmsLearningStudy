package main

// https://leetcode.cn/problems/best-time-to-buy-and-sell-stock-ii/
// 买卖股票的最佳时机
func maxProfit(prices []int) int {
	result := 0
	for i := 1; i < len(prices); i++ {
		result += max(prices[i]-prices[i-1], 0)
	}
	return result
}

// dp，最多只能持有一股
func maxProfit2(prices []int) int {
	size := len(prices)
	dp := make([][2]int, size)
	// 初始化，有两种操作，购买股票；出售股票
	dp[0][0] -= prices[0] // 持有股票
	dp[0][1] = 0          // 持有的现金
	for i := 1; i < size; i++ {
		// 持有股票 = 前一天的持有股票所得现金，前一天出售股票的现金
		dp[i][0] = max(dp[i-1][0], dp[i-1][1]-prices[i])
		// 出售股票（不持有股票）= 前一天持有股票 + 出售股票
		dp[i][1] = max(dp[i-1][1], dp[i-1][0]+prices[i])
	}
	return dp[size-1][1]
}
