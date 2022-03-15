namespace BitBayApiClient.Models
{
    public class ActiveOffer
    {
        public string Market { get; set; }
        public string OfferType { get; set; }
        public string Id { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? lockedAmount { get; set; }
        public decimal? Rate { get; set; }
        public decimal? StartAmount { get; set; }
        public string Time { get; set; }
        public bool PostOnly { get; set; }
        public bool Hidden { get; set; }
        public string Mode { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public string FirstBalanceId { get; set; }
        public string SecondBalanceId { get; set; }
    }
}
