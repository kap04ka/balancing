using balancing.Models;

namespace balancing.Services
{
    public interface ICalculatorService
    {
        public ResultData Calculate(AppDbContext context);
    }
}
