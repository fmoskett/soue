using CurrencyScraper.Application.Interfaces;

namespace CurrencyScraper.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker iniciado às: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var useCase = scope.ServiceProvider.GetRequiredService<ICurrencyUseCase>();
                    try
                    {
                        _logger.LogInformation("Executando ciclo de scraping...");
                        await useCase.ProcessScrapingAsync();
                        _logger.LogInformation("Ciclo finalizado com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro no ciclo do worker.");
                    }
                }

                // Aguarda 1 minuto antes da próxima execução
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
