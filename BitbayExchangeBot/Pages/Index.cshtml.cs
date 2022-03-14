using BitBayApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BitbayExchangeBot.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBitBayClient _client;

        public IndexModel(ILogger<IndexModel> logger, IBitBayClient client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task OnGet()
        {
            await _client.GetStats("BTC-PLN");
        }
    }
}