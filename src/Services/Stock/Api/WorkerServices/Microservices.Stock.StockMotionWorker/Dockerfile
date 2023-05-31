#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Stock/Api/WorkerServices/Microservices.Stock.StockMotionWorker/Microservices.Stock.StockMotionWorker.csproj", "src/Services/Stock/Api/WorkerServices/Microservices.Stock.StockMotionWorker/"]
RUN dotnet restore "src/Services/Stock/Api/WorkerServices/Microservices.Stock.StockMotionWorker/Microservices.Stock.StockMotionWorker.csproj"
COPY . .
WORKDIR "/src/src/Services/Stock/Api/WorkerServices/Microservices.Stock.StockMotionWorker"
RUN dotnet build "Microservices.Stock.StockMotionWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Stock.StockMotionWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Stock.StockMotionWorker.dll"]