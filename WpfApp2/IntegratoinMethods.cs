using System;

namespace MyApplication.IntegrationMethods
{
    public interface IIntegrationMethod
    {
        /// <summary>
        /// Вычислить определённый интеграл функции f на [a, b] с n разбиениями.
        /// </summary>
        double Integrate(Func<double, double> f, double a, double b, int n);
    }
}
