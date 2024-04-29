using balancing.Models;

namespace balancing.Calculators
{
    public interface ICalculatorQP
    {
        public List<double> Calculate(bool flag, int iterCount, double[,] Ab, double[] x0, double[] errors, byte[] I, double[] lb, double[] ub);
    }
}
