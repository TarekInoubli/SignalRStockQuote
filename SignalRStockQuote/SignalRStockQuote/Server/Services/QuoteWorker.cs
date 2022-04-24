using Microsoft.AspNetCore.SignalR;

namespace SignalRStockQuote.Server.Services
{
    public class QuoteWorker : BackgroundService
    {
        private readonly ILogger<QuoteWorker> _logger;
        private readonly IHubContext<QuoteHub, IQuoteHub> _quoteHub;
        private readonly IQuotePump _pump;

        public QuoteWorker(ILogger<QuoteWorker> logger, IHubContext<QuoteHub, IQuoteHub> quoteHub, IQuotePump pump)
        {
            _logger = logger;
            _quoteHub = quoteHub;
            _pump = pump;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _pump.QuoteUpdate += _pump_QuoteUpdate;
            await _pump.RunAsync(stoppingToken);
            _pump.QuoteUpdate -= _pump_QuoteUpdate;
        }

        private async void _pump_QuoteUpdate(object? sender, Shared.StockQuote quote)
        {
            await _quoteHub.Clients.All.SendQuoteInfo(quote);
        }
    }
}
