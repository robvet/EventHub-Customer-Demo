using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using eventhub_shared.Types;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Text;

namespace eventhub_demo_eventIngester.EventIngestion
{
    public class SendEvent : ISendEvent
    {
        private readonly IConfiguration _config;

        private string eventHubName;
        private string eventHubConnectionString;
        private string appInsightsConnectionString;

        private EventHubBufferedProducerClient _bufferedClient;
        private EventHubProducerClient _producerClient;
        private EventDataBatch eventBatch;

        private int _eventIterator = 0;

        public SendEvent(IConfiguration config)
        {
            _config = config;

            eventHubName = _config["eventHubName"];
            eventHubConnectionString = _config["eventHubConnectionString"];
            appInsightsConnectionString = _config["applicationInsightsInstrumentationKey"];

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
                Console.WriteLine($"Publishing failed for {args.EventBatch.Count} events.  Error: '{args.Exception.Message}'");
                return Task.CompletedTask;
            };

            // The success handler is optional.
            _bufferedClient.SendEventBatchSucceededAsync += args =>
            {
                Console.WriteLine($"{args.EventBatch.Count} events were published to partition: '{args.PartitionId}.");
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
                    PartitionKey = eventContainer.TransactionType
                };

                var eventData = new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventContainer.EventPayload)));



                // Add metadata properties to the event

                //EventData eventData = new EventData(Encoding.UTF8.GetBytes(recordString));
                //Console.WriteLine($"sending message {counter}");
                //// Optional "dynamic routing" properties for the database, table, and mapping you created. 
                //eventData.Properties.Add("Table", "TestTable");
                //eventData.Properties.Add("IngestionMappingReference", "TestMapping");
                //eventData.Properties.Add("Format", "json");




                await _bufferedClient.EnqueueEventAsync(eventData, enqueueOptions); // .SendAsync(eventBatch);

                Console.WriteLine($"Published #{_eventIterator}. Type {eventContainer.TransactionType} message with count {eventContainer.EventPayload.TransactionCounter}");

                _eventIterator++;

                if (_eventIterator % 50 == 0)
                {
                    await _bufferedClient.FlushAsync();
                    Console.WriteLine($"Published {_eventIterator} messages");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Flush the batch of events to the event hub
                await _bufferedClient.FlushAsync();

                //await _producerClient.CloseAsync();
            }
        }

        // Destructor 
        ~SendEvent()
        {
             _bufferedClient.CloseAsync();
        }
    }

}
