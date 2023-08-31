using DbSRV.DB;
using DbSRV.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
              
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Investment>>> GetInvestment()
        {
            List<Investment> result = db.Investments.ToList();
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Tariff>>> GetBankTariffs()
        {
            List<Tariff> result = db.Tariffs.ToList();
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Tax>>> GetTaxs()
        {
            List<Tax> result = db.Taxs.ToList();
            return Ok(result);
        }
    }
}
