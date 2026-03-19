using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyScraper.Application.DTOs;
using CurrencyScraper.Application.Interfaces;
using CurrencyScraper.Domain.Entities;
using CurrencyScraper.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CurrencyScraper.Application.UseCases
{
    public class CurrencyUseCase : ICurrencyUseCase
    {
        private readonly ICurrencyRepository _repository;
        private readonly ICurrencyScraperService _scraperService;
        private readonly ILogger<CurrencyUseCase> _logger;

        public CurrencyUseCase(
            ICurrencyRepository repository,
            ICurrencyScraperService scraperService,
            ILogger<CurrencyUseCase> logger)
        {
            _repository = repository;
            _scraperService = scraperService;
            _logger = logger;
        }

        public async Task<IEnumerable<CurrencyResponse>> GetAllCurrenciesAsync()
        {
            var currencies = await _repository.GetAllAsync();
            return currencies.Select(c => new CurrencyResponse(c.Code, c.Name, c.Bid, c.Ask, c.Timestamp));
        }

        public async Task<CurrencyResponse?> GetLatestByCodeAsync(string code)
        {
            var currency = await _repository.GetLatestByCodeAsync(code);
            if (currency == null) return null;
            return new CurrencyResponse(currency.Code, currency.Name, currency.Bid, currency.Ask, currency.Timestamp);
        }

        public async Task ProcessScrapingAsync()
        {
            _logger.LogInformation("Iniciando processo de scraping...");
            var currencies = await _scraperService.ScrapeCurrenciesAsync();
            foreach (var currency in currencies)
            {
                await _repository.AddAsync(currency);
                _logger.LogInformation("Moeda {Code} salva com sucesso.", currency.Code);
            }
        }
    }
}
