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

        public DaprClientImpl(ILogger<DaprClientImpl> logger)
        {
            _logger = logger;
        }

        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            switch (request.Method)
            {
                case "SayHello":
                    var requestString = request.Data.Value?.ToStringUtf8();
                    var requestHelloRequest = System.Text.Json.JsonSerializer.Deserialize<HelloRequest>(requestString);
                    var msg = new HelloReply {Message = $"Hello, {requestHelloRequest.Name}"};
                    var serMsg = new JsonFormatter(new JsonFormatter.Settings(false)).Format(msg);
                    var any = new Any
                    {
                        TypeUrl = HelloReply.Descriptor.FullName,
                        Value = ByteString.CopyFrom(serMsg, Encoding.UTF8)
                    };
                    #region Console Debug
                    Console.WriteLine($"Received request: {requestHelloRequest}");
                    Console.WriteLine($"Serialized response data: {serMsg}");
                    Console.WriteLine($"Deserialized response data with .Net Core serializer: {System.Text.Json.JsonSerializer.Deserialize<HelloReply>(serMsg).Message}");
                    Console.WriteLine($"Deserialized response data with Newtonsort serializer: {JsonConvert.DeserializeObject<HelloReply>(serMsg).Message}");
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
