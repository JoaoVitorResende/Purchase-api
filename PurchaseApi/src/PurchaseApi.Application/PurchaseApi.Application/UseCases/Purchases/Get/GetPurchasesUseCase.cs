using AutoMapper;
using PurchaseApi.Communication.Response;
using PurchaseApi.Domain.entities;
using PurchaseApi.Domain.Repositories.Purchases;
using StoreApi.Exception.ExceptionBase;

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

        public async Task<ResponsePurchase> GetById(Guid id)
        {
            var purchases = await _repository.GetById(id);

            if (purchases is null)
                throw new NotFoundException("Compra não encontrada.");

            return _mapper.Map<ResponsePurchase>(purchases);
        }
    }
}
