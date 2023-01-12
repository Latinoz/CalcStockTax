namespace DbSRV.Models
{
    public class Investment
    {
        public int InvestmentId { get; set; }
        public string StockName { get; set; }
        public int BuyPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime BuyDate { get; set; }
        public int CurrentPrice { get; set; }

    }
}
