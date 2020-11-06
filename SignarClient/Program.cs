using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignarClient
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var connection = new HubConnectionBuilder()
        .WithUrl("https://localhost:5001/hubs/greeter")
        .WithAutomaticReconnect()
        .Build();

      await connection.StartAsync();

      connection.On("ReceiveUpdate", (string stockName) =>
      {
        // show alert to the user
        Console.WriteLine($"Received update {stockName}");
      });

      await connection.InvokeAsync<string>("SubscribeOnStock", "MSFT");


      Console.ReadLine();
      await connection.StopAsync();
    }
  }
}