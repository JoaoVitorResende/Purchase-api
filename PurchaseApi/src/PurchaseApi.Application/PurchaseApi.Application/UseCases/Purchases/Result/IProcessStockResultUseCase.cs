using PurchaseApi.Communication.Requests;

namespace PurchaseApi.Application.UseCases.Purchases.Result
{
    public interface IProcessStockResultUseCase
    {
        Task Execute(RequestStockResult request);
    }
}
