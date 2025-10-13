using System;

namespace MyApplication.IntegrationMethods
{
    public class Trapezoidal : IIntegrationMethod
    {
        public double Integrate(Func<double, double> f, double a, double b, int n)
        {
            if (n <= 0) throw new ArgumentException("n должно быть > 0");
            double h = (b - a) / n;
            double sum = 0.0;

            // крайние точки
            sum += 0.5 * f(a);
            sum += 0.5 * f(b);

            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                sum += f(x); // может выбросить исключение при недопустимом x
            }

            return sum * h;
        }
    }
}