package main

func numIslands(grid [][]byte) int {
	if len(grid) == 0 {
		return 0
	}
	row := len(grid)
	column := len(grid[0])
	numberOfLand := 0
	for i := 0; i < row; i++ {
		for j := 0; j < column; j++ {
			if grid[i][j] == '1' {
				numberOfLand++
				dfsSearchLands(grid, i, j)
			}
		}
	}
	return numberOfLand
}

func dfsSearchLands(grid [][]byte, i, j int) {
	r := len(grid)
	c := len(grid[0])
	if i < 0 || j < 0 || i >= r || j >= c || grid[i][j] == '0' {
		return
	}
	grid[i][j] = '0' // 防止重复查询
	// 四个方向
	dfsSearchLands(grid, i-1, j)
	dfsSearchLands(grid, i+1, j)
	dfsSearchLands(grid, i, j-1)
	dfsSearchLands(grid, i, j+1)
}

func bfsSearchLands(grid [][]byte) int {
	if len(grid) == 0 {
		return 0
	}
	r := len(grid)
	c := len(grid[0])
	numberOfLand := 0
	for i := 0; i < r; i++ {
		for j := 0; j < c; j++ {
			if grid[i][j] == '1' {
				numberOfLand++
				grid[i][j] = '0'
				queue := []int{}
				queue = append(queue, i*c+j)
				for len(queue) != 0 {
					id := queue[:1][0]
					queue = queue[1:]
					row := id / c
					col := id % c
					if row-1 >= 0 && grid[row-1][col] == '1' {
						queue = append(queue, (row-1)*c+col)
						grid[row-1][col] = '0'
					}
					if row+1 < r && grid[row+1][col] == '1' {
						queue = append(queue, (row+1)*c+col)
						grid[row+1][col] = '0'
					}
					if col-1 >= 0 && grid[row][col-1] == '1' {
						queue = append(queue, row*c+col-1)
						grid[row][col-1] = '0'
					}
					if col+1 < c && grid[row][col+1] == '1' {
						queue = append(queue, row*c+col+1)
						grid[row][col+1] = '0'
					}
				}
			}
		}
	}
	return numberOfLand
}
