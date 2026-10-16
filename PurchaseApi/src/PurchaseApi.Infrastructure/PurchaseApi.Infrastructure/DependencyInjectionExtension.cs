using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PurchaseApi.Domain.Repositories;
using PurchaseApi.Domain.Repositories.Purchases;
using PurchaseApi.Infrastructure.DataAcess;
using PurchaseApi.Infrastructure.DataAcess.Repositories;

namespace PurchaseApi.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IPurchaseReadOnlyRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseWriteOnlyRepository, PurchaseRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var postgresConnection = configuration.GetConnectionString("Postgres");

            if (string.IsNullOrWhiteSpace(postgresConnection))
            {
                throw new InvalidOperationException("Configuração de banco de dados ausente.");
            }

            // Postgres (EF Core)
            services.AddDbContext<PurchaseDbContext>(options => options.UseNpgsql(postgresConnection).UseSnakeCaseNamingConvention());
        }
    }
}
