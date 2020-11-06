using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace Otus.SignalRFun.Hubs
{
  public class StockListenerService : BackgroundService
  {
    private readonly IHubContext<GreeterHub, IGreeterHubClient> _hubContext;

    public StockListenerService(IHubContext<GreeterHub, IGreeterHubClient> hubContext)
    {
      _hubContext = hubContext;
    }

    public async Task OnStockUpdated(string stock)
    {
      await _hubContext.Clients.Group(stock).ReceiveUpdate(stock);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        await OnStockUpdated("MSFT");
        await Task.Delay(1000, stoppingToken);
      }
    }
  }
}