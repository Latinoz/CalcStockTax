namespace CalcStockTax.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string SecId { get; set; }
        public string ShortName { get; set; }
        public int PrevAdmittedQuote { get; set; }
        public DateTime PrevDate { get; set; }

    }
}
