namespace GetStockSRV.Models
{
    public class Candles
    {         
        //public Dictionary<int,string> Columns { get; set; }
        public Metadata[] Data { get; set; }

    }

    public class Metadata
    {
        public double[] Open { get; set; }
        public double[] Close { get; set; }
        public double[] High { get; set; }
        public double[] Low { get; set; }
        public double[] Value { get; set; }
        public double[] Volume { get; set; }
        public DateTime[] Begin { get; set; }
        public DateTime[] End { get; set; }
    }

    public class Columns
    {
        public string[]? Name { get; set; }
    }
}
