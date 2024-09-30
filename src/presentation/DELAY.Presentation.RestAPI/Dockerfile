#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["src/presentation/DELAY.Presentation.RestAPI/DELAY.Presentation.RestAPI.csproj", "src/presentation/DELAY.Presentation.RestAPI/"]
COPY ["src/core/DELAY.Core.Application/DELAY.Core.Application.csproj", "src/core/DELAY.Core.Application/"]
COPY ["src/core/DELAY.Core.Domain/DELAY.Core.Domain.csproj", "src/core/DELAY.Core.Domain/"]
COPY ["src/infrastructure/DELAY.Infrastructure.Auth/DELAY.Infrastructure.Auth.csproj", "src/infrastructure/DELAY.Infrastructure.Auth/"]
COPY ["src/infrastructure/DELAY.Infrastructure/DELAY.Infrastructure.csproj", "src/infrastructure/DELAY.Infrastructure/"]
COPY ["src/infrastructure/DELAY.Infrastructure.Caching/DELAY.Infrastructure.Caching.csproj", "src/infrastructure/DELAY.Infrastructure.Caching/"]
COPY ["src/infrastructure/DELAY.Infrastructure.Persistence/DELAY.Infrastructure.Persistence.csproj", "src/infrastructure/DELAY.Infrastructure.Persistence/"]
RUN dotnet restore "./src/presentation/DELAY.Presentation.RestAPI/DELAY.Presentation.RestAPI.csproj"
COPY . .
WORKDIR "/src/src/presentation/DELAY.Presentation.RestAPI"
RUN dotnet build "./DELAY.Presentation.RestAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./DELAY.Presentation.RestAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["dotnet", "DELAY.Presentation.RestAPI.dll"]