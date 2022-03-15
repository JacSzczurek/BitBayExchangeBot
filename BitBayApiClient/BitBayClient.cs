using RestSharp;
using Newtonsoft.Json;
using BitBayApiClient.Models;
using Microsoft.Extensions.Configuration;

namespace BitBayApiClient
{
    public class BitBayClient : IBitBayClient, IDisposable
    {
        readonly IConfiguration _configuration;
        private readonly IBitBayApiHashGenerator _bitBayApiHashGenerator;
        readonly RestClient _client;
        public BitBayClient(IConfiguration configuration, IBitBayApiHashGenerator bitBayApiHashGenerator)
        {
            var options = new RestClientOptions("https://api.zonda.exchange/rest/");

            _client = new RestClient(options);
                            //.AddDefaultHeader(KnownHeaders.Accept, "application/json");
            _configuration = configuration;
            _bitBayApiHashGenerator = bitBayApiHashGenerator;
        }
        public async Task<TickerResponse> GetThicker(string currency)
        {
            var request = new RestRequest($"trading/ticker/{currency}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<TickerResponse>(response.Content);
        }

        public async Task<StatsResponse> GetStats(string currency)
        {
            var request = new RestRequest($"trading/stats/{currency}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<StatsResponse>(response.Content);
        }

        public async Task<BaseResponse> NewOffer(string currency, NewOfferRequest requestPayload)
        {
            var request = new RestRequest($"trading/offer/{currency}", Method.Post);
            request.AddJsonBody(requestPayload);
            request.RequestFormat = DataFormat.Json;
            SetPostHeaders(request, requestPayload);
            var response = await _client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<BaseResponse>(response.Content);
        }

        public async Task<GetActiveOfferResponse> GetActiveOffers(string currency)
        {
            var request = new RestRequest($"trading/offer/{currency}", Method.Get);
            SetPostHeaders(request);
            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<GetActiveOfferResponse>(response.Content);
        }

        private void SetPostHeaders(RestRequest request, object payload = null)
        {
            var timeStamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString();
            var hash = _bitBayApiHashGenerator.ComputeBitBayHash(payload, timeStamp);

            request.AddHeader("API-Key", _configuration["ApiKeyPublic"]);
            request.AddHeader("API-Hash", hash);
            request.AddHeader("operation-id", Guid.NewGuid());
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Request-Timestamp", timeStamp);
            request.AddHeader("Content-Type", "application/json");
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }       
    
    public interface IBitBayClient
    {
        Task<TickerResponse> GetThicker(string currency);
        Task<StatsResponse> GetStats(string currency);
        Task<BaseResponse> NewOffer(string currency, NewOfferRequest requestPayload);
        Task<GetActiveOfferResponse> GetActiveOffers(string currency);
    }
}