﻿using System;
using System.Diagnostics;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LabLogic
{
    public class Program
    {
        static void Main()
        {
            int maxN = 1000;  // Максимальное значение n
            int numTrials = 10;  // Количество запусков для каждого n
            double x = 1.5;  // Значение x для полинома

            int mode = int.Parse(Console.ReadLine());
            Program program = new Program();
            // Массив для хранения среднего времени
            double[] time = new double[maxN];

            Random random = new Random();

            for (int n = 1; n <= maxN; n++)
            {
                double[] times = new double[numTrials];

                // Пять запусков для замера времени
                for (int trial = 0; trial < numTrials; trial++)
                {
                    // Генерация случайного вектора длины n
                    int[] vector = new int[n];
                    for (int i = 0; i < n; i++)
                    {
                        vector[i] = random.Next(maxN);
                    }
                    times[trial] = program.StopWatchFunc(mode, vector, x);
                }

                // Среднее время выполнения для каждой функции
                time[n - 1] = times.Average();
            }

            // Вывод данных в консоль для анализа
            Console.WriteLine($"n\t{mode}");
            for (int n = 1; n <= maxN; n++)
            {
                Console.WriteLine($"{time[n - 1]:0.00000000000000000000}");
            }
        }

        public double StopWatchFunc(int mode, int[] vector, double x)
        {
            Stopwatch stopwatch;
            switch (mode)
            {
                case 1:
                    // Замер времени для постоянной функции
                    stopwatch = Stopwatch.StartNew();
                    ConstantFunction(vector);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 2:
                    // Замер времени для суммы элементов
                    stopwatch = Stopwatch.StartNew();
                    SumFunction(vector);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 3:
                    // Замер времени для произведения элементов
                    stopwatch = Stopwatch.StartNew();
                    ProductFunction(vector);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 4:
                    // Замер времени для наивного метода вычисления полинома
                    stopwatch = Stopwatch.StartNew();
                    NaivePolynomial(vector, x);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 5:
                    // Замер времени для метода Горнера
                    stopwatch = Stopwatch.StartNew();
                    HornerMethod(vector, x);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 6:
                    // Bubble Sort
                    stopwatch = Stopwatch.StartNew();
                    BubbleSort(vector);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 7:
                    // Quick Sort
                    stopwatch = Stopwatch.StartNew();
                    QuickSort(vector, 0, vector.Length - 1);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 8:
                    // Замер времени для сортировки Timsort
                    stopwatch = Stopwatch.StartNew();
                    TimSort(vector);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 9:
                    // OddEven Sort
                    stopwatch = Stopwatch.StartNew();
                    OddEvenSort(vector);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 10:
                    // Matrix multiply
                    int size = vector.Length; // size of matrix
                    int[,] A = GenerateRandomMatrix(size); // 1st matrix generator
                    int[,] B = GenerateRandomMatrix(size); // 2nd matrix generator
                    stopwatch = Stopwatch.StartNew();
                    MultiplyMatrices(A, B, size);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 11:
                    // Замер времени для сортировки расчёской (Comb sort)
                    stopwatch = Stopwatch.StartNew();
                    CombSort(vector);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                case 12:
                    // Selection Sort
                    stopwatch = Stopwatch.StartNew();
                    SelectionSort(vector);
                    stopwatch.Stop();
                    return stopwatch.Elapsed.TotalMilliseconds;
                default:
                    return 0;
            }
        }

        // Постоянная функция f(v) = 1
        public static double ConstantFunction(int[] v)
        {
            return 1;
        }

        // Сумма элементов вектора f(v) = сумма(v)
        public static double SumFunction(int[] v)
        {
            return v.Sum();
        }

        // Произведение элементов вектора f(v) = произведение(v)
        public static double ProductFunction(int[] v)
        {
            return v.Aggregate(1.0, (prod, next) => prod * next);
        }

        // Наивное вычисление полинома
        public static double NaivePolynomial(int[] coefficients, double x)
        {
            double result = 0;
            int n = coefficients.Length;

            // Прямое вычисление по формуле P(x) = v1 + v2 * x + v3 * x^2 + ...
            for (int i = 0; i < n; i++)
            {
                result += coefficients[i] * Math.Pow(x, i);
            }

            return result;
        }

        // Вычисление полинома методом Горнера
        public static double HornerMethod(int[] coefficients, double x)
        {
            double result = 0;
            int n = coefficients.Length;

            // Метод Горнера: P(x) = v1 + x(v2 + x(v3 + ...))
            for (int i = n - 1; i >= 0; i--)
            {
                result = result * x + coefficients[i];
            }

            return result;
        }

        // Реализация пузырьковой сортировки
        public static void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        // Реализация быстрой сортировки
        public static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);
                QuickSort(array, left, pivot - 1);
                QuickSort(array, pivot + 1, right);
            }
        }

        // help method for Quick Sort
        public static int Partition(int[] array, int left, int right)
        {
            double pivot = array[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i + 1, right);
            return i + 1;
        }
        
        // Swap method
        public static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        //TimSort

        public const int RUN = 32;

        //Функция Timsort
        public static void TimSort(int[] arr)
        {
            int n = arr.Length;

            // Сортировка отдельных подмассивов по размеру RUN
            for (int i = 0; i < n; i += RUN)
                InsertionSort(arr, i,
                              Math.Min((i + RUN - 1), (n - 1)));

            // Старт Merge с размера RUN (или 32). 
            // Оно будет объединено 
            // до размера 64, затем 
            // 128, 256 и так далее....
            for (int size = RUN; size < n; size = 2 * size)
            {

                // Выбераем начальную точку левого подмассива.
                // Мы собираемся объединить arr[left..left+size-1] и arr[left+size, left+2*size-1] 
                // После каждого Merge мы увеличиваем 
                // левые на 2*size
                for (int left = 0; left < n; left += 2 * size)
                {

                    // Находим конечную точку левого подмассива
                    // mid+1 - начальная точка правого подмассива
                    int mid = left + size - 1;
                    int right = Math.Min((left + 2 * size - 1),
                                         (n - 1));

                    // Слияние подмассивов arr[слева.....посередине] и arr[середина+1....справа]
                    if (mid < right)
                        Merge(arr, left, mid, right);
                }
            }
        }
        // Эта функция сортирует массив от левого индекса к
        // к правому индексу, размер которого не превышает RUN
        public static void InsertionSort(int[] arr, int left,
                                         int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = arr[i];
                int j = i - 1;
                while (j >= left && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
        }

        // Функция merge объединяет отсортированные прогоны 
        public static void Merge(int[] arr, int l, int m, int r)
        {
            // Исходный массив разбит на две части 
            // левый и правый массив 
            int len1 = m - l + 1, len2 = r - m;
            int[] left = new int[len1];
            int[] right = new int[len2];
            for (int x = 0; x < len1; x++)
                left[x] = arr[l + x];
            for (int x = 0; x < len2; x++)
                right[x] = arr[m + 1 + x];

            int i = 0;
            int j = 0;
            int k = l;

            // После сравнения мы объединяем эти два массива 
            // в большиц подмассив
            while (i < len1 && j < len2)
            {
                if (left[i] <= right[j])
                {
                    arr[k] = left[i];
                    i++;
                }
                else
                {
                    arr[k] = right[j];
                    j++;
                }
                k++;
            }

            // Копируем левые элементы если такие существуют
            while (i < len1)
            {
                arr[k] = left[i];
                k++;
                i++;
            }

            // Копируем правые элементы если такие существуют
            while (j < len2)
            {
                arr[k] = right[j];
                k++;
                j++;
            }
        }

        public static void OddEvenSort(int[] array)
        {
            int n = array.Length;

            for (int i = 1; i < n - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    Swap(array, i, i + 1);
                }
                for (int j = 0; j < n - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(array, j, j + 1);
                    }
                }
            }
        }

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
        public static void CombSort(int[] array)
        {
            int n = array.Length;
            int gap = n;
            bool swapped = true;

            while (gap != 1 || swapped)
            {
                gap = GetNextGap(gap);
                swapped = false;

                for (int i = 0; i < n - gap; i++)
                {
                    if (array[i] > array[i + gap])
                    {
                        Swap(array, i, i + gap);
                        swapped = true;
                    }
                }
            }
        }

        private static int GetNextGap(int gap) // Дополнение к Comb sort
        {
            gap = (gap * 10) / 13; // Уменьшаем шаг на 30%
            return gap < 1 ? 1 : gap; // Минимальный шаг равен 1
        }

        // Реализация сортировки выбором
        static void SelectionSort(int[] arr)
        {
            int n = arr.Length;

            // Один за другим перемещаем границу неотсортированной части массива
            for (int i = 0; i < n - 1; i++)
            {
                // Находим минимальный элемент в неотсортированной части
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }

                // Обмениваем найденный минимальный элемент с первым элементом неотсортированной части
                int temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }
        }
    }
}
