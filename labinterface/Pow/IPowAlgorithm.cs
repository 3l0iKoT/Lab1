using System.Collections.Generic;

namespace labinterface
{
    public interface IPowAlgorithm
    {
        List<(int, int)> Steps { get; } // Список шагов (степень, количество шагов)
        void Run(int maxDegree); // Метод для выполнения алгоритма
    }
}
