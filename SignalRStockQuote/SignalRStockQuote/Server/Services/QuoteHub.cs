using Microsoft.AspNetCore.SignalR;
using SignalRStockQuote.Shared;

namespace SignalRStockQuote.Server.Services
{
    public class QuoteHub : Hub<IQuoteHub>
    {
        public override async Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            var msg = $"{Context.ConnectionId} joined the hub (Instance:{this.GetHashCode()})";
            await Clients.All.QuoteHubMessage(msg).ConfigureAwait(false);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            var msg = $"{Context.ConnectionId} left the hub (Instance:{this.GetHashCode()})";
            await Clients.All.QuoteHubMessage(msg).ConfigureAwait(false);
            await base.OnDisconnectedAsync(exception);
        }

        public Task SendQuoteInfo(StockQuote quote)
        {
            return Clients.All.SendQuoteInfo(quote);
        }

        public Task QuoteHubMessage(string msg)
        {
            return Clients.All.QuoteHubMessage(msg);
        }
    }

    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new();
    }
}
