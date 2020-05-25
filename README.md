# JJAzure gRCP service running on Azure
Playground for gRPC in Azure

About gRPC - https://grpc.io/about/

Introduction to gRPC on .NET Core 3.1 - https://docs.microsoft.com/en-us/aspnet/core/grpc

Compare gRpc with HTTP Api - https://docs.microsoft.com/en-us/aspnet/core/grpc/comparison?view=aspnetcore-3.1

*How to use with dotnet core 2, check [this](\src-dotnet2\readme.md)*

## Create gRPC server and client with dotNet Core

Follow this article https://docs.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-3.1&tabs=visual-studio

Server

- create new gRpc service project

Client

- create new console application
- add packages
- define Protos
- call service

## Test gRPC server in Azure

### Build, publish and run docker localy

```bash
docker run -it -p 80:80 jjgrpcserver:latest
docker tag jjgrpcserver:latest jjakscontainers.azurecr.io/jjgrpcserver:latest
docker push jjakscontainers.azurecr.io/jjgrpcserver:latest
``` 

About encrypted communication

- when running in Visual Studio localy, development SSL is used automatically
- start endpoint with custom certificate - https://docs.microsoft.com/en-us/aspnet/core/grpc/aspnetcore?view=aspnetcore-3.1&tabs=visual-studio#tls
- change code to allow unsecure communication (we have no certificate in docker) - https://docs.microsoft.com/en-US/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.1#call-insecure-grpc-services-with-net-core-client

### Use Azure Web App for Containers

Not supported because http.sys - https://docs.microsoft.com/en-us/aspnet/core/grpc/?view=aspnetcore-3.1

### User Azure Container Instances

Deploy with Azure ACI with port 80.

Check service is working - OK - getting message.

