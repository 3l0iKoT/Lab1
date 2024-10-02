using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class MatrixOperations
    {

        // генератор рандомных матриц
        public static int[,] GenerateRandomMatrix(int size)
        {
            Random rand = new Random();
            int[,] matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rand.Next(0, 21); // Случайные значения от 0 до 20
                }
            }
            return matrix;
        }

        // перемножение двух матрицы
        public static int[,] MultiplyMatrices(int[,] A, int[,] B, int size)
        {
            int[,] result = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        result[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return result;
        }
    }
}
