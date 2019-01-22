using System;
using System.Threading.Tasks;
using Grpc.Core;
using JJTaskGrpcDemo;

namespace jjgrpcs_server
{
    class Program
    {

        class JJTaskManagerImpl : JJTaskManager.JJTaskManagerBase
        {
            public override Task<JJTaskDetail> AddTask(JJTask task, ServerCallContext context)
            {
                return Task.FromResult(new JJTaskDetail { Id = 100, Name = task.Name });
            }
        }

        static void Main(string[] args)
        {
            const int Port = 8080;

            Server server = new Server
            {
                Services = { JJTaskManager.BindService(new JJTaskManagerImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("JJTaskManager service is listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
