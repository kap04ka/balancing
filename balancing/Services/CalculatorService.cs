using balancing.Migrations;
using balancing.Models;
using balancing.Calculators;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace balancing.Services
{
    public class CalculatorService : ICalculatorService
    {
        public ResultData Calculate(AppDbContext context)
        {
            ICalculatorQP calculatorQP = new CalculatorQP();
            ResultData resultData = new ResultData();
            var flowsList = context.Flows.ToList();

            List<string> names = new List<string>();
            List<double> startResults = new List<double>();

            int iterCount = 2000;

            double[] x0 = new double[flowsList.Count];
            double[] errors = new double[flowsList.Count];
            byte[] I = new byte[flowsList.Count];
            double[] lb = new double[flowsList.Count];
            double[] ub = new double[flowsList.Count];

            int i = 0;
            bool flag = true;
            int size = 0;
            foreach (var flow in flowsList.OrderBy(w => w.Name))
            {
                int.TryParse(string.Join("", flow.Name.Where(c => char.IsDigit(c))), out i);
                i--;

                names.Add(flow.Name);
                startResults.Add(flow.Value);

                // Заполнение данных, которые есть всегда
                x0[i] = flow.Value;
                errors[i] = flow.Tols;
                if (flow.IsUsed) I[i] = 1;

                if (flow.LowerBound == null || flow.UpperBound == null) flag = false;

                // Заполнение данных, которые могут быть null
                if (flag)
                {
                    lb[i] = (double)flow.LowerBound;
                    ub[i] = (double)flow.UpperBound;
                }

                // Узнаем размер матрицы AB

                if (flow.DestNode > size) size = (int)flow.DestNode;

            }

            double[,] Ab = new double[size, flowsList.Count + 1];
            // Заполнение Ab
            foreach (var flow in flowsList.OrderBy(w => w.Name))
            {
                int.TryParse(string.Join("", flow.Name.Where(c => char.IsDigit(c))), out i);
                i--;

                if (flow.SourceNode != null) Ab[(int)flow.SourceNode - 1, i] = -1;
                if (flow.DestNode != null) Ab[(int)flow.DestNode - 1, i] = 1;
            }

            List<double> results = calculatorQP.Calculate(flag, iterCount, Ab, x0, errors, I, lb, ub);

            resultData.Names = names;
            resultData.StartValues = startResults;
            resultData.ResultValues = results;

            return resultData;
        }
    }
}
