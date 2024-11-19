#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Microservices.Logistics/Microservices.Logistics.csproj", "Microservices.Logistics/"]
COPY ["Microservices.Logistics.Domain/Microservices.Logistics.Domain.csproj", "Microservices.Logistics.Domain/"]
COPY ["Microservices.Logistics.Public/Microservices.Logistics.Public.csproj", "Microservices.Logistics.Public/"]
RUN dotnet restore "./Microservices.Logistics/Microservices.Logistics.csproj"
COPY . .
WORKDIR "/src/Microservices.Logistics"
RUN dotnet build "./Microservices.Logistics.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Microservices.Logistics.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./Microservices.Logistics"]