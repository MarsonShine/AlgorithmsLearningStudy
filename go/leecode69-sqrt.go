package main

func mySqrt(x int) int {
	if x < 2 {
		return x
	}
	start := 0
	end := x
	mid := half(start, end)
	for end-start > 1 {
		r := mid * mid
		if r == x {
			return mid
		}
		if r > x {
			end = mid
			mid = half(start, end)
		} else {
			start = mid
			mid = half(start, end)
		}
	}
	return start
}

func half(x, y int) int {
	return (x + y) / 2
}
