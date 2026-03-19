using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyScraper.Application.DTOs;

namespace CurrencyScraper.Application.Interfaces
{
    public interface ICurrencyUseCase
    {
        Task<IEnumerable<CurrencyResponse>> GetAllCurrenciesAsync();
        Task<CurrencyResponse?> GetLatestByCodeAsync(string code);
        Task ProcessScrapingAsync();
    }
}
