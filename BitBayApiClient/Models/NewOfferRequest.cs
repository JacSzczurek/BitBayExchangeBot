using Newtonsoft.Json.Converters;

namespace BitBayApiClient.Models
{
    public class NewOfferRequest
    {
        public decimal Amount { get; set; }
        public decimal? Price { get; set; }
        public decimal Rate { get; set; }
        public bool PostOnly { get; set; }
        public bool FillOrKill { get; set; }
        public string OcoValue { get; set; }
        public string Mode { get; set; }
        public string OfferType { get; set; }
    }
}
