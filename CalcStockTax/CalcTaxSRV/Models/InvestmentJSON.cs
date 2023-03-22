namespace CalcTaxSRV.Models
{

    public class InvestmentJSON
    {
        public Investment[] Property { get; set; }
    }

    public class Investment
    {
        public int investmentId { get; set; }
        public string stockName { get; set; }
        public float buyPrice { get; set; }
        public int quantity { get; set; }
        public DateTime buyDate { get; set; }
        public int currentPrice { get; set; }
    }

}
