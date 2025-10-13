using System;

namespace MyApplication.IntegrationMethods
{
    public interface IIntegrationMethod
    {
        double Integrate(Func<double, double> f, double a, double b, int n);
    }
}