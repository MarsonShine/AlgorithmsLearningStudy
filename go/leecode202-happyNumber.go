package main

// https://leetcode.cn/problems/happy-number/
// 判断是否是快乐数
func isHappy(n int) bool {
	m := make(map[int]bool, 0)
	sum := getSum(n)
	for !m[sum] {
		if sum == 1 {
			return true
		}
		m[sum] = true
		sum = getSum(sum)
	}
	return false
}

func getSum(n int) int {
	sum := 0
	for n > 0 {
		sum += (n % 10) * (n % 10) // 取余（个位数）
		n = n / 10                 // 取整
	}
	return sum
}
