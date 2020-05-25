FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["jjgrpcs-server/jjgrpcs-server.csproj", "jjgrpcs-server/"]
RUN dotnet restore "jjgrpcs-server/jjgrpcs-server.csproj"
COPY . .
WORKDIR "/src/jjgrpcs-server"
RUN dotnet build "jjgrpcs-server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "jjgrpcs-server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "jjgrpcs-server.dll"]