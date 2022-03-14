using RestSharp;
using Newtonsoft.Json;
using BitBayApiClient.Models;
using Microsoft.Extensions.Configuration;

namespace BitBayApiClient
{
    public class BitBayClient : IBitBayClient, IDisposable
    {
        readonly IConfiguration _configuration;
        readonly RestClient _client;
        public BitBayClient(IConfiguration configuration)
        {
            var options = new RestClientOptions("https://api.zonda.exchange/rest/");

            _client = new RestClient(options)
                            .AddDefaultHeader(KnownHeaders.Accept, "application/json");
            _configuration = configuration;
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

        public async Task NewOffer(string currency)
        {
            var request = new RestRequest($"trading/offer/{currency}", Method.Post);
            SetPostHeaders(request);
            var response = await _client.ExecuteAsync(request);
        }
        private void SetPostHeaders(RestRequest request)
        {
            request.AddHeader("API-Key", _configuration["ApiKey"]);
            request.AddHeader("API-Hash", _configuration["ApiHash"]);
            request.AddHeader("operation-id", Guid.NewGuid());
            request.AddHeader("Request-Timestamp", ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds());
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
    }
}