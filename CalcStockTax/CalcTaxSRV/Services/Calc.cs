using CalcTaxSRV.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text.Json;

namespace CalcTaxSRV.Services
{
    public class Calc
    {        
        static readonly HttpClient client = new HttpClient();

        static Task<string> token;

        public Calc() 
        { 
            token = GetToken();
        }

        public string GetStocksCalc(string message)
        {
            string result;

            //Здесь сделать проверку на null
            List<Stocks> list = JsonSerializer.Deserialize<List<Stocks>>(message);

            result = CalcTax(list);

            return result;

        }

        //Налог при продаже акций (Физ. Лицо, Резидент РФ)
        public string CalcTax(List<Stocks> list)
        {
            string result;

            List<Stocks> currentPrice = list;

            List<float> taxBase = new List<float>();

            //0) Получить сумму комисии брокера (Сейчас только тариф - "Инвестор")
            float commission = GetBrokerFee().Result.FirstOrDefault(x => x.tariffId == 1).brokerFee;

            //ToDo Сделать подстановку процента, в зависимости от того, какая налоговая ставка у налогоплательщика (сейчас только 13%)
            int tax = GetTax().Result.FirstOrDefault(y => y.taxValue == 13).taxValue;

            //1) Получить список купленных акций с их стоимостью и датой покупки из МосБиржи
            List<Investment> stocks = GetInvestment().Result.ToList();

            //2) Посчитать разницу между ценой каждой купленной акции, и текущей ценой акции

            //Получаем из списка stock, первое значние из купленных акций
            foreach (var stock in stocks)
            {
                //Находим текущее значение в списке currentPrice
                Stocks? current = currentPrice.Where(x => x.NameStock == stock.stockName).FirstOrDefault();

                if(current != null && current.ValueStock != null)
                {
                    //Вычитаем из текущей цены акции, цену купленной акции 
                    float difference = float.Parse(current.ValueStock, CultureInfo.InvariantCulture.NumberFormat) - stock.buyPrice;
                    
                    //3) Если разница(дельта) у акции отрицательная, то игнорировать, если положительная
                    //то добавить и вычесть 13%
                    if (difference > 0)
                    {
                        //Вычесть комисию
                        float priceWithoutCommis = difference - commission;                        

                        //Вычесть %                        
                        float ndfl = (float)Math.Round((double)(priceWithoutCommis * tax / 100));

                        taxBase.Add(ndfl);
                    }                    
                }                
            }

            //4) Суммировать налог           
            result = taxBase.Sum().ToString();

            return result;

        }

        //ToDo Налог с дивидендов (Физ. Лицо, Резидент РФ)
        public async Task<ModelTax[]> GetTax()
        {
            ModelTax[] result;
            
            try
            {
                string _request = "https://localhost:7163/api/Db/GetTaxs";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Result);
                string? responseBody = await client.GetStringAsync(_request);

                ModelTax[]? response = JsonSerializer.Deserialize<ModelTax[]>(responseBody);

                result = response;

                return result;
            }

            catch (Exception e)
            {
                //ToDo Сделать логирование
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Method: GetTax(), Message :{0} ", e.Message);

                return null;
            }
        }

        public async Task<Investment[]> GetInvestment()
        {
            Investment[] result;               
            
            try
            {
                string _request = "https://localhost:7163/api/Db/GetInvestment";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Result);
                string? responseBody = await client.GetStringAsync(_request);                

                Investment[]? response = JsonSerializer.Deserialize<Investment[]> (responseBody);

                result = response;

                return result;
            }

            catch (Exception e)
            {
                //Здесь сделать логирование
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Method: GetInvestmentFromDB(), Message :{0} ", e.Message);

                return null;
            }
        }

        public async Task<ModelTariff[]> GetBrokerFee()
        {
            ModelTariff[] result;
            
            try
            {
                string _request = "https://localhost:7163/api/Db/GetBankTariffs";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Result);
                string? responseBody = await client.GetStringAsync(_request);

                ModelTariff[]? response = JsonSerializer.Deserialize<ModelTariff[]>(responseBody);

                result = response;

                return result;
            }
            
            catch(Exception e)
            {
                //ToDo Сделать логирование
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Method: GetBrokerFee(), Message :{0} ", e.Message);

                return null;
            }            
        }

        [HttpPost]
        public async Task<string> GetToken()
        {
            try
            {
                string _request = "https://localhost:7163/security/createToken";                

                Person login = new Person
                {
                    UserName = "joydip",
                    Password = "joydip123"
                };

                string? token;

                HttpResponseMessage response = await client.PostAsJsonAsync(_request, login);
                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    token = await JsonSerializer.DeserializeAsync<string>(contentStream);
                }                              

                return token;
            }

            catch (Exception e)
            {
                //ToDo Сделать логирование
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Method: GetToken(), Message :{0} ", e.Message);

                return null;
            }
        }
    }
}
