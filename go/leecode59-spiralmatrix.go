package main

func generateMatrix(n int) [][]int {
	matrix := make([][]int, n)
	// init
	for i := 0; i < n; i++ {
		matrix[i] = make([]int, n)
	}
	loop := 0 // 便利次数
	val := 1
	start := 0 //每次循环的开始点
	var x, y int
	for loop < n/2 {
		loop++
		// 从左到右
		for y = start; y < n-loop; y++ {
			matrix[start][y] = val
			val++
		}
		// 从上到下
		for x = start; x < n-loop; x++ {
			matrix[x][y] = val
			val++
		}
		// 从右到左
		for ; y >= loop; y-- {
			matrix[x][y] = val
			val++
		}
		// 从下到上
		for ; x >= loop; x-- {
			matrix[x][y] = val
			val++
		}
		start++
	}
	if n%2 == 1 {
		matrix[start][start] = val
	}
	return matrix
}

/*
n=3 [1,2,3] [8,9,4] [7,6,5]
n=4 [1,2,3,4] [12,13,14,5] [11,16,15,6] [10,9,8,7]
*/
