# SayHello grpc example via DAPR

Sample taken from https://github.com/GennadiiSvichinskyi/test-grpc-dapr

---
This is a demo project for demonstating how to use grpc client-service collaboration via [DAPR](https://dapr.io/)
# Getting Started
---
## Prerequesites
To build this example you need [.NET CORE SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) installed on your computer and DAPR [runtime](https://github.com/dapr/dapr). You can run dapr [locally or in Kuberneties](https://github.com/dapr/docs/blob/master/getting-started/environment-setup.md)  

---
## Build
We recommend installing [Visual Studio 2019 v16.4](https://visualstudio.microsoft.com/vs/) or later which will set you up with all the .NET build tools and allow you to open the solution files.

---
## Run Server locally
Assuming that you have DAPR running open command line and go to the directory with TestGrpcWithDapr.csproj. Then run the command  
`dapr run --app-id testGrpcDaprService --app-port 5000 --protocol grpc --grpc-port 50001 --log-level debug dotnet run`

## Run Client locally
Open a second command line, go to the directory with TestGrpcWithDapr.Client.csproj and run the command  
`dapr run --app-id testClient --protocol grpc --log-level debug dotnet run testGrpcDaprService SayHello GrpcLover`

or running outside from dapr
- uncomment DaprClientBuilder to use endpoint http://localhost:50001

## Notes
1. Notice tht we have to set a protocol parameter to 'grpc' in order to tell DAPR that we will use GRPC communication.
2. When you run the client you should set parameters for it :   
    a. server name  (testGrpcDaprService in example)  
    b. method name  (SayHello in example)  
    c. message (GrpcLover in example)  
---

## Output
`Reply : Hello, GrpcLover`