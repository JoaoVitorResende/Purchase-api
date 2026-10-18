using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PurchaseApi.Domain.Repositories;
using PurchaseApi.Domain.Repositories.Purchases;
using PurchaseApi.Domain.Services;
using PurchaseApi.Infrastructure.DataAcess;
using PurchaseApi.Infrastructure.DataAcess.Repositories;
using PurchaseApi.Infrastructure.Messaging;

namespace PurchaseApi.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(
         this IServiceCollection services,
         IConfiguration configuration,
         Action<IBusRegistrationConfigurator>? configureConsumers = null)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
            RabbitmqConfiguration(services, configuration, configureConsumers);
        }

        private static void RabbitmqConfiguration(IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator>? configureConsumers)
        {
            var host = configuration["RabbitMq:Host"];
            if (string.IsNullOrWhiteSpace(host))
                throw new InvalidOperationException("RabbitMq:Host não configurado.");

            var user = configuration["RabbitMq:User"] ?? "guest";
            var pass = configuration["RabbitMq:Password"] ?? "guest";

            services.AddScoped<IPurchaseEventPublisher, PurchaseEventPublisher>();

            services.AddMassTransit(x =>
            {
                configureConsumers?.Invoke(x);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, "/", h =>
                    {
                        h.Username(user);
                        h.Password(pass);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IPurchaseReadOnlyRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseWriteOnlyRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseEventPublisher, PurchaseEventPublisher>();
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
