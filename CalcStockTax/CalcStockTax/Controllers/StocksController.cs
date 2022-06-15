using GetStockSRV.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace GetStockSRV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<ActionResult<StockJSON>> Get()
        {
            string sber = "SBER";
            string amd = "AMD";
            string microsoft = "MSFT";
            string apple = "AAPL";

            DateTime from = new DateTime(2022,05,25);
            DateTime timeNow = DateTime.Now;            
            string pattern = "yyyy-MM-dd";

            int interval = 10;

            bool reverse = true;

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {   
                string linehttp = string.Format("http://iss.moex.com/iss/engines/stock/markets/shares/securities/{0}/candles.json?from={1}&till={1}&interval={2}&iss.reverse={3}", sber, from.ToString(pattern), interval.ToString(),reverse.ToString());

                HttpResponseMessage responseHttp = await client.GetAsync(linehttp);
                responseHttp.EnsureSuccessStatusCode();
                string responseBody = await responseHttp.Content.ReadAsStringAsync();

                StockJSON? response = JsonSerializer.Deserialize<StockJSON>(responseBody);

                return Ok(response);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);

                return BadRequest(e.Message);
            }
            
        }
    }
}
