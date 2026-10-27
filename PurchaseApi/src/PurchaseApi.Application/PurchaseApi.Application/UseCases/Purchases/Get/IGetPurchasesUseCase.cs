using PurchaseApi.Communication.Response;

namespace PurchaseApi.Application.UseCases.Purchases.Get
{
    public interface IGetPurchasesUseCase
    {
        public Task<List<ResponsePurchase>> GetAll();
        public Task<ResponsePurchase> GetById(Guid id);
    }
}
