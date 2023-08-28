using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using System.Text;

namespace eventhub.consumer
{
    internal class ConsumeEvents
    {
        private string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

        public async Task ConsumeEventsAsync(string eventHubName,
                                             string eventHubConnectionString,
                                             string appInsightsConnectionString,
                                             string storageContainerConnectionString,
                                             string storageContainerName)
        {

            // Create a blob container client that the event processor will use 
            BlobContainerClient storageClient =
                new BlobContainerClient(storageContainerConnectionString, storageContainerName);

            // Create an event processor client to process events in the event hub
            var processor = new EventProcessorClient(
                storageClient, consumerGroup, eventHubConnectionString, eventHubName);

            var consumer = new EventHubConsumerClient(consumerGroup, eventHubConnectionString, eventHubName);

            //processor.AddEventProcessorTelemetryInitializer(
            //                   (string partitionId, EventProcessorTelemetryContext context) =>
            //                   {
            //        context.SetProperty("PartitionId", partitionId);
            //        context.SetProperty("ConsumerGroup", consumerGroup);
            //    });


            // Register handlers for processing events and handling errors
            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            // Add the Application Insights integration

            await QueryPartitionProperties(consumer);


            // Start the processing
            await processor.StartProcessingAsync();

            // Wait for 30 seconds for the events to be processed
            await Task.Delay(TimeSpan.FromSeconds(30));

            // Stop the processing
            await processor.StopProcessingAsync();

            Task ProcessEventHandler(ProcessEventArgs eventArgs)
            {
                var partitionId = eventArgs.Partition.PartitionId;
                Console.WriteLine($"\tPartitionId read: {partitionId}");

                long offset = eventArgs.Data.Offset;
                Console.WriteLine($"\tOffset read: {offset}");

                var data = eventArgs.Data.Body.ToArray();
                var dataString = Encoding.UTF8.GetString(data);

                Console.WriteLine("\t\t{0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
                return Task.CompletedTask;
            }

            Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
            {
                // Write details about the error to the console window
                Console.WriteLine($"\tPartition '{eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
                Console.WriteLine(eventArgs.Exception.Message);
                return Task.CompletedTask;
            }
        }

        private async Task QueryPartitionProperties(EventHubConsumerClient consumer)
        {
            try
            {
                string[] partitions = await consumer.GetPartitionIdsAsync();

                foreach (string partition in partitions)
                {
                    //string firstPartition = partitions.FirstOrDefault();

                    PartitionProperties partitionProperties = await consumer.GetPartitionPropertiesAsync(partition);

                    Console.WriteLine($"Partition: {partitionProperties.Id}");
                    Console.WriteLine($"\tThe partition contains no events: {partitionProperties.IsEmpty}");
                    Console.WriteLine($"\tThe first sequence number is: {partitionProperties.BeginningSequenceNumber}");
                    Console.WriteLine($"\tThe last sequence number is: {partitionProperties.LastEnqueuedSequenceNumber}");
                    Console.WriteLine($"\tThe last offset is: {partitionProperties.LastEnqueuedOffset}");
                    Console.WriteLine($"\tThe last enqueued time is: {partitionProperties.LastEnqueuedTime}, in UTC.");
                }


            }
            finally
            {
                await consumer.CloseAsync();
            }

        }
    }
}
