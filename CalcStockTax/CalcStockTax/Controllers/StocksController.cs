using GetStockSRV.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Linq;
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
        public async Task<List<ListStocks>> GetMskStockExch()
        {

            DateTime dateFrom = new DateTime(2022, 06, 15);
            DateTime timeNow = DateTime.Now;
            string pattern = "yyyy-MM-dd";

            int interval = 10;
            bool reverse = true;

            string sber = "SBER";
            string yandex = "YNDX";
            string amd = "AMD";
            string microsoft = "MSFT";
            string apple = "AAPL";

            List<string> listStk = new List<string>();
            listStk.Add(sber);
            listStk.Add(yandex);
            listStk.Add(amd);
            listStk.Add(microsoft);
            listStk.Add(apple);

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


                //Dictionary<string, int?> keyValuePairs = new Dictionary<string, int?>();

                
                object[][] responseObj = response.marketdata.data;

                List<ListStocks> list = new List<ListStocks>();

                List<ListStocks> resultList = new List<ListStocks>();


                foreach (object[]? item in responseObj) 
                {
                    List<object> listObj = item.ToList();

                    string? _name;

                    string? _value;

                    if (listObj.ElementAt(0) == null)
                    {
                        _name = null;
                    }
                    else
                    {
                        _name = listObj.ElementAt(0).ToString();
                    }


                    if (listObj.ElementAt(1) == null)
                    {
                        _value = null;
                    }
                    else
                    {
                        _value = listObj.ElementAt(1).ToString();
                    }
                    

                    list.Add(new ListStocks() { NameStock = _name, ValueStock = _value });                        

                }

                List<ListStocks>? q = new List<ListStocks>();

                foreach (var item in listStk)
                {
                    
                    q = list.Where(x => x.NameStock == item).ToList();
                    
                    if(q.Count == 0)
                    {
                        continue;
                    }

                    resultList.Add(new ListStocks() { NameStock = q.FirstOrDefault().NameStock, ValueStock = q.FirstOrDefault().ValueStock });
                }

                //var p = list.Where(x => x.NameStock == sber).ToList();



                //return Ok(response);

                return resultList;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);

                //return BadRequest(e.Message);
                return null;
            }
            
        }
    }
}
