using Microsoft.Extensions.Logging;
using PurchaseApi.Communication.Requests;
using PurchaseApi.Domain.Repositories;
using PurchaseApi.Domain.Repositories.Purchases;

namespace PurchaseApi.Application.UseCases.Purchases.Result
{
    public class ProcessStockResultUseCase : IProcessStockResultUseCase
    {
        private readonly IPurchaseWriteOnlyRepository _purchaseWriteRepository;
        private readonly IPurchaseReadOnlyRepository _purchaseReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProcessStockResultUseCase> _logger;

        public ProcessStockResultUseCase(
            IPurchaseWriteOnlyRepository purchaseWriteRepository,
            IPurchaseReadOnlyRepository purchaseReadRepository,
            IUnitOfWork unitOfWork,
            ILogger<ProcessStockResultUseCase> logger)
        {
            _purchaseWriteRepository = purchaseWriteRepository;
            _purchaseReadRepository = purchaseReadRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Execute(RequestStockResult request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var purchase = await _purchaseReadRepository.GetById(request.PurchaseId);

            if (purchase is null)
            {
                _logger.LogWarning("Compra {Id} não encontrada para aplicar o resultado do estoque.", request.PurchaseId);
                return;
            }

            if (request.Succeeded)
                purchase.Confirm();
            else
                purchase.Reject(request.FailureReason);

            await _unitOfWork.Commit();

            _logger.LogInformation("Compra {Id} agora está {Status}.", purchase.Id, purchase.Status);
        }
    }
}
