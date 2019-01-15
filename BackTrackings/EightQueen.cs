using System;

namespace BackTrackings {
    /// <summary>
    /// 八皇后问题
    /// 有个8x8的棋盘，要求每个旗子放进去时，它所在的行和列以及对角线上的没有旗子，如何把8个旗子放到棋盘中
    /// </summary>
    public class EightQueen {
        private readonly int[] m_result; //下标表示行，值表示 queen 存储在那一列
        public EightQueen() {
            m_result = new int[8];
        }
        public void Call() {

        }
        private void CallEightQueen(int row) {
            if (row == m_result.Length) { //8个棋子都已经放好了，结束
                PrintQueen(m_result);
                return;
            }
            for (int column = 0; column < m_result.Length; column++) {
                if (IsOK(row, column)) { //是否符合存放规则
                    m_result[row] = column;
                    CallEightQueen(column + 1);
                }
            }
        }
        //判断row行 column列是否符合规则
        private bool IsOK(int row, int column) {
            int leftColumn = column - 1;
            int rightColumn = column + 1;
            for (int i = row - 1; i >= 0; i--) {
                //判断第i行的column列是否有棋子
                if (m_result[i] == column) return false;
                if (leftColumn >= 0) {
                    //左对角线上的第i行的left列有棋子么
                    if (m_result[i] == leftColumn) return false;
                }
                if (rightColumn < m_result.Length) {
                    if (m_result[i] == rightColumn) return false;
                }
                --leftColumn;
                ++rightColumn;
            }
            return true;
        }

        private void PrintQueen(int[] result) {
            for (int row = 0; row < result.Length; row++) {
                for (int column = 0; column < result.Length; column++) {
                    if (result[row] == column) Console.Write("Q ");
                    else Console.Write("* ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}