using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PurchaseApi.Infrastructure.DataAcess;

namespace PurchaseApi.Infrastructure.Migrations
{
    public static class DataBaseMigration
    {
        public async static Task MigrateDataBase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            // Postgres: aplica as migrations pendentes (cria o banco se não existir)
            var postgres = scope.ServiceProvider.GetRequiredService<PurchaseDbContext>();
            await postgres.Database.MigrateAsync();
        }
    }
}
