using System;

namespace MyApplication.IntegrationMethods
{
    public class MidpointRectangles : IIntegrationMethod
    {
        public double Integrate(Func<double, double> f, double a, double b, int n)
        {
            if (n <= 0) throw new ArgumentException("n должно быть > 0");
            double h = (b - a) / n;
            double sum = 0.0;

            for (int i = 0; i < n; i++)
            {
                double xMid = a + (i + 0.5) * h;
                double fx = f(xMid); // может выбросить исключение при недопустимом x
                sum += fx;
            }

            return sum * h;
        }
    }
}