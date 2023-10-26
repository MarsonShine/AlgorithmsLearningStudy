package main

// https://leetcode.cn/problems/candy/
func candy(ratings []int) int {
	length := len(ratings)
	candyValues := make([]int, length)
	for i := 0; i < length; i++ {
		candyValues[i] = 1
	}
	// 从前向后便利，找右边比左边大的
	for i := 1; i < length; i++ {
		if ratings[i] > ratings[i-1] {
			candyValues[i] = candyValues[i-1] + 1
		}
	}
	// 从后向前便利，找左边比右边大的
	for i := length - 2; i >= 0; i-- {
		if ratings[i] > ratings[i+1] {
			candyValues[i] = max(candyValues[i], candyValues[i+1]+1)
		}
	}
	// 这样经过两遍查找，就能找到candyValues[i]既可以保持比candyValues[i+1]的值大，也比candyValues[i-1]的值大
	return sum(candyValues)
}

func sum(values []int) int {
	sum := 0
	for i := 0; i < len(values); i++ {
		sum += values[i]
	}
	return sum
}
