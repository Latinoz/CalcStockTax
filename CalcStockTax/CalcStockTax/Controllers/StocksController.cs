using CalcStockTax.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml;


namespace CalcStockTax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> Get()
        {
            IEnumerable<Stock> result;

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //HttpResponseMessage response = await client.GetAsync("https://iss.moex.com/iss/engines/stock/markets/foreignshares/boards/FQBR/securities.xml?iss.dp=comma&iss.meta=off&iss.only=securities");
                HttpResponseMessage response = await client.GetAsync("http://iss.moex.com/iss/engines/stock/markets/shares/securities/SBER/candles.json?from=2022-05-25&till=2022-05-25&interval=10&iss.reverse=true");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return result;
            
        }
    }
}
