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
            RuleFor(purch => purch.Quantity).GreaterThan(0).WithMessage("A compra tem que ter quantidade maior que zero");
            RuleFor(purch => purch.Total).GreaterThan(0).WithMessage("Total da compra tem que ser maior que zero");
        }
    }
}
