using System;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr.Client;

namespace TestGrpcWithDapr.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string AppId = "testGrpcDaprService";
            string MethodName = "SayHello";
            string name = "Gennadii";
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            // run inside Dapr
            //var client = new DaprClientBuilder().UseJsonSerializationOptions(options).Build();
            
            // run separately
            var client = new DaprClientBuilder().UseEndpoint("http://localhost:50001").UseJsonSerializationOptions(options).Build();

            var anyReply = await client.InvokeMethodAsync<HelloRequest, HelloReply>(
                AppId, 
                MethodName, 
                new HelloRequest
                {
                    Name = name
                }
            );

            Console.WriteLine($"Reply : {anyReply.Message}");
        }
    }
}
