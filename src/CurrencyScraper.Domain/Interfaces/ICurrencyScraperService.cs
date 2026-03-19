using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyScraper.Domain.Entities;

namespace CurrencyScraper.Domain.Interfaces
{
    public interface ICurrencyScraperService
    {
        Task<IEnumerable<Currency>> ScrapeCurrenciesAsync();
    }
}
