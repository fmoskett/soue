using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CurrencyScraper.Domain.Entities;
using CurrencyScraper.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CurrencyScraper.Infrastructure.Services
{
    public class CurrencyScraperService : ICurrencyScraperService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CurrencyScraperService> _logger;

        public CurrencyScraperService(HttpClient httpClient, ILogger<CurrencyScraperService> logger )
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Currency>> ScrapeCurrenciesAsync( )
        {
            _logger.LogInformation("Consultando API de economia...");
            
            var response = await _httpClient.GetAsync("https://economia.awesomeapi.com.br/last/USD-BRL,EUR-BRL,BTC-BRL" );
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;

            var result = new List<Currency>();
            foreach (var property in root.EnumerateObject())
            {
                var data = property.Value;
                result.Add(new Currency
                {
                    Id = Guid.NewGuid(),
                    Code = data.GetProperty("code").GetString() ?? "",
                    Name = data.GetProperty("name").GetString() ?? "",
                    Bid = decimal.Parse(data.GetProperty("bid").GetString() ?? "0", System.Globalization.CultureInfo.InvariantCulture),
                    Ask = decimal.Parse(data.GetProperty("ask").GetString() ?? "0", System.Globalization.CultureInfo.InvariantCulture),
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(data.GetProperty("timestamp").GetString() ?? "0")).DateTime
                });
            }
            return result;
        }
    }
}
