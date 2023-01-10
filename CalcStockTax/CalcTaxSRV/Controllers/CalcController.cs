using CalcTaxSRV.Services;
using CalcTaxSRV.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalcTaxSRV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcController : ControllerBase
    {

        [HttpGet]
        public ActionResult<ListStocks> Get()
        {
            

            List<ListStocks> result = new List<ListStocks>();

            return Ok(result);
        }

    }

}
