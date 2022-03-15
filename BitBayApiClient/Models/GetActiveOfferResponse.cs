using Newtonsoft.Json;

namespace BitBayApiClient.Models
{
    public class GetActiveOfferResponse : BaseResponse
    {
        [JsonProperty("items")]
        public List<ActiveOffer> ActiveOffers { get; set; }
    }
}
