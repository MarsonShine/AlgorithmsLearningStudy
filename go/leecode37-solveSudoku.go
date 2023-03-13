package main

// https://leetcode.cn/problems/sudoku-solver/
// 数独问题
func solveSudoku(board [][]byte) {
	var getSudoku func() bool
	getSudoku = func() bool {
		// 遍历行
		for i := 0; i < 9; i++ {
			// 遍历列
			for j := 0; j < 9; j++ {
				if board[i][j] != '.' {
					continue
				}
				// 放1~9数字
				for n := '1'; n <= '9'; n++ {
					if isValidSudoku(byte(n), i, j, board) {
						board[i][j] = byte(n)
						if getSudoku() {
							return true
						}
						board[i][j] = byte('.')
					}
				}
				return false
			}
		}
		return true
	}
	getSudoku()
}

// 同行，同列不能有相同的，1～9
// 九宫格内的数字不能重复
func isValidSudoku(n byte, row, col int, bord [][]byte) bool {
	for i := 0; i < 9; i++ {
		if bord[i][col] == n {
			return false
		}
		if bord[row][i] == n {
			return false
		}
		startRow := row / 3 * 3
		startCol := col / 3 * 3
		for j := startRow; j < startRow+3; j++ {
			for k := startCol; k < startCol+3; k++ {
				if bord[j][k] == n {
					return false
				}
			}
		}
	}
	return true
}
