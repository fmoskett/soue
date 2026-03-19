using System;
using CurrencyScraper.Application.Interfaces;
using CurrencyScraper.Application.UseCases;
using CurrencyScraper.Domain.Interfaces;
using CurrencyScraper.Infrastructure.Data;
using CurrencyScraper.Infrastructure.Repositories;
using CurrencyScraper.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace CurrencyScraper.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Banco de Dados
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositórios e Casos de Uso
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICurrencyUseCase, CurrencyUseCase>();

            // HttpClient com Polly (Resiliência)
            services.AddHttpClient<ICurrencyScraperService, CurrencyScraperService>()
                .AddPolicyHandler(HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

            return services;
        }
    }
}
