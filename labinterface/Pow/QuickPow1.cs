using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labinterface
{
    public class QuickPow1 : IPowAlgorithm
    {
        public List<(int, int)> Steps { get; private set; } = new List<(int, int)>();

        public void Run(int maxDegree)
        {
            int count = 0;
            int number = 2;
            long res = 1;
            int degree = maxDegree;

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
            Steps.Add((maxDegree, count));
        }

        public string GetName()
        {
            return "QuickPow1";
        }
    }
}
