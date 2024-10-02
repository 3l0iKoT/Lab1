using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class SortingAlgorithms
    {
        // сортировка пузырьком, массив делится на 2 части
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

        // быстрая сортировка
        public static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);
                QuickSort(array, left, pivot - 1);
                QuickSort(array, pivot + 1, right);
            }
        }

        // вспомогательный метод
        public static int Partition(int[] array, int left, int right)
        {
            double pivot = array[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    Array.Reverse(array, i, j);
                }
            }
            Array.Reverse(array, i + 1, right - i);
            return i + 1;
        }

        // чет-нечет сортировка
        public static void OddEvenSort(int[] array)
        {
            int n = array.Length;

            for (int i = 1; i < n - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    Array.Reverse(array, i, i + 1);
                }
                for (int j = 0; j < n - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Array.Reverse(array, j, j + 1);
                    }
                }
            }
        }

        // сортировка расческой
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
                        Array.Reverse(array, i, i + gap);
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

        // сортировка выборкой
        public static void SelectionSort(int[] arr)
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

        public const int RUN = 32;

        //Функция Timsort
        public static void TimSort(int[] arr)
        {
            int n = arr.Length;

            // Сортировка отдельных подмассивов по размеру RUN
            for (int i = 0; i < n; i += RUN)
                InsertionSort(arr, i, Math.Min((i + RUN - 1), (n - 1)));

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
                    int right = Math.Min((left + 2 * size - 1), (n - 1));

                    // Слияние подмассивов arr[слева.....посередине] и arr[середина+1....справа]
                    if (mid < right)
                        Merge(arr, left, mid, right);
                }
            }
        }

        //Эта функция сортирует массив от левого индекса к
        // к правому индексу, размер которого не превышает RUN
        public static void InsertionSort(int[] arr, int left, int right)
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
    }
}
