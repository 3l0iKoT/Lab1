using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labinterface
{
    public class QuickPow : IPowAlgorithm
    {
        public List<(int, int)> Steps { get; private set; } = new List<(int, int)>();

        public void Run(int maxDegree)
        {
            int number = 2;
            int result = 1;
            int stepCount = 0;
            int exp = maxDegree;
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
            Steps.Add((maxDegree, stepCount));
        }

        public string GetName()
        {
            return "QuickPow";
        }
    }
}
