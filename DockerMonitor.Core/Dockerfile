#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DockerMonitor.Core/DockerMonitor.Core.csproj", "DockerMonitor.Core/"]
COPY ["DockerMonitor.Services/DockerMonitor.Services.csproj", "DockerMonitor.Services/"]
COPY ["DockerMonitor.Services.Abstractions/DockerMonitor.Services.Abstractions.csproj", "DockerMonitor.Services.Abstractions/"]
COPY ["DockerMonitor.Domain/DockerMonitor.Domain.csproj", "DockerMonitor.Domain/"]
COPY ["DockerMonitor.Infastructure/DockerMonitor.Infastructure.csproj", "DockerMonitor.Infastructure/"]
RUN dotnet restore "DockerMonitor.Core/DockerMonitor.Core.csproj"
COPY . .
WORKDIR "/src/DockerMonitor.Core"
RUN dotnet build "DockerMonitor.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerMonitor.Core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerMonitor.Core.dll"]