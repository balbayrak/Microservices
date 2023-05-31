#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Basket/Api/WebApi/Microservices.Basket.WebApi/Microservices.Basket.WebApi.csproj", "src/Basket/Api/WebApi/Microservices.Basket.WebApi/"]
COPY ["src/Core/WebApi/Microservices.WebApi/Microservices.WebApi.csproj", "src/Core/WebApi/Microservices.WebApi/"]
COPY ["src/Core/Application/Microservices.Application/Microservices.Application.csproj", "src/Core/Application/Microservices.Application/"]
COPY ["src/Core/Domain/Microservices.Domain/Microservices.Domain.csproj", "src/Core/Domain/Microservices.Domain/"]
COPY ["src/Basket/Api/Infrastructure/Microservices.Basket.Persistence/Microservices.Basket.Persistence.csproj", "src/Basket/Api/Infrastructure/Microservices.Basket.Persistence/"]
COPY ["src/Basket/Api/Core/Microservices.Basket.Application/Microservices.Basket.Application.csproj", "src/Basket/Api/Core/Microservices.Basket.Application/"]
RUN dotnet restore "src/Basket/Api/WebApi/Microservices.Basket.WebApi/Microservices.Basket.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Basket/Api/WebApi/Microservices.Basket.WebApi"
RUN dotnet build "Microservices.Basket.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Basket.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Basket.WebApi.dll"]