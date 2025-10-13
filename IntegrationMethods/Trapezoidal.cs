using System;

namespace MyApplication.IntegrationMethods
{
    public class Trapezoidal : IIntegrationMethod
    {
        public double Integrate(Func<double, double> f, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0.5 * (f(a) + f(b));
            for (int i = 1; i < n; i++)
            {
                sum += f(a + i * h);
            }
            return sum * h;
        }
    }
}