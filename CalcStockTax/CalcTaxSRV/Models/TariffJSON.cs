namespace CalcTaxSRV

{
    public class TariffJSON
    {
        public ModelTariff[] Property { get; set; }
    }

    public class ModelTariff
    {
        public int tariffId { get; set; }
        public string name { get; set; }
        public float brokerFee { get; set; }
        public int taxId { get; set; }
        public object[] taxs { get; set; }
    }
}

