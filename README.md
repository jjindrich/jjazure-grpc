# JJAzure gRCP service running on Azure
Playground for gRPC in Azure

About gRPC - https://grpc.io/about/

## Create gRPC server with dotNet Core

I will create new server running service called JJTaskManager.

Create new dotNet Core project and follow this steps:
- add nuget Grpc, Google.Protobuf
- add nuget Grpc.Tools to compile proto file (set Build action to Protobuf compiler with Server only option)
- define gRPC service and messages (jjtask.proto)
- add code to run server (program.cs)

## Create gRPC test client with dotNet Core

I will create test c# client to test server

Create new project in same way as server
- change proto compilation to Client only
- add code to call server (program.cs)

I make copy of proto file to be fully independent. If you want, you can share proto class in common library (change generator to Server and Client).