using System;
using System.Diagnostics;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main()
    {
        int maxN = 2000;  // Максимальное значение n
        int numTrials = 5;  // Количество запусков для каждого n
        double x = 1.5;  // Значение x для полинома

        string mode = Console.ReadLine();

        // Массивы для хранения среднего времени
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
                Stopwatch stopwatch;
                switch (mode)
                {
                    case "1":
                        // Замер времени для постоянной функции
                        stopwatch = Stopwatch.StartNew();
                        ConstantFunction(vector);
                        stopwatch.Stop();
                        times[trial] = stopwatch.Elapsed.TotalMilliseconds;
                        break;
                    case "2":
                        // Замер времени для суммы элементов
                        stopwatch = Stopwatch.StartNew();
                        SumFunction(vector);
                        stopwatch.Stop();
                        times[trial] = stopwatch.Elapsed.TotalMilliseconds;
                        break;
                    case "3":
                        // Замер времени для произведения элементов
                        stopwatch = Stopwatch.StartNew();
                        ProductFunction(vector);
                        stopwatch.Stop();
                        times[trial] = stopwatch.Elapsed.TotalMilliseconds;
                        break;
                    case "4":
                        // Замер времени для наивного метода вычисления полинома
                        stopwatch = Stopwatch.StartNew();
                        NaivePolynomial(vector, x);
                        stopwatch.Stop();
                        times[trial] = stopwatch.Elapsed.TotalMilliseconds;
                        break;
                    case "5":
                        // Замер времени для метода Горнера
                        stopwatch = Stopwatch.StartNew();
                        HornerMethod(vector, x);
                        stopwatch.Stop();
                        times[trial] = stopwatch.Elapsed.TotalMilliseconds;
                        break;
                    case "6":
                        // Замер времени для пузырьковой сортировки
                        int[] vectorCopy = (int[])vector.Clone();
                        stopwatch = Stopwatch.StartNew();
                        BubbleSort(vectorCopy);
                        stopwatch.Stop();
                        times[trial] = stopwatch.Elapsed.TotalMilliseconds;
                        break;
                    case "7":
                        // Замер времени для быстрой сортировки
                        vectorCopy = (int[])vector.Clone();
                        stopwatch = Stopwatch.StartNew();
                        QuickSort(vectorCopy, 0, vectorCopy.Length - 1);
                        stopwatch.Stop();
                        times[trial] = stopwatch.Elapsed.TotalMilliseconds;
                        break;
                    case "8":
                        // Замер времени для сортировки Timsort (Array.Sort, т.к. это его реализация)
                        vectorCopy = (int[])vector.Clone();
                        stopwatch = Stopwatch.StartNew();
                        Array.Sort(vectorCopy);  // В .NET используется Timsort для массивов
                        stopwatch.Stop();
                        times[trial] = stopwatch.Elapsed.TotalMilliseconds;
                        break;
                }
            }

            // Среднее время выполнения для каждой функции
            time[n - 1] = times.Average();
        }

        // Вывод данных в консоль для анализа
        Console.WriteLine($"n\t{mode}");
        for (int n = 1; n <= maxN; n++)
        {
            Console.WriteLine($"{n}\t{time[n - 1]:0.00000000000000000000}");
        }
    }

    // Постоянная функция f(v) = 1
    static double ConstantFunction(int[] v)
    {
        return 1;
    }

    // Сумма элементов вектора f(v) = сумма(v)
    static double SumFunction(int[] v)
    {
        return v.Sum();
    }

    // Произведение элементов вектора f(v) = произведение(v)
    static double ProductFunction(int[] v)
    {
        return v.Aggregate(1.0, (prod, next) => prod * next);
    }

    // Наивное вычисление полинома
    static double NaivePolynomial(int[] coefficients, double x)
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
    static double HornerMethod(int[] coefficients, double x)
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
    static void BubbleSort(int[] array)
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
    static void QuickSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(array, left, right);
            QuickSort(array, left, pivot - 1);
            QuickSort(array, pivot + 1, right);
        }
    }

    static int Partition(int[] array, int left, int right)
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

    static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}