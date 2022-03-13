using RestSharp;
using Newtonsoft.Json;
using BitBayApiClient.Models;

namespace BitBayApiClient
{
    public class BitBayClient : IBitBayClient
    {
        public async Task<TickerResponse> GetThicker(string currency)
        {
            var client = new RestClient($"https://api.zonda.exchange/rest/trading/ticker/{currency}");
            var request = new RestRequest("", Method.Get);
            request.AddHeader("Accept", "application/json");
            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<TickerResponse>(response.Content);
        }

        public async Task<StatsResponse> GetStats(string currency)
        {
            var client = new RestClient($"https://api.zonda.exchange/rest/trading/stats/{currency}");
            var request = new RestRequest("", Method.Get);
            request.AddHeader("Accept", "application/json");
            var response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<StatsResponse>(response.Content);
        }

    }
    
    public interface IBitBayClient
    {
        Task<TickerResponse> GetThicker(string currency);
        Task<StatsResponse> GetStats(string currency);
    }
}