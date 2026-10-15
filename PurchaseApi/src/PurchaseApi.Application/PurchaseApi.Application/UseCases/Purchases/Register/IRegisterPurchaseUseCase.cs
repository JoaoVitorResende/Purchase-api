using PurchaseApi.Communication.Request;
using PurchaseApi.Communication.Response;

namespace PurchaseApi.Application.UseCases.Purchases.Register
{
    public interface IRegisterPurchaseUseCase
    {
        public Task<ResponsePurchase> Execute(RequestPurchaseItem req);
    }
}
