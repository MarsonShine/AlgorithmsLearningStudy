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
					if isSudoku(byte(n), i, j, board) {
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

func isSudoku(n byte, row, col int, bord [][]byte) bool {
	return true
}
