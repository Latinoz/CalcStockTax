namespace GetStockSRV.Models
{    
    public class OutJSON
    {
        public Marketdata marketdata { get; set; }
    }

    public class Marketdata
    {
        public string[] columns { get; set; }

        //public List<ValuesStock> data { get; set; }

        public object[][] data { get; set; }
    }

    public class ListStocks
    {
        public string? NameStock { get; set; }
        
        public string? ValueStock { get; set; }

    }
    
}
