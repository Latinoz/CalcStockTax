namespace GetStockSRV.Models
{    
    public class OutJSON
    {
        public Marketdata marketdata { get; set; }
    }

    public class Marketdata
    {
        public string[] columns { get; set; }
        public object[][] data { get; set; }
    }

}
