using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicProgramming
{
    // 最短路径问题
    // 从A点到G点的最短路径
    public class ShortestPathProblem
    {
        public static int MinPath(int[,] matrix)
        {
            return Process(matrix, matrix.GetLength(1) - 1);
        }

        private static int Process(int[,] matrix, int i)
        {
            if (i == 0)
                return 0;
            // 状态转移方程
            else
            {
                int distance = int.MaxValue;
                for (int j = 0; j < i; j++)
                {
                    if (matrix[j, i] != 0)
                    {
                        int temp = matrix[j, i] + Process(matrix, j);
                        if (temp < distance)
                        {
                            distance = temp;
                        }
                    }
                }
                return distance;
            }
        }
    }
}
