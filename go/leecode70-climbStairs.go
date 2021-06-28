package main

func climbStairs(n int) int {
	if n == 1 {
		return 1
	}
	if n == 2 {
		return 2
	}
	return climbStairs(n-1) + climbStairs(n-2)
}

func climbStairs2(n int) int {
	return climbStairsTail(n, 0, 1)
}

func climbStairsTail(n int, acc1 int, acc2 int) int {
	if n == 0 {
		return acc2
	}
	return climbStairsTail(n-1, acc2, acc1+acc2)
}
