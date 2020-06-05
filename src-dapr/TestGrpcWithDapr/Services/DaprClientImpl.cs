using System;
using System.Text;
using System.Threading.Tasks;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TestGrpcWithDapr.Services
{
    public class DaprClientImpl : DaprClient.DaprClientBase
    {
        private readonly ILogger<DaprClientImpl> _logger;
        private readonly GreeterService _greeterService;

        public DaprClientImpl(ILogger<DaprClientImpl> logger, GreeterService greeterService)
        {
            _logger = logger;
            _greeterService = greeterService;
        }

        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            switch (request.Method)
            {
                case "SayHello":
                    var requestString = request.Data.Value?.ToStringUtf8();
                    var requestHelloRequest = System.Text.Json.JsonSerializer.Deserialize<HelloRequest>(requestString);
                    var reply = await _greeterService.SayHello(requestHelloRequest, context);
                    var formattedReply = new JsonFormatter(new JsonFormatter.Settings(false)).Format(reply);
                    var any = new Any
                    {
                        TypeUrl = HelloReply.Descriptor.FullName,
                        Value = ByteString.CopyFrom(formattedReply, Encoding.UTF8)
                    };
                    #region Console Debug
                    Console.WriteLine($"Received request: {requestHelloRequest}");
                    Console.WriteLine($"reply from Greeter: {reply}");
                    #endregion
                    var response = new InvokeResponse()
                    {
                        Data = any
                    };

                    return response;
                default:
                    throw new Exception($"Unknown method invocation {request.Method}");
            }
        }

        public override async Task<GetBindingsSubscriptionsEnvelope> GetBindingsSubscriptions(Empty request,
            ServerCallContext context)
        {
            var binding = new GetBindingsSubscriptionsEnvelope();
            binding.Bindings.Add("test binding");

            return binding;

        }

        public override async Task<GetTopicSubscriptionsEnvelope> GetTopicSubscriptions(Empty request,
            ServerCallContext context)
        {
            var subscription = new GetTopicSubscriptionsEnvelope();
            subscription.Topics.Add("test topic");

            return subscription;
        }

        public override async Task<BindingResponseEnvelope> OnBindingEvent(BindingEventEnvelope request,
            ServerCallContext context)
        {
            var bindingResponse = new BindingResponseEnvelope();

            return bindingResponse;
        }

        public override async Task<Empty> OnTopicEvent(CloudEventEnvelope request, ServerCallContext context)
        {
            return new Empty();
        }
    }
}
