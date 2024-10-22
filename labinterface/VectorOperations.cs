using System;

namespace labinterface
{
    public class VectorOperations
    {
        public static double ConstantFunction(int[] v)
        {
            double result = v.Length * 2;
            return result;
        }

        // сумма элементов
        public static double SumFunction(int[] v)
        {
            double product = 0.0;
            for (int i = 0; i < v.Length; i++)
            {
                product += v[i];
            }
            return product;
        }

        // каждый элемент умножается на 1
        public static double ProductFunction(int[] v)
        {
            double product = 1.0;
            for (int i = 0; i < v.Length; i++)
            {
                product *= v[i];
            }
            return product;
        }

        // подсчет полинома прямым методом
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

        // метод Горнера
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
    }
}
