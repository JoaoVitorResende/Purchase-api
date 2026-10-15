using AutoMapper;
using PurchaseApi.Application.UseCases.Purchase;
using PurchaseApi.Communication.Request;
using PurchaseApi.Communication.Response;
using PurchaseApi.Domain.Repositories;
using PurchaseApi.Domain.Repositories.Purchases;
using StoreApi.Exception.ExceptionBase;

namespace PurchaseApi.Application.UseCases.Purchases.Register
{
    public class RegisterPurchaseUseCase : IRegisterPurchaseUseCase
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterPurchaseUseCase(IMapper mapper, IPurchaseWriteOnlyRepository writeRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponsePurchase> Execute(RequestPurchaseItem req)
        {
            Validate(req);
            var entity = _mapper.Map<Domain.entities.Purchase>(req);
            await _writeRepository.Add(entity);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponsePurchase>(entity);
        }

        private void Validate(RequestPurchaseItem req)
        {
            var validator = new PurchaseValidator();
            var result = validator.Validate(req);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
