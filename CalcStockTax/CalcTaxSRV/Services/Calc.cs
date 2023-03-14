using CalcTaxSRV.Models;
using System;
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
            List<ListStocks> list = JsonSerializer.Deserialize<List<ListStocks>>(message);

            result = CalculateTax(list);

            return result;

        }

        public string CalculateTax(List<ListStocks> list)
        {
            string result = null;

            //0) Получить сумму комисии брокера (Инвестор)
            GetBankFee();            

            //1) Получить список купленных акций с их стоимостью и датой покупки из МосБиржи

            //2) Посчитать их общую сумму

            //2.1) Получить сумму комисиии брокера
            //   (сумма всех купленных акций из МосБиржи - комиссия брокера)

            //3) Получить общую сумму стоимости купленных акций из БД

            //4) Получить сумму комисиии брокера
            //   (сумма всех купленных акций - комиссия брокера)

            //5) 

            return result;

        }

        public void GetInvestmentFromDB()
        {

        }

        public async Task<string> GetBankFee()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string _request = "https://localhost:7163/api/Db/GetBankFee";

                string linehttp = _request;

                HttpResponseMessage responseHttp = await client.GetAsync(linehttp);
                responseHttp.EnsureSuccessStatusCode();
                string responseBody = await responseHttp.Content.ReadAsStringAsync();

                BankFee? response = JsonSerializer.Deserialize<BankFee>(responseBody);

                

                return null;
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
