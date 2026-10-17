using AutoMapper;
using PurchaseApi.Application.UseCases.Purchase;
using PurchaseApi.Communication.Request;
using PurchaseApi.Communication.Response;
using PurchaseApi.Domain.Events;
using PurchaseApi.Domain.Repositories;
using PurchaseApi.Domain.Repositories.Purchases;
using PurchaseApi.Domain.Services;
using StoreApi.Exception.ExceptionBase;

namespace PurchaseApi.Application.UseCases.Purchases.Register
{
    public class RegisterPurchaseUseCase : IRegisterPurchaseUseCase
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseEventPublisher _publisher;

        public RegisterPurchaseUseCase(IMapper mapper,
            IPurchaseWriteOnlyRepository writeRepository,
            IUnitOfWork unitOfWork,
            IPurchaseEventPublisher publisher)
        {
            _mapper = mapper;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<ResponsePurchase> Execute(RequestPurchaseItem req)
        {
            Validate(req);
            var entity = _mapper.Map<Domain.entities.Purchase>(req);
            await _writeRepository.Add(entity);
            await _unitOfWork.Commit();
            await _publisher.PublishPurchaseCreatedAsync(new PurchaseCreatedEvent(entity.Id, entity.Total, DateTime.UtcNow));
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
