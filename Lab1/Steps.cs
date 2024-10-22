using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Steps
    {
        public Steps(int stepNumber, int degree)
        {
            StepNumber = stepNumber;
            Degree = degree;
        }

        public int StepNumber { get; set; }
        public int Degree { get; set; }
    }
}
