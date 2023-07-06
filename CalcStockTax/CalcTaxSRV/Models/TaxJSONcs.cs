namespace CalcTaxSRV
{
    public class TaxJSON
    {
        public ModelTax[] Property { get; set; }
    }

    public class ModelTax
    {
        public int taxId { get; set; }
        public int taxValue { get; set; }
        public DateTime date { get; set; }
        public int sumTax { get; set; }
        public int tariffId { get; set; }
        public object tariff { get; set; }
    }

}
