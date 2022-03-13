using Newtonsoft.Json;

namespace BitBayApiClient.Models
{
    public class Stats
    {
        [JsonProperty("m")]
        public string MarketCode { get; set; }
        [JsonProperty("h")]
        public decimal HighestRate { get; set; }
        [JsonProperty("l")]
        public decimal LowestRate { get; set; }
        [JsonProperty("v")]
        public decimal Volume24h { get; set; }
        [JsonProperty("r24h")]
        public string OpeningRate24h { get; set; }
    }
}
