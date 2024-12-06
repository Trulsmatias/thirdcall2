using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class ServiceBusQueueTrigger2
    {
        private readonly ILogger<ServiceBusQueueTrigger2> _logger;

        public ServiceBusQueueTrigger2(ILogger<ServiceBusQueueTrigger2> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ServiceBusQueueTrigger2))]
        public async Task Run(
            [ServiceBusTrigger("firstqueue", Connection = "servicebusaz04_SERVICEBUS")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
