using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PurchaseNotificationFunction;

public record PurchaseStatusChangedMessage(
    Guid PurchaseId,
    string Status,
    string? Reason,
    DateTime OccurredAt);

public class Function
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public Task FunctionHandler(SQSEvent sqsEvent, ILambdaContext context)
    {
        if (sqsEvent?.Records is null)
        {
            context.Logger.LogWarning("Evento SQS sem registros.");
            return Task.CompletedTask;
        }

        foreach (var record in sqsEvent.Records)
        {
            PurchaseStatusChangedMessage? message;

            try
            {
                message = JsonSerializer.Deserialize<PurchaseStatusChangedMessage>(record.Body, JsonOptions);
            }
            catch (JsonException ex)
            {
                context.Logger.LogError($"Mensagem {record.MessageId} inválida: {ex.Message}");
                continue;
            }

            if (message is null || message.PurchaseId == Guid.Empty)
            {
                context.Logger.LogWarning($"Mensagem {record.MessageId} sem dados válidos.");
                continue;
            }

            if (message.Status == "Confirmed")
                context.Logger.LogInformation($"Notificação: a compra {message.PurchaseId} foi CONFIRMADA.");
            else if (message.Status == "Rejected")
                context.Logger.LogInformation($"Notificação: a compra {message.PurchaseId} foi REJEITADA. Motivo: {message.Reason}");
            else
                context.Logger.LogWarning($"Status desconhecido '{message.Status}' na compra {message.PurchaseId}.");
        }

        return Task.CompletedTask;
    }
}