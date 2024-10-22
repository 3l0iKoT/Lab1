using System.Collections.Generic;
namespace labinterface
{
    public class QuickPow : IPowAlgorithm
    {
        public List<(int, int)> Steps { get; private set; } = new List<(int, int)>();

        public void Run(int maxDegree)
        {
            int number = 2;
            for (int degree = 1; degree <= maxDegree; degree++)
            {
                int result = 1;
                int stepCount = 0;
                int exp = degree;
                int baseNum = number;

                while (exp > 0)
                {
                    if (exp % 2 == 1)
                    {
                        result *= baseNum;
                        stepCount++;
                    }
                    baseNum *= baseNum;
                    exp /= 2;
                    stepCount++;
                }
                Steps.Add((degree, stepCount));
            }
        }

        public string GetName()
        {
            return "QuickPow";
        }
    }
}
