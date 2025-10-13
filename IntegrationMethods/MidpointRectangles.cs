using System;

namespace MyApplication.IntegrationMethods
{
    public class MidpointRectangles : IIntegrationMethod
    {
        public double Integrate(Func<double, double> f, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                double xMid = a + (i + 0.5) * h;
                sum += f(xMid);
            }
            return sum * h;
        }
    }
}