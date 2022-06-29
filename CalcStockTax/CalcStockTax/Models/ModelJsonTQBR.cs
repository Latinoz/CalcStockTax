namespace GetStockSRV.Models
{    
    public class ModelJsonTQBR
    {
        public Marketdata marketdata { get; set; }
    }

    public class Marketdata
    {
        public string[] columns { get; set; }        

        public object[][] data { get; set; }
    }

    
    
}
