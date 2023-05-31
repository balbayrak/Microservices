#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Payment/Api/WebApi/Microservices.Payment.WebApi/Microservices.Payment.WebApi.csproj", "src/Services/Payment/Api/WebApi/Microservices.Payment.WebApi/"]
COPY ["src/Core/WebApi/Microservices.WebApi/Microservices.WebApi.csproj", "src/Core/WebApi/Microservices.WebApi/"]
COPY ["src/Core/Application/Microservices.Application/Microservices.Application.csproj", "src/Core/Application/Microservices.Application/"]
COPY ["src/Core/Domain/Microservices.Domain/Microservices.Domain.csproj", "src/Core/Domain/Microservices.Domain/"]
COPY ["src/Services/Payment/Api/Infrastructure/Microservices.Payment.Integration/Microservices.Payment.Integration.csproj", "src/Services/Payment/Api/Infrastructure/Microservices.Payment.Integration/"]
COPY ["src/Core/Integration/Microservices.Integration/Microservices.Integration.csproj", "src/Core/Integration/Microservices.Integration/"]
COPY ["src/Services/Payment/Api/Infrastructure/Microservices.Payment.IntegrationEvents/Microservices.Payment.IntegrationEvents.csproj", "src/Services/Payment/Api/Infrastructure/Microservices.Payment.IntegrationEvents/"]
RUN dotnet restore "src/Services/Payment/Api/WebApi/Microservices.Payment.WebApi/Microservices.Payment.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Services/Payment/Api/WebApi/Microservices.Payment.WebApi"
RUN dotnet build "Microservices.Payment.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Payment.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Payment.WebApi.dll"]