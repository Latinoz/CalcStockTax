using CalcTaxSRV.Models;
using System.Text.Json;

namespace CalcTaxSRV.Services
{
    public class Calc
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        public string GetStocksCalc(string message)
        {
            string result;

            //Здесь сделать проверку на null
            List<Stocks> list = JsonSerializer.Deserialize<List<Stocks>>(message);

            result = CalcTax(list);

            return result;

        }

        //Налог при выводе денег (Физ. Лицо, Резидент РФ)
        public string CalcTax(List<Stocks> list)
        {
            string result = null;

            List<Stocks> currentPrice = list;

            List<float> taxBase = new List<float>();

            //0) Получить сумму комисии брокера (Инвестор)
            float commission = GetBankFee().Result.FirstOrDefault(x => x.tariffId == 1).brokerFee;

            //1) Получить список купленных акций с их стоимостью и датой покупки из МосБиржи
            List<Investment> stocks = GetInvestmentFromDB().Result.ToList();

            //2) Посчитать разницу между ценой каждой купленной акции, и текущей ценой акции
            foreach(var stock in stocks)
            {
                //Получаем из списка stock первое значние из списка купленных акций
                //stock;

                //Находим текущее значение в списке currentPrice
                Stocks? selectable = currentPrice.Where(x => x.NameStock == stock.stockName).FirstOrDefault();

                //Вычитаем из цены купленной акции и текущей цены данной акции
                float intermediate = stock.buyPrice - (float)Convert.ToDouble(selectable.ValueStock);

                //3) Если разница(дельта) у акции отрицательная, то игнорировать, если положительная
                //то добавить и вычесть 13%
                if(intermediate > 0)
                {
                    //Вычесть комисию
                    float tax = intermediate - commission;

                    //Вычесть 13%

                    taxBase.Add(tax);
                }
            }

            //4) Суммировать налог           
            

            return result;

        }

        //Налог с дивидендов (Физ. Лицо, Резидент РФ)
        public string CalcDividendsTax(List<Stocks> list)
        {
            string result = null;     
            
            ////

            return result;

        }

        public async Task<Investment[]> GetInvestmentFromDB()
        {
            Investment[] result = null;

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string _request = "https://localhost:7163/api/Db/GetInvestment";

                string linehttp = _request;

                HttpResponseMessage responseHttp = await client.GetAsync(linehttp);
                responseHttp.EnsureSuccessStatusCode();
                string responseBody = await responseHttp.Content.ReadAsStringAsync();

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

        public async Task<ModelTariff[]> GetBankFee()
        {
            ModelTariff[] result = null;

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string _request = "https://localhost:7163/api/Db/GetBankFee";

                string linehttp = _request;

                HttpResponseMessage responseHttp = await client.GetAsync(linehttp);
                responseHttp.EnsureSuccessStatusCode();
                string responseBody = await responseHttp.Content.ReadAsStringAsync();

                ModelTariff[]? response = JsonSerializer.Deserialize<ModelTariff[]>(responseBody);

                result = response;

                return result;
            }
            
            catch(Exception e)
            {
                //Здесь сделать логирование
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Method: GetBankFee(), Message :{0} ", e.Message);

                return null;
            }
            
        }
    }

}
