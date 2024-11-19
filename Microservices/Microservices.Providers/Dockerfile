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
COPY ["Microservices.Providers/Microservices.Providers.csproj", "Microservices.Providers/"]
COPY ["Microservices.Providers.Domain/Microservices.Providers.Domain.csproj", "Microservices.Providers.Domain/"]
COPY ["Microservices.Providers.Public/Microservices.Providers.Public.csproj", "Microservices.Providers.Public/"]
RUN dotnet restore "./Microservices.Providers/Microservices.Providers.csproj"
COPY . .
WORKDIR "/src/Microservices.Providers"
RUN dotnet build "./Microservices.Providers.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Microservices.Providers.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./Microservices.Providers"]