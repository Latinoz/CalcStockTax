namespace DbSRV.Models
{
    public class Tariff
    {
        public int TariffId { get; set; }
        public string Name { get; set; }
        public double BankFee { get; set; }
        public int TaxId { get; set; }

        public List<Tax> Taxs { get;set; } = new List<Tax>();   

    }
}
