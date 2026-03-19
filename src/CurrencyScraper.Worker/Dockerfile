FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/CurrencyScraper.Worker/CurrencyScraper.Worker.csproj", "src/CurrencyScraper.Worker/"]
COPY ["src/CurrencyScraper.Infrastructure/CurrencyScraper.Infrastructure.csproj", "src/CurrencyScraper.Infrastructure/"]
COPY ["src/CurrencyScraper.Application/CurrencyScraper.Application.csproj", "src/CurrencyScraper.Application/"]
COPY ["src/CurrencyScraper.Domain/CurrencyScraper.Domain.csproj", "src/CurrencyScraper.Domain/"]
RUN dotnet restore "src/CurrencyScraper.Worker/CurrencyScraper.Worker.csproj"
COPY . .
WORKDIR "/src/src/CurrencyScraper.Worker"
RUN dotnet build "CurrencyScraper.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrencyScraper.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyScraper.Worker.dll"]
