namespace GetStockSRV.Models
{    
    public class ModelJsonFQBR
    {
        public Securities securities { get; set; }
    }

    public class Securities
    {
        public string[] columns { get; set; }        

        public object[][] data { get; set; }
    }    
    
}
