using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class CalcEngine
    {
        public double calculate(double a, double b, MainPage.Operations op)
        {
            switch(op)
            {
                case MainPage.Operations.Plus:
                    return a + b;
                case MainPage.Operations.Minus:
                    return a - b;
                case MainPage.Operations.Mult:
                    return a * b;
                case MainPage.Operations.Devision:
                    if (b != 0.0) return a / b;
                    break;
            }

            return 0.0;
        }
    }
}
