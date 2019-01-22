# JJAzure gRCP service running on Azure
Playground for gRPC in Azure

## Create gRPC server

I will create new server running service called JJTaskManager.

Create new dotNet Core project and follow this steps:
- add nuget Grpc, Grpc.Tools, Google.Protobuf
- define gRPC service and messages (jjtask.proto)
- add code to run server