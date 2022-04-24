using SignalRStockQuote.Shared;

namespace SignalRStockQuote.Server.Services
{
    public interface IQuoteHub
    {
        Task SendQuoteInfo(StockQuote quote);
        Task QuoteHubMessage(string msg);
    }
}
