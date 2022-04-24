using SignalRStockQuote.Shared;

namespace SignalRStockQuote.Server.Services
{
    public interface IQuotePump
    {
        event EventHandler<StockQuote> QuoteUpdate;
        Task RunAsync(CancellationToken stopToken);
    }
}
