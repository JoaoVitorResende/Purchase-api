using Microsoft.Extensions.DependencyInjection;
using PurchaseApi.Application.UseCases.Purchases.Register;

namespace PurchaseApi.Application.UseCases
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddUseCases(services);
            AddAutoMapper(services);
        }
        private static void AddAutoMapper(IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(AutoMapping));
        }
        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterPurchaseUseCase, RegisterPurchaseUseCase>();
        }
    }
}
