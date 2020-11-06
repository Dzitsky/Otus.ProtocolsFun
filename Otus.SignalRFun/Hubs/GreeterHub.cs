using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Otus.SignalRFun.Hubs
{
  public class GreeterHub : Hub<IGreeterHubClient>
  {
    public async Task<string> SayHello(string name)
    {
      return $"Hello {name}";
    }

    public async Task SubscribeOnStock(string stockName)
    {
      // MSFT
      // ALPB

      await Groups.AddToGroupAsync(Context.ConnectionId, stockName);
    }
  }

  public interface IGreeterHubClient
  {
    Task SayHelloOnClient(string name);
    Task ReceiveUpdate(string stock);
  }
}