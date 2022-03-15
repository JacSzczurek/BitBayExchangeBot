using BitBayApiClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BitBayTests
{
    public class ApiClientTests
    {
        
        [Test]
        public async Task ApiClient_ticker_returns_values()
        {
            var client = GetClient();

            var result = await client.GetThicker("BTC-PLN");

            Assert.AreEqual(result.Status, "Ok");
        }

        [Test]
        public async Task ApiClient_getActiveOffers_returns_values()
        {
            var client = GetClient();

            var result = await client.GetActiveOffers("XRP-PLN");

            Assert.AreEqual(result.Status, "Ok");
        }

        [Test]
        public async Task ApiClient_newOffer_returns_values()
        {
            var client = GetClient();

            var payload = new BitBayApiClient.Models.NewOfferRequest
            {
                Amount = 5,
                Mode = "limit",
                OfferType = "SELL",
                Rate = 15,

            };
            var result = await client.NewOffer("XRP-PLN", payload);

            Assert.AreEqual(result.Status, "Fail");
        }


        private BitBayClient GetClient()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("authConfig.json", optional: true, reloadOnChange: true).Build();

            if (configuration["ApiKeyPublic"] == null || configuration["ApiKeyPrivate"] == null)
                throw new System.ArgumentNullException("authConfig.json not found");

            IBitBayApiHashGenerator apiHashGenerator = new BitBayApiHashGenerator(configuration["ApiKeyPublic"], configuration["ApiKeyPrivate"]);

            return new BitBayClient(configuration, apiHashGenerator);
        }
    }
}