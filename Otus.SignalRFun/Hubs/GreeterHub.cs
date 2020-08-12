using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Otus.SignalRFun.Hubs
{
  public class GreeterHub : Hub<IGreeterHubClient>
  {
    public async Task<string> SayHello(string name)
    {
      // await Clients.Group().SayHelloOnClient("Jon");

      // MSFT
      // APPL
      
      return $"Hello {name}";
    }

    public async Task Subscribe(string stockName)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, stockName);
    }
  }

  public class StockListener
  {
    private readonly IHubContext<GreeterHub, IGreeterHubClient> _hubContext;

    public StockListener(IHubContext<GreeterHub, IGreeterHubClient> hubContext)
    {
      _hubContext = hubContext;
    }

    public async Task OnStockUpdated(string stock)
    {
      await _hubContext.Clients.Group(stock).ReceiveUpdate(stock);
    }
  }

  public interface IGreeterHubClient
  {
    Task SayHelloOnClient(string name);
    Task ReceiveUpdate(string stock);
  }
}