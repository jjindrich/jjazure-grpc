using System;
using System.Text;
using System.Threading.Tasks;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace TestGrpcWithDapr.Services
{
    public class DaprImpl : Dapr.Client.Autogen.Grpc.v1.Dapr.DaprBase
    {
        public override Task<InvokeResponse> InvokeService(InvokeServiceRequest request, ServerCallContext context)
        {
            switch (request.Message.Method)
            {
                case "SayHello":

                    Console.WriteLine("Inside DaprImpl! InvokeService");
                    var response = new InvokeResponse()
                    {
                        Data = new Any()
                        {
                            Value = ByteString.CopyFrom(
                                Encoding.UTF8.GetBytes($"Hello from {request.Message?.Data?.Value?.ToStringUtf8()}"))
                        }
                    };
                    
                    return Task.FromResult(response);

                default:
                   throw new Exception($"Unknown method invocation {request.Message.Method}");
            }
        }


        public override async Task<Empty> InvokeBinding(InvokeBindingEnvelope request, ServerCallContext context)
        {
            await base.InvokeBinding(request, context);

            return new Empty();
        }

        public override async Task<Empty> PublishEvent(PublishEventEnvelope request, ServerCallContext context)
        {
            await base.PublishEvent(request, context);

            return new Empty();
        }

        public override async Task<GetStateResponseEnvelope> GetState(GetStateEnvelope request, ServerCallContext context)
        {
            await base.GetState(request, context);

            return new GetStateResponseEnvelope();
        }

        public override async  Task<Empty> SaveState(SaveStateEnvelope request, ServerCallContext context)
        {
            await base.SaveState(request, context);

            return new Empty();
        }

        public override async Task<Empty> DeleteState(DeleteStateEnvelope request, ServerCallContext context)
        {
            await base.DeleteState(request, context);

            return new Empty();
        }

        public override async Task<GetSecretResponseEnvelope> GetSecret(GetSecretEnvelope request, ServerCallContext context)
        {
            await base.GetSecret(request, context);

            return new GetSecretResponseEnvelope();
        }
    }
}
