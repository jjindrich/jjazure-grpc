using Grpc.Core;
using System;
using JJTaskGrpcDemo;

namespace jjgrpc_client
{
    class Program
    {
        const int Port = 8080;
        
        static void Main(string[] args)
        {
            Channel channel = new Channel(string.Format("localhost:{0}",Port), ChannelCredentials.Insecure);
            var client = new JJTaskManager.JJTaskManagerClient(channel);
            Console.WriteLine("JJTaskManager client created...");

            var reply = client.AddTask(new JJTask() { Name = "Muj task" });

            Console.WriteLine(string.Format("Result: id {0} for task name {1}", reply.Id, reply.Name));

            channel.ShutdownAsync().Wait();

            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
        }
    }
}
