package main

import "strings"

// https://leetcode.cn/problems/n-queens/
// N皇后问题
func solveNQueens(n int) [][]string {
	results := [][]string{}
	// 初始化
	chessboard := make([][]string, n)
	for i := 0; i < n; i++ {
		chessboard[i] = make([]string, n)
	}
	for i := 0; i < n; i++ {
		for j := 0; j < n; j++ {
			chessboard[i][j] = "."
		}
	}

	var getNQueens func(int) // n, 当前行, 路径
	getNQueens = func(row int) {
		if row == n {
			temp := make([]string, n)
			for i, rowStr := range chessboard {
				temp[i] = strings.Join(rowStr, "")
			}
			results = append(results, temp)
			return
		}
		for i := 0; i < n; i++ {
			if valiedNQueen(n, i, row, chessboard) {
				chessboard[row][i] = "Q"
				getNQueens(row + 1)
				chessboard[row][i] = "."
			}
		}
	}

	getNQueens(n)
	return results
}

func valiedNQueen(n, i, row int, chessboard [][]string) bool {
	// 3种情况 x不动 y动（同列） 2：x动 y不动（同行） 3：x y 都递增/减（斜线）
}
