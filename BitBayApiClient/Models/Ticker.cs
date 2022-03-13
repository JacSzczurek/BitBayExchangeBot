namespace BitBayApiClient.Models
{
    public class Ticker
    {
        public Market Market { get; set; }
        public string Time { get; set; }
        public decimal HighestBid { get; set; }
        public decimal LowestAsk { get; set; }
        public decimal Rate { get; set; }
        public decimal PreviousRate { get; set; }
    }
}
