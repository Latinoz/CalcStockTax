namespace DbSRV.Models
{
    public class Investment
    {
        public int InvestmentId { get; set; }
        public string StockName { get; set; }
        public double BuyPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime BuyDate { get; set; }
        public double CurrentPrice { get; set; }

    }
}
