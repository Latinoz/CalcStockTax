﻿using GetStockSRV.Models;
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
        public async Task<ActionResult<List<ListStocks>>> Get()
        {
            string sber = "SBER";  //TQBR
            string yandex = "YNDX";  //TQBR

            string amd = "AMD";  //FQBR
            string microsoft = "MSFT";  //FQBR
            string apple = "AAPL";  //FQBR

            string vtb_indx = "VTBX";  //TQTF


            List<string> listTQBR = new List<string>();
            listTQBR.Add(sber);
            listTQBR.Add(yandex);

            List<string> listFQBR = new List<string>();
            listFQBR.Add(amd);
            listFQBR.Add(microsoft);
            listFQBR.Add(apple);

            List<string> listTQTF = new List<string>();
            listTQTF.Add(apple);

            string request = "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json?iss.meta=off&iss.only=marketdata&marketdata.columns=SECID,LAST";

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string linehttp = request;

                HttpResponseMessage responseHttp = await client.GetAsync(linehttp);
                responseHttp.EnsureSuccessStatusCode();
                string responseBody = await responseHttp.Content.ReadAsStringAsync();

                OutJSON? response = JsonSerializer.Deserialize<OutJSON>(responseBody);
                
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

                    foreach (var item in listTQBR)
                    {
                    
                    q = list.Where(x => x.NameStock == item).ToList();
                    
                    if(q.Count == 0)
                    {
                        continue;
                    }

                    resultList.Add(new ListStocks() { NameStock = q.FirstOrDefault().NameStock, ValueStock = q.FirstOrDefault().ValueStock });
                    }

                return Ok(resultList);
            }
            
            catch (HttpRequestException e)            
            {
                //Здесь сделать логирование
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);

                return BadRequest(e.Message);                
            }

        }
    }
}
