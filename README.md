# JJAzure gRCP service running on Azure
Playground for gRPC in Azure

About gRPC - https://grpc.io/about/

## Create gRPC server

I will create new server running service called JJTaskManager.

Create new dotNet Core project and follow this steps:
- add nuget Grpc, Google.Protobuf
- add nuget Grpc.Tools to compile proto file (set Build action to Protobuf compiler)
- define gRPC service and messages (jjtask.proto)
- add code to run server (program.cs)