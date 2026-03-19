using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyScraper.Domain.Entities;

namespace CurrencyScraper.Domain.Interfaces
{
    public interface ICurrencyRepository
    {
        Task AddAsync(Currency currency);
        Task<IEnumerable<Currency>> GetAllAsync();
        Task<Currency?> GetLatestByCodeAsync(string code);
    }
}
