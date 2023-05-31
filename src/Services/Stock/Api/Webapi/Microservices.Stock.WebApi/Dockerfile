#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Stock/Api/Webapi/Microservices.Stock.WebApi/Microservices.Stock.WebApi.csproj", "src/Stock/Api/Webapi/Microservices.Stock.WebApi/"]
COPY ["src/Stock/Api/Infrastructure/Microservices.Stock.Persistence/Microservices.Stock.Persistence.csproj", "src/Stock/Api/Infrastructure/Microservices.Stock.Persistence/"]
COPY ["src/Core/Integration/Microservices.Integration/Microservices.Integration.csproj", "src/Core/Integration/Microservices.Integration/"]
COPY ["src/Core/Application/Microservices.Application/Microservices.Application.csproj", "src/Core/Application/Microservices.Application/"]
COPY ["src/Core/Domain/Microservices.Domain/Microservices.Domain.csproj", "src/Core/Domain/Microservices.Domain/"]
COPY ["src/Core/Persistence/Microservices.Persistence.Repository.EfCore/Microservices.Persistence.Repository.EfCore.csproj", "src/Core/Persistence/Microservices.Persistence.Repository.EfCore/"]
COPY ["src/Core/Persistence/Microservices.Persistence.Repository/Microservices.Persistence.Repository.csproj", "src/Core/Persistence/Microservices.Persistence.Repository/"]
COPY ["src/Stock/Api/Core/Microservices.Stock.Domain/Microservices.Stock.Domain.csproj", "src/Stock/Api/Core/Microservices.Stock.Domain/"]
COPY ["src/Product/Api/Infrastructure/Microservices.Product.Integration/Microservices.Product.Integration.csproj", "src/Product/Api/Infrastructure/Microservices.Product.Integration/"]
COPY ["src/Product/Api/Core/Microservices.Product.Application/Microservices.Product.Application.csproj", "src/Product/Api/Core/Microservices.Product.Application/"]
COPY ["src/Stock/Api/Infrastructure/Microservices.Stock.Integration/Microservices.Stock.Integration.csproj", "src/Stock/Api/Infrastructure/Microservices.Stock.Integration/"]
COPY ["src/Stock/Api/Core/Microservices.Stock.Application/Microservices.Stock.Application.csproj", "src/Stock/Api/Core/Microservices.Stock.Application/"]
COPY ["src/Core/EventSourcing/Microservices.EventSourcing/Microservices.EventSourcing.csproj", "src/Core/EventSourcing/Microservices.EventSourcing/"]
COPY ["src/Product/Api/Core/Microservices.Product.Domain/Microservices.Product.Domain.csproj", "src/Product/Api/Core/Microservices.Product.Domain/"]
COPY ["src/Core/WebApi/Microservices.WebApi/Microservices.WebApi.csproj", "src/Core/WebApi/Microservices.WebApi/"]
RUN dotnet restore "src/Stock/Api/Webapi/Microservices.Stock.WebApi/Microservices.Stock.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Stock/Api/Webapi/Microservices.Stock.WebApi"
RUN dotnet build "Microservices.Stock.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Stock.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Stock.WebApi.dll"]