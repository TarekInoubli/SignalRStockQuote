using SignalRStockQuote.Shared;

namespace SignalRStockQuote.Server.Services
{
    public class QuotePump : IQuotePump
    {
        public event EventHandler<StockQuote> QuoteUpdate;

        private List<StockQuote> stockQuotes = new();

        public QuotePump()
        {
            this.Init();
        }

        private void Init()
        {
            stockQuotes.Add(new StockQuote() { Symbol ="MSFT", Price =80, BasePrice =80, Volume =2000,Time =DateTime.Now});
            stockQuotes.Add(new StockQuote() { Symbol = "AAPL", Price = 100, BasePrice = 100, Volume = 3000, Time = DateTime.Now });
            stockQuotes.Add(new StockQuote() { Symbol = "TSLA", Price = 350, BasePrice = 350, Volume = 120, Time = DateTime.Now });
            stockQuotes.Add(new StockQuote() { Symbol = "NVDA", Price = 420, BasePrice = 420, Volume = 400, Time = DateTime.Now });
            stockQuotes.Add(new StockQuote() { Symbol = "GOOG", Price = 1640, BasePrice = 1640, Volume = 110, Time = DateTime.Now });
            stockQuotes.Add(new StockQuote() { Symbol = "AMZN", Price = 3200, BasePrice = 3200, Volume = 99, Time = DateTime.Now });
            stockQuotes.Add(new StockQuote() { Symbol = "710000", Price = 80, BasePrice = 34.845, Volume = 3990203, Time = DateTime.Now });
        }

        public async Task RunAsync(CancellationToken stopToken)
        {
            int i = 0;

            Random rndPrice = new Random();
            Random rndVol = new Random();
            Random rndIdx = new Random();

            while (!stopToken.IsCancellationRequested)
            {
                i = (int)(rndIdx.NextDouble() * this.stockQuotes.Count);
                if (i < this.stockQuotes.Count)
                {
                    var quote = this.stockQuotes[i];

                    var p = quote.Price * (((rndPrice.NextDouble() - 0.48d) / 1000d) + 1d);
                    quote.Price = Math.Round(p, 2);
                    quote.Volume = (int)(rndVol.NextDouble() * 3000d / p);
                    quote.Time = DateTime.Now;

                    this.QuoteUpdate?.Invoke(this, quote);
                }

                await Task.Delay(500);
            }
        }
    }
}
