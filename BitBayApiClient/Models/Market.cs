namespace BitBayApiClient.Models
{
    public class Market
    {
        public string Code { get; set; }
        public CurrencyThick First { get; set; }
        public CurrencyThick Second { get; set; }
    }
}
