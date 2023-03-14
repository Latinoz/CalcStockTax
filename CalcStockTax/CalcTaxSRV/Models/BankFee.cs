namespace CalcTaxSRV.Models

{
        public class RootBankFee
        {
        public BankFee[] PropertyBankFee { get; set; }
        }

        public class BankFee
        {
            public int tariffId { get; set; }
            public string name { get; set; }
            public float brokerFee { get; set; }
            public int taxId { get; set; }
            public object[] taxs { get; set; }
        }

    
}
