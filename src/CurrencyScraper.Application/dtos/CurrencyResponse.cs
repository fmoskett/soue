using System;

namespace CurrencyScraper.Application.DTOs
{
    public record CurrencyResponse(
        string Code,
        string Name,
        decimal Bid,
        decimal Ask,
        DateTime Timestamp
    );
}
