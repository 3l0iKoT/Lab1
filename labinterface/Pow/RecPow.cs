using System.Collections.Generic;

namespace labinterface
{
    public class RecPow : IPowAlgorithm
    {
        public List<(int, int)> Steps { get; private set; } = new List<(int, int)>();

        public void Run(int maxDegree)
        {
            int number = 2;
            for (int degree = 1; degree <= maxDegree; degree++)
            {
                int stepCount = 0;
                int result = RecPowRecursive(number, degree, ref stepCount);
                Steps.Add((degree, stepCount));
            }
        }

        private int RecPowRecursive(int baseNum, int exp, ref int stepCount)
        {
            int f;

            stepCount += 1;

            if (exp == 0)
            {
                f = 1;
                return f;
            }
            f = RecPowRecursive(baseNum, exp / 2, ref stepCount);
            if (exp % 2 == 1)
            {
                f = f * f * baseNum;
            }
            else
            {
                f = f * f;
            }
            return f;
        }

        public string GetName()
        {
            return "RecPow";
        }
    }
}
