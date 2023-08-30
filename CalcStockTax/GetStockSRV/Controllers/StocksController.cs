using GetStockSRV.Models;
using Microsoft.AspNetCore.Mvc;
using GetStockSRV.Services;

namespace GetStockSRV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {        
        private readonly IGetActionService _actionService;
        static bool count = true;

        public StocksController(IGetActionService actionService)        {
            
            _actionService = actionService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Stocks>>> Get()
        {
            List<Stocks> result = _actionService.DoGet().Result;

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Start()
        {
            List<Stocks> result = null;

            while (count == true)
            {
                result = _actionService.DoGet().Result;

                await Task.Delay(5000);
            }                       

            return Ok(result);
        }        

        [HttpPost("[action]")]
        public async Task<IActionResult> Stop()
        {         
            await Task.Run(() => {count = false;});
            
            return Ok();
        }
    }
}
