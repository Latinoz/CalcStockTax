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

        public string Index()
        {
            return "Hello DB";
        }


        [HttpGet]
        public async Task<ActionResult<List<Investment>>> GetInvestment()
        {
            List<Investment> result = db.Investments.ToList();
            
            return result;
        }
    }
}
