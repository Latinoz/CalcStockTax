namespace DbSRV.Models
{
    public class Tax
    {
        public int TaxId { get; set; }
        public int TaxValue { get; set; }
        public int BankFee { get; set; }
        public DateTime Date { get; set; }
        public int SumTax { get; set; }
        public int TariffId { get; set; }

    }
}
