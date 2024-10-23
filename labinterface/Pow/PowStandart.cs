using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labinterface
{
    public class PowStandart : IPowAlgorithm
    {
        public List<(int, int)> Steps { get; private set; } = new List<(int, int)>();

        public void Run(int maxDegree)
        {
            int number = 2;
            int result = 1;
            int stepCount = 0;
            for (int i = 0; i < maxDegree; i++)
            {
                result *= number;
                stepCount++;
            }
            Steps.Add((maxDegree, stepCount));
        }
    }
}
