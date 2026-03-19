FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/CurrencyScraper.Api/CurrencyScraper.Api.csproj", "src/CurrencyScraper.Api/"]
COPY ["src/CurrencyScraper.Infrastructure/CurrencyScraper.Infrastructure.csproj", "src/CurrencyScraper.Infrastructure/"]
COPY ["src/CurrencyScraper.Application/CurrencyScraper.Application.csproj", "src/CurrencyScraper.Application/"]
COPY ["src/CurrencyScraper.Domain/CurrencyScraper.Domain.csproj", "src/CurrencyScraper.Domain/"]
RUN dotnet restore "src/CurrencyScraper.Api/CurrencyScraper.Api.csproj"
COPY . .
WORKDIR "/src/src/CurrencyScraper.Api"
RUN dotnet build "CurrencyScraper.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrencyScraper.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyScraper.Api.dll"]
