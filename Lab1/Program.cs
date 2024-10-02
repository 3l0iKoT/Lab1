using System;
using System.Diagnostics;
using System.Linq;
using Lab1;

namespace LabLogic
{

        public class Program
        {
            static void Main()
            {
                // не используется
            }
            // метод для посчета времени используя Stopwatch, выходные параметры mode => case(номер функции)
            // vector - сам массив, x - используется для полинома 
            public double StopWatchFunc(int mode, int[] vector, double x)
            {
                VectorOperations vectorOperations = new VectorOperations();                
                Stopwatch stopwatch = Stopwatch.StartNew();
                switch (mode)
                {
                    case 1:
                        VectorOperations.ConstantFunction(vector);
                        break;
                    case 2:
                        VectorOperations.SumFunction(vector);
                        break;
                    case 3:
                        VectorOperations.ProductFunction(vector);
                        break;
                    case 4:
                        VectorOperations.NaivePolynomial(vector, x);
                        break;
                    case 5:
                        VectorOperations.HornerMethod(vector, x);
                        break;
                    case 6:
                        SortingAlgorithms.BubbleSort(vector);
                        break;
                    case 7:
                        SortingAlgorithms.QuickSort(vector, 0, vector.Length - 1);
                        break;
                    case 8:
                        SortingAlgorithms.OddEvenSort(vector);
                        break;
                    case 9:
                        SortingAlgorithms.CombSort(vector);
                        break;
                    case 11:
                        SortingAlgorithms.SelectionSort(vector);
                        break;
                    case 12:
                        SortingAlgorithms.TimSort(vector);
                        break;
                    case 10: // here maybe bug with time m/sec
                        int size = vector.Length; // size of matrix
                        int[,] A = MatrixOperations.GenerateRandomMatrix(size); // 1st matrix generator
                        int[,] B = MatrixOperations.GenerateRandomMatrix(size); // 2nd matrix generator
                        MatrixOperations.MultiplyMatrices(A, B, size);
                        break;
                    default:
                        return 0;
                }
                stopwatch.Stop();
                return stopwatch.Elapsed.TotalMilliseconds;
            }
        }
}
