#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["../../src/Product/Api/WebApi/Microservices.Product.WebApi/Microservices.Product.WebApi.csproj", "Product/Api/WebApi/Microservices.Product.WebApi/"]
COPY ["../../src/Product/Api/Core/Microservices.Product.Application/Microservices.Product.Application.csproj", "Product/Api/Core/Microservices.Product.Application/"]
COPY ["../../src/Product/Api/Core/Microservices.Product.Domain/Microservices.Product.Domain.csproj", "Product/Api/Core/Microservices.Product.Domain/"]
COPY ["../../src/Product/Api/Infrastructure/Microservices.Product.Persistence/Microservices.Product.Persistence.csproj", "Product/Api/Infrastructure/Microservices.Product.Persistence/"]
RUN dotnet restore "../../src/Product/Api/WebApi/Microservices.Product.WebApi/Microservices.Product.WebApi.csproj"
COPY . .
WORKDIR "/src/Product/Api/WebApi/Microservices.Product.WebApi"
RUN dotnet build "Microservices.Product.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Product.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Product.WebApi.dll"]