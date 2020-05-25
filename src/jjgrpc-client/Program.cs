using System;
using System.Linq;
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
            //url = "http://localhost";            

            // process args
            if (args.Count() > 0)
            {
                url = args[0].ToString();
            }

            if (!url.Contains("https://"))
                secure = false;

            if (!secure)
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            Console.WriteLine(url);
            Console.WriteLine(string.Format("encrypted: {0}", secure));

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
