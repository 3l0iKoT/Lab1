using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public interface IPowAlgorithm
    {
        List<(int, int)> Steps { get; } // Список шагов (степень, количество шагов)
        void Run(int maxDegree); // Метод для выполнения алгоритма
        string GetName(); // Имя алгоритма
    }
}
