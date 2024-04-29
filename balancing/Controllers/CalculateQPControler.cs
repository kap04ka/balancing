using balancing.Models;
using balancing.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace balancing.Controllers
{
    [Route("api/calculate")]
    [ApiController]
    public class CalculateQPControler : ControllerBase
    {
        private ICalculatorService _calculatorService;
        private readonly AppDbContext _context;

        public CalculateQPControler(ICalculatorService calcServ, AppDbContext context)
        {
            _calculatorService = calcServ;
            _context = context;

        }

        // POST api/integrals
        [EnableCors]
        [HttpGet]
        public ActionResult<ResultData> Post()
        {
            return _calculatorService.Calculate(_context);
        }

    }
}
