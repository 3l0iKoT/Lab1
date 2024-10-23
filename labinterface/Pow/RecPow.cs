using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labinterface
{
    public class RecPow : IPowAlgorithm
    {
        public List<(int, int)> Steps { get; private set; } = new List<(int, int)>();

        public void Run(int maxDegree)
        {
            int number = 2;
            int degree = maxDegree;
            int stepCount = 0;
            int result = RecPowRecursive(number, degree, ref stepCount);
            Steps.Add((degree, stepCount));
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
    }
}
