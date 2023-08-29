using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using eventhub_shared.Types;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace eventhub_demo_eventIngester.EventIngestion
{
    public class SendEvent : ISendEvent
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        private string eventHubName;
        private string eventHubConnectionString;
        private string appInsightsConnectionString;

        private EventHubBufferedProducerClient _bufferedClient;
        private EventHubProducerClient _producerClient;
        private EventDataBatch eventBatch;

        private int _eventIterator = 1;

        public SendEvent(ILogger logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;

            eventHubName = _config["eventHubName"];
            eventHubConnectionString = _config["eventHubConnectionString"];
            appInsightsConnectionString = _config["applicationInsightsInstrumentationKey"];

            TelemetryConfiguration telemetryConfig = TelemetryConfiguration.CreateDefault();
            telemetryConfig.InstrumentationKey = appInsightsConnectionString;

            var telemetryDependency = new DependencyTrackingTelemetryModule();
            telemetryDependency.Initialize(telemetryConfig);

            // Create a producer client that you can use to send events to an event hub
            // The Event Hubs client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when events are being published or read regularly.
            _producerClient = new EventHubProducerClient(eventHubConnectionString, eventHubName);


            // Maybe use the EventHubBufferedProducerClient???
            // It allows each batch to be sent to a different partition as assigns the partition when the individual event is enqueued.
            // EventHubProducerClient send an entire batch to a single partition.
            _bufferedClient = new EventHubBufferedProducerClient(eventHubConnectionString, eventHubName);

            // The failure handler is required and invoked after all allowable retries were applied.
            _bufferedClient.SendEventBatchFailedAsync += args =>
            {
                Console.WriteLine($"******* Publishing failed for {args.EventBatch.Count} events.  Error: '{args.Exception.Message}'");
                return Task.CompletedTask;
            };

            // The success handler is optional.
            _bufferedClient.SendEventBatchSucceededAsync += args =>
            {
                Console.WriteLine($"******* {args.EventBatch.Count} event were published to partition: '{args.PartitionId}");
                return Task.CompletedTask;
            };
        }

        public async Task SendAsync(EventContainer eventContainer)
        {
            try
            {
                var enqueueOptions = new EnqueueEventOptions
                {
                    // Reference partition key based on transaction type
                    // Eventhub will map to a partition based on the partition key
                    ////PartitionKey = eventContainer.TransactionType,

                    // However, couldn't get this to work. So, for POC, hardcode partition based on transaction type
                    PartitionId = DetermineParitionId(eventContainer)
                };

                var eventData = new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventContainer.EventPayload)));
                // Add metadata properties to the event
                eventData.Properties.Add("TransactionType", eventContainer.TransactionType);

                await _bufferedClient.EnqueueEventAsync(eventData, enqueueOptions);

                Console.WriteLine($"({_eventIterator++}) Added {eventContainer.TransactionType} message to cache");

                //_logger.LogInformation($"Event: {JsonConvert.SerializeObject(eventContainer)}");

                // Flush the batch of events to the event hub for every 50 messages
                if (_eventIterator % 10 == 0)
                {
                    Console.WriteLine($"{Environment.NewLine}");
                    await _bufferedClient.FlushAsync();
                    Console.WriteLine($"{Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
               

                //await _producerClient.CloseAsync();
            }
        }


        private static string DetermineParitionId(EventContainer eventContainer)
        {
            var partitionId = "0";

            switch (eventContainer.TransactionType)
            {
                case "A53":
                    partitionId = "0";
                    break;

                case "S60":
                    partitionId = "1";
                    break;
                case "T60":
                    partitionId = "2";
                    break;
            }

            return partitionId;
        }

        // Destructor 
        ~SendEvent()
        {
            // Flush the batch of events to the event hub
            _bufferedClient.FlushAsync();
            _bufferedClient.CloseAsync();
        }
    }
}
