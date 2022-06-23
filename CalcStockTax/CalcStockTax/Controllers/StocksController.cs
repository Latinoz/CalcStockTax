using GetStockSRV.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        public async Task<ActionResult<OutJSON>> GetMskStockExch()
        {

            DateTime dateFrom = new DateTime(2022, 06, 15);
            DateTime timeNow = DateTime.Now;
            string pattern = "yyyy-MM-dd";

            int interval = 10;
            bool reverse = true;

            string sber = "SBER";
            //string yandex = "YNDX";
            //string amd = "AMD";
            //string microsoft = "MSFT";
            //string apple = "AAPL";           

            string request = string.Format("http://iss.moex.com/iss/engines/stock/markets/shares/securities/{0}/candles.json?from={1}&till={1}&interval={2}&iss.reverse={3}", sber, dateFrom.ToString(pattern), interval.ToString(), reverse.ToString());

            string req_test = "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.only=marketdata&marketdata.columns=SECID,LAST";

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //string linehttp = request;

                string linehttp = req_test;

                HttpResponseMessage responseHttp = await client.GetAsync(linehttp);
                responseHttp.EnsureSuccessStatusCode();
                string responseBody = await responseHttp.Content.ReadAsStringAsync();

                //StockJSON? response = JsonSerializer.Deserialize<StockJSON>(responseBody);                

                OutJSON? response = JsonSerializer.Deserialize<OutJSON>(responseBody);

                JObject o = JObject.Parse(responseBody);

                var SBER_value = (string)o["data"];

                return Ok(response);

                //return null;

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
