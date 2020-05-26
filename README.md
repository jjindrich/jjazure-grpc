# JJAzure gRCP service running on Azure
Playground for gRPC in Azure

About gRPC - https://grpc.io/about/
Compare gRpc with HTTP Api - https://docs.microsoft.com/en-us/aspnet/core/grpc/comparison?view=aspnetcore-3.1

Introduction to gRPC on .NET Core 3.1 - https://docs.microsoft.com/en-us/aspnet/core/grpc

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

### Use Azure Container Instances

Deploy with Azure ACI with port 80. There is no scalling/balancing model.

Check service is running - OK - getting message.

### Use Service Fabric

Not supported Http/2 proxy - https://github.com/microsoft/service-fabric/issues/828

### Use Azure Kubernetes Service

Prepare for deployment and fill-in values, modify ingess template (without host)

```bash
helm create charts
```

Balancing problem with gRpc

- multiplexing of multiple HTTP/2 calls over a single TCP connection
- scale on client - bad way because of how to manage list of backends
- L4 load balancer scalling clients-server (selects one of backend servers)
   - not scalling server-server communication for long living connections
- L7 reverse proxy to enable scalling
  - linkerd (sidecar reverse proxy)
  - Nginx ingress (annotation grpc) or Traefik ingress 
  - AppGw not supported (http/2 backend not supported)

For L4 will deploy Azure Load balancer 

- check using [Azure Load Balancer as internal load balancer](https://docs.microsoft.com/en-us/azure/aks/internal-lb)
- change Service to type: LoadBalancer. 

For L7 will deploy [Traefik](https://docs.traefik.io/v1.7/user-guide/kubernetes/)) as Ingress

```bash
helm install traefik stable/traefik --set nodeSelector."kubernetes\.io/os"=linux
```

For L7 will deploy [Nginx](https://docs.microsoft.com/en-us/azure/aks/ingress-basic)

- [create certificate](https://docs.microsoft.com/en-us/azure/aks/ingress-own-tls) and import
- my common name jjaks.jjdev.local for internal ingress

```bash
openssl pkcs12 -in jjaks.pfx -nocerts -out jjaks.key
openssl pkcs12 -in jjaks.pfx -clcerts -nokeys -out jjaks.crt
openssl rsa -in jjaks.key -out jjaks-nopass.key

kubectl create secret tls aks-ingress-tls --key jjaks-nopass.key --cert jjaks.crt
```

Now deploy jjgrpc server to AKS with port 80

```bash
helm install jjgrpcserver charts
```

Check service is running on https://jjaks.jjdev.local - OK - getting message.


