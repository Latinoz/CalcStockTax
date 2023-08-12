using GetStockSRV.Models;
using Microsoft.AspNetCore.Mvc;
using GetStockSRV.Services;

namespace GetStockSRV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IRabbitMQSrv _mqService;

        public StocksController(IRabbitMQSrv mqService)
        {
            _mqService = mqService;            
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Stocks>>> Get()
        {
            List<Stocks> result = new GetActionService(_mqService).DoGet().Result;

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Start()
        {            
            return Ok();
        }        

        [HttpPost("[action]")]
        public async Task<IActionResult> Stop()
        {
            return Ok();
        }
    }
}
