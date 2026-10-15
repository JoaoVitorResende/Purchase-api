using FluentValidation;
using PurchaseApi.Communication.Request;

namespace PurchaseApi.Application.UseCases.Purchase
{
    public class PurchaseValidator : AbstractValidator<RequestPurchaseItem>
    {
        public PurchaseValidator()
        {
            RuleFor(purch => purch.itemID).NotNull().NotEmpty().WithMessage("O ID do item nao pode faltar");
            RuleFor(purch => purch.paymentType).IsInEnum().WithMessage("tipo de pagamento invalido");
        }
    }
}
