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
		for col := 0; col < n; col++ {
			if valiedNQueen(n, col, row, chessboard) {
				chessboard[row][col] = "Q"
				getNQueens(row + 1)
				chessboard[row][col] = "."
			}
		}
	}

	getNQueens(0)
	return results
}

func valiedNQueen(n, col, row int, chessboard [][]string) bool {
	// 3种情况 x不动 y动（同列） 2：x动 y不动（同行） 3：x y 都递增/减（斜线）
	for i := 0; i < row; i++ {
		if chessboard[i][col] == "Q" {
			return false
		}
	}
	for i, j := row-1, col-1; i >= 0 && j >= 0; i, j = i-1, j-1 {
		if chessboard[i][j] == "Q" {
			return false
		}
	}
	for i, j := row-1, col+1; i >= 0 && j < n; i, j = i-1, j+1 {
		if chessboard[i][j] == "Q" {
			return false
		}
	}
	return true
}
