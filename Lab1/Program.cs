using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Lab1;


namespace LabLogic
{

        public class Program
        {
            static void Main()
            {
                int maxDegree = 10; // Вы можете изменить это значение

                // Создайте экземпляры ваших алгоритмов
                IPowAlgorithm[] algorithms = new IPowAlgorithm[]
                {
                    new PowStandart(),
                    new QuickPow(),
                    new QuickPow1(),
                    new RecPow()
                };

                // Запустите каждый алгоритм и выведите результаты
                foreach (var algorithm in algorithms)
                {
                    algorithm.Run(maxDegree);
                    Console.WriteLine($"{algorithm.GetName()}:");
                    foreach (var step in algorithm.Steps)
                    {
                        Console.WriteLine($"Степень: {step.Item1}, Шаги: {step.Item2}");
                    }
                    Console.WriteLine();
            }
        }
            // метод для посчета времени используя Stopwatch, выходные параметры mode => case(номер функции)
            // vector - сам массив, x - используется для полинома 
            public double StopWatchFunc(string mode, int[] vector, double x)
            {
                VectorOperations vectorOperations = new VectorOperations();                
                Stopwatch stopwatch = Stopwatch.StartNew();
                switch (mode)
                {
                    case "Постаянная функция":
                        VectorOperations.ConstantFunction(vector);
                        break;
                    case "Сумма элементов":
                        VectorOperations.SumFunction(vector);
                        break;
                    case "Произведение элементов":
                        VectorOperations.ProductFunction(vector);
                        break;
                    case "Полином":
                        VectorOperations.NaivePolynomial(vector, x);
                        break;
                    case "Метод Горнера":
                        VectorOperations.HornerMethod(vector, x);
                        break;
                    case "BubbleSort":
                        SortingAlgorithms.BubbleSort(vector);
                        break;
                    case "QuickSort":
                        SortingAlgorithms.QuickSort(vector, 0, vector.Length - 1);
                        break;
                    case "OddEvenSort":
                        SortingAlgorithms.OddEvenSort(vector);
                        break;
                    case "CombSort":
                        SortingAlgorithms.CombSort(vector);
                        break;
                    case "SelectionSort":
                        SortingAlgorithms.SelectionSort(vector);
                        break;
                    case "TimSort":
                        SortingAlgorithms.TimSort(vector);
                        break;
                    case "Умножение матриц": // here maybe bug with time m/sec
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
