
using PurchaseApi.Application.UseCases.Purchase;
using PurchaseApi.Communication.Request;

namespace PurchaseApi.Aplicartions.Register
{
    public class RegisterPurchaseUseCaseTests
    {
        [Fact]
        public void Success()
        {
            var validator = new PurchaseValidator();
            var request = new RequestPurchaseItem
            {
                itemID = "teste",
                paymentType = Domain.Enuns.PaymentType.Cash,
                Quantity = 1,
                Total = 1
            };
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Failure()
        {
            var validator = new PurchaseValidator();
            var request = new RequestPurchaseItem
            {
                itemID = "teste",
                paymentType = Domain.Enuns.PaymentType.Cash,
                Total = 0
            };
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }
    }
}
