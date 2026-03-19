Currency Scraper - Desafio Técnico RPA & Web API Este projeto consiste em um ecossistema automatizado para captura e disponibilização de dados de cotação de moedas (USD, EUR, BTC) em relação ao Real (BRL). A solução utiliza Clean Architecture, .NET 8, Docker e SQL Server.

🚀 Visão Geral da Solução A arquitetura foi dividida em quatro camadas principais seguindo os princípios da Clean Architecture: • Domain: Entidades de negócio e interfaces de contrato. • Application: Casos de uso, DTOs e lógica de orquestração. • Infrastructure: Implementação de acesso a dados (EF Core), serviços externos (Scraping via AwesomeAPI) e resiliência (Polly). • Presentation: Composta por dois serviços independentes: ◦ Worker (RPA): Serviço em background que executa o scraping periódico. ◦ Web API: Interface RESTful para consulta dos dados coletados.

🛠️ Tecnologias Utilizadas • Linguagem: C# (.NET 8) • Banco de Dados: SQL Server 2022 • ORM: Entity Framework Core • Resiliência: Polly (Retry Policy para falhas de rede) • Containerização: Docker & Docker Compose • Documentação: Swagger / OpenAPI

📋 Instruções para Rodar o Projeto Pré-requisitos • Docker e Docker Compose instalados. • SDK do .NET 8 (opcional, se quiser rodar localmente sem Docker).

Passo a Passo (Docker) 1 Clone o repositório. 2 Na raiz do projeto, execute: docker-compose up --build 3 A API estará disponível em: http://localhost:5000/swagger 4 O Worker começará a coletar dados automaticamente a cada 1 minuto.

Passo a Passo (Local) 5 Certifique-se de ter um SQL Server rodando e ajuste a DefaultConnection nos arquivos appsettings.json. 6 Execute as migrations (se necessário): dotnet ef database update --project src/CurrencyScraper.Infrastructure --startup-project src/CurrencyScraper.Api 7 Inicie os projetos.

🏗️ Decisões Arquiteturais 8 Clean Architecture: Garante que a lógica de negócio seja independente de frameworks e detalhes de infraestrutura. 9 Resiliência com Polly: O serviço de scraping utiliza uma política de Wait and Retry para lidar com instabilidades temporárias na rede ou no provedor de dados. 10 Injeção de Dependência: Centralizada na camada de Infrastructure para facilitar a manutenção e testes. 11 AwesomeAPI: Escolhida como fonte de dados por ser uma API pública estável, permitindo focar na estrutura do RPA e da API conforme solicitado no desafio.

📈 Melhorias Futuras • Autenticação: Implementar JWT para proteger os endpoints da API. • Cache: Adicionar Redis para consultas rápidas de cotações recentes. • Testes Unitários: Cobertura de testes para os casos de uso e serviços. • Dashboard: Uma interface simples em Blazor ou React para visualizar as cotações em tempo real. • Configuração Dinâmica: Mover o intervalo de scraping para o appsettings.json ou uma variável de ambiente.

Desenvolvido como parte de um desafio técnico para demonstrar competências em arquitetura de software e desenvolvimento .NET.
