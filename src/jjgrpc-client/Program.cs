using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using jjgrpc_server;

namespace jjgrpc_client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool secure = true;
            // The port number(5001) must match the port of the gRPC server.
            string url = "https://localhost:5001";

            // test docker localy
            secure = false;
            //url = "http://localhost";
            //url = "http://20.50.164.47";
            if (!secure)
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var channel = GrpcChannel.ForAddress(url);
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
