using Contracts.Events;
using MassTransit;
using PurchaseApi.Application.UseCases.Purchases.Result;
using PurchaseApi.Communication.Requests;

namespace PurchaseApi.Messaging
{
    public class StockProcessedConsumer : IConsumer<StockProcessedEvent>
    {
        private readonly IProcessStockResultUseCase _useCase;
        private readonly ILogger<StockProcessedConsumer> _logger;

        public StockProcessedConsumer(
            IProcessStockResultUseCase useCase,
            ILogger<StockProcessedConsumer> logger)
        {
            _useCase = useCase;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<StockProcessedEvent> context)
        {
            var evt = context.Message;

            if (evt is null || evt.PurchaseId == Guid.Empty)
            {
                _logger.LogWarning("Resultado de estoque inválido recebido.");
                return;
            }

            _logger.LogInformation(
                "Resultado de estoque recebido para a compra {Id}: sucesso = {Succeeded}.",
                evt.PurchaseId, evt.Succeeded);

            var request = new RequestStockResult(evt.PurchaseId, evt.Succeeded, evt.FailureReason);

            await _useCase.Execute(request);
        }
    }
}
