using System.Diagnostics;
using labinterface;

namespace LabLogic
{

        public class Program
        {
            public double StopWatchFunc(string mode, int[] vector, double x, int degree)
            {
                IPowAlgorithm[] algorithms = new IPowAlgorithm[]
                {
                        new PowStandart(),
                        new QuickPow(),
                        new QuickPow1(),
                        new RecPow()
                };

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
                case "Pow(x,n)":
                    var algorithm1 = algorithms[0];
                    algorithm1.Run(degree);
                    return algorithm1.Steps[0].Item2;
                case "QuickPow(x,n)":
                    var algorithm2 = algorithms[1];
                    algorithm2.Run(degree);
                    return algorithm2.Steps[0].Item2;
                case "QuickPow1(x,n)":
                    var algorithm3 = algorithms[2];
                    algorithm3.Run(degree);
                    return algorithm3.Steps[0].Item2;
                case "RecPow(x,n)":
                    var algorithm4 = algorithms[3];
                    algorithm4.Run(degree);
                    return algorithm4.Steps[0].Item2;
                default:
                    return 0;
                }
                stopwatch.Stop();
                return stopwatch.Elapsed.TotalMilliseconds;
            }
        }
}
