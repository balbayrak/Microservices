#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/StateMachine/SagaStateMachineWorkerService/SagaStateMachineWorkerService.csproj", "src/Services/StateMachine/SagaStateMachineWorkerService/"]
COPY ["src/Services/Stock/Api/Infrastructure/Microservices.Stock.IntegrationEvent/Microservices.Stock.IntegrationEvents.csproj", "src/Services/Stock/Api/Infrastructure/Microservices.Stock.IntegrationEvent/"]
COPY ["src/Core/Integration/Microservices.Integration/Microservices.Integration.csproj", "src/Core/Integration/Microservices.Integration/"]
COPY ["src/Core/Application/Microservices.Application/Microservices.Application.csproj", "src/Core/Application/Microservices.Application/"]
COPY ["src/Core/Domain/Microservices.Domain/Microservices.Domain.csproj", "src/Core/Domain/Microservices.Domain/"]
COPY ["src/Core/Persistence/Microservices.Persistence.Repository.EfCore/Microservices.Persistence.Repository.EfCore.csproj", "src/Core/Persistence/Microservices.Persistence.Repository.EfCore/"]
COPY ["src/Core/Persistence/Microservices.Persistence.Repository/Microservices.Persistence.Repository.csproj", "src/Core/Persistence/Microservices.Persistence.Repository/"]
COPY ["src/Services/Payment/Api/Infrastructure/Microservices.Payment.IntegrationEvents/Microservices.Payment.IntegrationEvents.csproj", "src/Services/Payment/Api/Infrastructure/Microservices.Payment.IntegrationEvents/"]
COPY ["src/Services/Order/Api/Infrastructure/Microservices.Order.IntegrationEvents/Microservices.Order.IntegrationEvents.csproj", "src/Services/Order/Api/Infrastructure/Microservices.Order.IntegrationEvents/"]
RUN dotnet restore "src/Services/StateMachine/SagaStateMachineWorkerService/SagaStateMachineWorkerService.csproj"
COPY . .
WORKDIR "/src/src/Services/StateMachine/SagaStateMachineWorkerService"
RUN dotnet build "SagaStateMachineWorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SagaStateMachineWorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SagaStateMachineWorkerService.dll"]