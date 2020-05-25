using System;
using System.Threading;
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

        public static ManualResetEvent Shutdown = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            const int Port = 80;

            Server server = new Server
            {
                Services = { JJTaskManager.BindService(new JJTaskManagerImpl()) },
                // issue with localhost in docker
                // https://stackoverflow.com/questions/52454840/c-sharp-grpc-client-not-able-to-connect-to-grpc-server-hosted-in-dockerfor-wind
                // Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                Ports = { new ServerPort("0.0.0.0", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("JJTaskManager service is listening on port " + Port);
            //Console.ReadKey();

            // run in Docker
            // https://stackoverflow.com/questions/45989148/keep-dotnet-core-grpc-server-running-as-a-console-application
            Shutdown.WaitOne();

            server.ShutdownAsync().Wait();
        }
    }
}
