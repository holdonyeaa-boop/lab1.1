using System;

namespace MyApplication
{
    public static class Function
    {
        public static double SampleFunction(double x)
        {
            if (11.0 * x <= 0.0)
                throw new ArgumentException($"Значение x = {x} не принадлежит области определения функции (требуется 11*x > 0).");
            return 11.0 * x - Math.Log(11.0 * x) - 2.0;
        }
    }
}