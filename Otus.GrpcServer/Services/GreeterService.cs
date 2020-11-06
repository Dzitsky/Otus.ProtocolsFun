using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Otus.GrpcServer.Services
{
  public class GreeterService : Greeter.GreeterBase
  {
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
      _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
      var helloReply = new HelloReply
      {
        Message = "Hello " + request.Name
      };

      return Task.FromResult(helloReply);
    }

    public override async Task BidiHello(IAsyncStreamReader<HelloRequest> requestStream,
      IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
    {
      await foreach (var request in requestStream.ReadAllAsync())
      {
        var helloReply = new HelloReply
        {
          Message = "Hello " + request.Name
        };

        await responseStream.WriteAsync(helloReply);
      }
    }
  }
}