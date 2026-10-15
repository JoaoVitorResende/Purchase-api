using AutoMapper;
using PurchaseApi.Application.UseCases.Purchase;
using PurchaseApi.Communication.Request;
using PurchaseApi.Communication.Response;

namespace PurchaseApi.Application.UseCases.Purchases.Register
{
    public class RegisterPurchaseUseCase : IRegisterPurchaseUseCase
    {
        private readonly IMapper _mapper;
        public RegisterPurchaseUseCase(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ResponsePurchase Execute(RequestPurchaseItem req)
        {
            
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
