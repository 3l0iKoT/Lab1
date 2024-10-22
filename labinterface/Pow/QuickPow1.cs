using System.Collections.Generic;

namespace labinterface
{
    public class QuickPow1 : IPowAlgorithm
    {
        public List<(int, int)> Steps { get; private set; } = new List<(int, int)>();

        public void Run(int maxDegree)
        {
            for (int i = 0; i < maxDegree; i++)
            {
                int count = 0;
                int number = 2;
                long res = 1;
                int degree = i;

                while (degree != 0)
                {
                    if (degree % 2 == 0)
                    {
                        number *= number;
                        degree /= 2;
                        count += 2;
                    }
                    else
                    {
                        res *= number;
                        degree--;
                        count += 2;
                    }
                }
                Steps.Add((i, count));
            }
        }

        public string GetName()
        {
            return "QuickPow1";
        }
    }
}
