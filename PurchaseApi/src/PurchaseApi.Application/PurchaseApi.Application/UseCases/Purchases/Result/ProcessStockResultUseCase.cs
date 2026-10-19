using Microsoft.Extensions.Logging;
using PurchaseApi.Communication.Requests;
using PurchaseApi.Domain.Messages;
using PurchaseApi.Domain.Repositories;
using PurchaseApi.Domain.Repositories.Purchases;
using PurchaseApi.Domain.Services;

namespace PurchaseApi.Application.UseCases.Purchases.Result
{
    public class ProcessStockResultUseCase : IProcessStockResultUseCase
    {
        private readonly IPurchaseWriteOnlyRepository _purchaseWriteRepository;
        private readonly IPurchaseReadOnlyRepository _purchaseReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProcessStockResultUseCase> _logger;
        private readonly IPurchaseNotificationPublisher _notificationPublisher;

        public ProcessStockResultUseCase(
            IPurchaseWriteOnlyRepository purchaseWriteRepository,
            IPurchaseReadOnlyRepository purchaseReadRepository,
            IUnitOfWork unitOfWork,
            ILogger<ProcessStockResultUseCase> logger,
            IPurchaseNotificationPublisher notificationPublisher)
        {
            _purchaseWriteRepository = purchaseWriteRepository;
            _purchaseReadRepository = purchaseReadRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notificationPublisher = notificationPublisher;
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

            var previousStatus = purchase.Status;

            if (request.Succeeded)
                purchase.Confirm();
            else
                purchase.Reject(request.FailureReason);

            if (purchase.Status == previousStatus)
            {
                _logger.LogInformation("Compra {Id} já estava {Status}, sem notificação.", purchase.Id, purchase.Status);
                return;
            }

            await _unitOfWork.Commit();

            await _notificationPublisher.Publish(new PurchaseStatusChangedMessage(
            purchase.Id,
            purchase.Status,
            purchase.RejectionReason,
            DateTime.UtcNow));

            _logger.LogInformation("Compra {Id} agora está {Status}.", purchase.Id, purchase.Status);
        }
    }
}
