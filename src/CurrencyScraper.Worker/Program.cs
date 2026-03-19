using CurrencyScraper.Infrastructure;
using CurrencyScraper.Worker;

var builder = Host.CreateApplicationBuilder(args);

// Injeção de Dependência
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
