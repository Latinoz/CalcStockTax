using CalcTaxSRV.Models;
using System;
using System.Text.Json;

namespace CalcTaxSRV.Services
{
    public class Calc
    {
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

            //0) Получить сумму комисии брокера

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

        public void GetPortfolio()
        {

        }
    }

}
