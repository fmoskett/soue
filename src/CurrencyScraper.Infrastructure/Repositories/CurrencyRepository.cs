using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyScraper.Domain.Entities;
using CurrencyScraper.Domain.Interfaces;
using CurrencyScraper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CurrencyScraper.Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _context;

        public CurrencyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Currency currency)
        {
            await _context.Currencies.AddAsync(currency);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await _context.Currencies.OrderByDescending(c => c.Timestamp).ToListAsync();
        }

        public async Task<Currency?> GetLatestByCodeAsync(string code)
        {
            return await _context.Currencies
                .Where(c => c.Code == code)
                .OrderByDescending(c => c.Timestamp)
                .FirstOrDefaultAsync();
        }
    }
}
