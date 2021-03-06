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

## Create gRPC test client with Go lang

You can check examples https://github.com/grpc/grpc-go/tree/master/examples/helloworld

Install gRPC for Go

```powershell
go get -u google.golang.org/grpc
```

Generate client from .proto

```powershell
protoc -I jjtask/ jjtask/jjtask.proto --go_out=plugins=grpc:jjtask
```

Run code

```powershell
go run .\main.go
```

## Test gRPC server in Azure

### Run Docker locally

Be sure you are running gRpc server correctly, check this issue https://stackoverflow.com/questions/52454840/c-sharp-grpc-client-not-able-to-connect-to-grpc-server-hosted-in-dockerfor-wind

```ps
docker run -it -p 80:80 jjgrpcsserver:latest
```

Run test Go client

### Publish to Azure Container Registry

Publish to Azure Container Registry from Visual Studio.

```ps
docker tag jjgrpcsserver:latest jjcontainers.azurecr.io/jjgrpcsserver:latest
docker push jjcontainers.azurecr.io/jjgrpcsserver:latest
```

### Use Azure Web App for Containers to run gRPC server

Create new Azure Web App for Containers on Linux and use container from registry.

Deployment not working, getting this error from Web App
2019-04-15 14:07:59.204 ERROR - Container jjgrpc_0 for site jjgrpc has exited, failing site start
2019-04-15 14:07:59.211 ERROR - Container jjgrpc_0 didn't respond to HTTP pings on port: 80, failing site start. See container logs for debugging.

There is problem with checking Docker container is live, trying access HTTP on port 80.

### User Azure Container Instance to run gRPC server

Create new Container Instance from Azure Container Registry - click Run instance

![Azure Container Registry Run Instance](media/acr-instance.png)

Now run test client, first change connection to ip address of Container instance.

```ps
go run .\main.go
2019/04/17 12:49:06 JJTaskManager client created...
2019/04/17 12:49:06 Result: Muj task
```