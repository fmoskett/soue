using System;

namespace CurrencyScraper.Domain.Entities
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty; // Ex: USD
        public string Name { get; set; } = string.Empty; // Ex: Dólar Americano
        public decimal Bid { get; set; } // Preço de compra
        public decimal Ask { get; set; } // Preço de venda
        public DateTime Timestamp { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}