using AutoMapper;
using PurchaseApi.Communication.Response;
using PurchaseApi.Domain.Repositories.Purchases;

namespace PurchaseApi.Application.UseCases.Purchases.Get
{
    public class GetPurchasesUseCase : IGetPurchasesUseCase
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseReadOnlyRepository _repository;
        public GetPurchasesUseCase(IPurchaseReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponsePurchase>> GetAll()
        {
            var purchases = await _repository.GetAll();

            return _mapper.Map<List<ResponsePurchase>>(purchases);
        }
    }
}
