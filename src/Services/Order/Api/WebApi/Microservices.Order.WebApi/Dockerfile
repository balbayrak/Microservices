#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Order/Api/WebApi/Microservices.Order.WebApi/Microservices.Order.WebApi.csproj", "src/Services/Order/Api/WebApi/Microservices.Order.WebApi/"]
COPY ["src/Core/WebApi/Microservices.WebApi/Microservices.WebApi.csproj", "src/Core/WebApi/Microservices.WebApi/"]
COPY ["src/Core/Application/Microservices.Application/Microservices.Application.csproj", "src/Core/Application/Microservices.Application/"]
COPY ["src/Core/Domain/Microservices.Domain/Microservices.Domain.csproj", "src/Core/Domain/Microservices.Domain/"]
COPY ["src/Services/Order/Api/Infrastructure/Microservices.Order.Persistence/Microservices.Order.Persistence.csproj", "src/Services/Order/Api/Infrastructure/Microservices.Order.Persistence/"]
COPY ["src/Services/Order/Api/Core/Microservices.Order.Domain/Microservices.Order.Domain.csproj", "src/Services/Order/Api/Core/Microservices.Order.Domain/"]
COPY ["src/Services/Order/Api/Core/Microservices.Order.Application/Microservices.Order.Application.csproj", "src/Services/Order/Api/Core/Microservices.Order.Application/"]
COPY ["src/Services/Order/Api/Infrastructure/Microservices.Order.IntegrationEvents/Microservices.Order.IntegrationEvents.csproj", "src/Services/Order/Api/Infrastructure/Microservices.Order.IntegrationEvents/"]
COPY ["src/Core/Integration/Microservices.Integration/Microservices.Integration.csproj", "src/Core/Integration/Microservices.Integration/"]
COPY ["src/Core/Persistence/Microservices.Persistence.Repository.EfCore/Microservices.Persistence.Repository.EfCore.csproj", "src/Core/Persistence/Microservices.Persistence.Repository.EfCore/"]
COPY ["src/Core/Persistence/Microservices.Persistence.Repository/Microservices.Persistence.Repository.csproj", "src/Core/Persistence/Microservices.Persistence.Repository/"]
RUN dotnet restore "src/Services/Order/Api/WebApi/Microservices.Order.WebApi/Microservices.Order.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Services/Order/Api/WebApi/Microservices.Order.WebApi"
RUN dotnet build "Microservices.Order.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Order.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Order.WebApi.dll"]