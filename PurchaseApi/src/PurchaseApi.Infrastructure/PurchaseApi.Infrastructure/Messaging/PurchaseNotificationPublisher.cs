using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PurchaseApi.Domain.Messages;
using PurchaseApi.Domain.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PurchaseApi.Infrastructure.Messaging
{
    internal class PurchaseNotificationPublisher : IPurchaseNotificationPublisher
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            Converters = { new JsonStringEnumConverter() }
        };

        private readonly IAmazonSQS _sqs;
        private readonly string _queueName;
        private readonly ILogger<PurchaseNotificationPublisher> _logger;
        private string? _queueUrl;

        public PurchaseNotificationPublisher(
            IAmazonSQS sqs,
            IConfiguration configuration,
            ILogger<PurchaseNotificationPublisher> logger)
        {
            _sqs = sqs;
            _logger = logger;

            var name = configuration["Aws:NotificationQueueName"];
            _queueName = string.IsNullOrWhiteSpace(name) ? "purchase-status-changed" : name;
        }

        public async Task Publish(PurchaseStatusChangedMessage message)
        {
            ArgumentNullException.ThrowIfNull(message);

            try
            {
                _queueUrl ??= (await _sqs.GetQueueUrlAsync(_queueName)).QueueUrl;

                var body = JsonSerializer.Serialize(message, JsonOptions);
                await _sqs.SendMessageAsync(_queueUrl, body);

                _logger.LogInformation("Notificação da compra {Id} enviada para a fila {Queue}.", message.PurchaseId, _queueName);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Falha ao enviar a notificação da compra {Id}.", message.PurchaseId);
            }
        }
    }
}
