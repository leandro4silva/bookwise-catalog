using Amazon.SQS;
using Amazon.SQS.Model;
using BookWise.Catalog.Infrastructure.MessageBus.Abstraction;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookWise.Catalog.Infrastructure.MessageBus.Clients;

public sealed class AwsSqsClient : IPublisher
{
    private readonly IAmazonSQS _sqsClient;
    private readonly ILogger<AwsSqsClient> _logger;

    public AwsSqsClient(IAmazonSQS sqsClient, ILogger<AwsSqsClient> logger)
    {
        _sqsClient = sqsClient;
        _logger = logger;
    }

    public async Task PublishAsync(object message, string queueUrl, CancellationToken cancellationToken)
    {
        try
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var payload = JsonConvert.SerializeObject(message, settings);

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = payload
            };

            var response = await _sqsClient.SendMessageAsync(sendMessageRequest);

            _logger.LogInformation("Message sent with ID: {MessageId}", response.MessageId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{class} - {method} - Falha - " +
                "Mensagem {@mensagem}",
                nameof(AwsSqsClient), nameof(PublishAsync), ex.Message);

            throw;
        }
    }
}
