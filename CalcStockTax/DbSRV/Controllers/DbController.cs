using DbSRV.DB;
using DbSRV.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbSRV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbController : Controller
    {
        ApplicationContext db;

        public DbController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public string Index()
        {
            return "Hello DB";
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<List<Investment>>> GetInvestment()
        {
            List<Investment> result = db.Investments.ToList();

            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Tariff>>> GetBankFee()
        {
            List<Tariff> result = db.Tariffs.ToList();
            return Ok(result);
        }
    }
}
