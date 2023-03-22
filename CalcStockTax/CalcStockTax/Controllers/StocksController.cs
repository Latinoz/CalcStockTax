using GetStockSRV.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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


        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<ActionResult<List<Stocks>>> Get()
        {
            string sber = "SBER";  //TQBR
            string yandex = "YNDX";  //TQBR

            string amd = "AMD-RM";  //FQBR
            string microsoft = "MSFT-RM";  //FQBR
            string apple = "AAPL-RM";  //FQBR

            string vtb_indx = "VTBX";  //TQTF


            List<string> listTQBR = new List<string>();
            listTQBR.Add(sber);
            listTQBR.Add(yandex);

            List<string> listFQBR = new List<string>();
            listFQBR.Add(amd);
            listFQBR.Add(microsoft);
            listFQBR.Add(apple);

            List<string> listTQTF = new List<string>();
            listTQTF.Add(vtb_indx);

            string requestTQBR = "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.only=marketdata&marketdata.columns=SECID,LAST";

            string requestFQBR = "https://iss.moex.com/iss/engines/stock/markets/foreignshares/boards/FQBR/securities.json?iss.dp=comma&iss.meta=off&iss.only=securities&securities.columns=SECID,PREVWAPRICE";

            string requestTQTF = "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQTF/securities.json?iss.meta=off&iss.only=securities&securities.columns=SECID,PREVWAPRICE";


            List<Stocks> resultTQBR = await GetListStocksTQBRAsync(requestTQBR, listTQBR);

            List<Stocks> resultFQBR = await GetListStocksFQBRAsync(requestFQBR, listFQBR);

            List<Stocks> resultTQTF = await GetListStocksFQBRAsync(requestTQTF, listTQTF);

            List <Stocks> result = new List<Stocks>();
            result.AddRange(resultTQBR);
            result.AddRange(resultFQBR);
            result.AddRange(resultTQTF);

            //Нельзя отправлять null в параметр (добавить)
            //Здесь отправка в RabbitMQ            

            //DI
            _mqService.Send(result);

            return Ok(result);

        }

        private async Task<List<Stocks>> GetListStocksTQBRAsync(string _request, List<string> _listStk)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string linehttp = _request;

                HttpResponseMessage responseHttp = await client.GetAsync(linehttp);
                responseHttp.EnsureSuccessStatusCode();
                string responseBody = await responseHttp.Content.ReadAsStringAsync();

                ModelJsonTQBR? response = JsonSerializer.Deserialize<ModelJsonTQBR>(responseBody);

                object[][] responseObj = response.marketdata.data;

                List<Stocks> list = new List<Stocks>();
                
                List<Stocks> resultList = new List<Stocks>();

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


                    list.Add(new Stocks() { NameStock = _name, ValueStock = _value });

                }

                List<Stocks>? q = new List<Stocks>();

                foreach (var item in _listStk)
                {

                    q = list.Where(x => x.NameStock == item).ToList();

                    if (q.Count == 0)
                    {
                        continue;
                    }

                    resultList.Add(new Stocks() { NameStock = q.FirstOrDefault().NameStock, ValueStock = q.FirstOrDefault().ValueStock });
                }

                return resultList;
            }

            catch (HttpRequestException e)
            {
                //Здесь сделать логирование
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);

                return null;
            }
        }

        private async Task<List<Stocks>> GetListStocksFQBRAsync(string _request, List<string> _listStk)
        {
            

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string linehttp = _request;

                HttpResponseMessage responseHttp = await client.GetAsync(linehttp);
                responseHttp.EnsureSuccessStatusCode();
                string responseBody = await responseHttp.Content.ReadAsStringAsync();

                ModelJsonFQBR? response = JsonSerializer.Deserialize<ModelJsonFQBR>(responseBody);

                object[][] responseObj = response.securities.data;

                List<Stocks> list = new List<Stocks>();

                List<Stocks> resultList = new List<Stocks>();

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


                    list.Add(new Stocks() { NameStock = _name, ValueStock = _value });

                }

                List<Stocks>? q = new List<Stocks>();

                foreach (var item in _listStk)
                {

                    q = list.Where(x => x.NameStock == item).ToList();

                    if (q.Count == 0)
                    {
                        continue;
                    }

                    resultList.Add(new Stocks() { NameStock = q.FirstOrDefault().NameStock, ValueStock = q.FirstOrDefault().ValueStock });
                }

                return resultList;
            }

            catch (HttpRequestException e)
            {
                //Здесь сделать логирование
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);

                return null;
            }
        }

    }
}
