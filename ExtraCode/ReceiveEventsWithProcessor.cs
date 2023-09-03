//using Azure.Messaging.EventHubs.Consumer;
//using Azure.Messaging.EventHubs.Primitives;
//using Microsoft.Azure.EventHubs.Processor;
//using System.Text;

//namespace eventhub.consumer
//{
//    internal class EventProcessorHostConsumer
//    {
//        private readonly string _eventHubName;
//        private readonly string _eventHubConnectionString;
//        private readonly string _consumerGroup;

//        public EventProcessorHostConsumer(string eventHubName, string eventHubConnectionString, string consumerGroup)
//        {
//            _eventHubName = eventHubName;
//            _eventHubConnectionString = eventHubConnectionString;
//            _consumerGroup = consumerGroup;
//        }

//        public async Task ConsumeEventsAsync()
//        {
//            var storageConnectionString = "YourStorageConnectionStringHere";
//            var storageContainerName = "YourStorageContainerNameHere";

//            var checkpointStore = new BlobStorageCheckpointStore(storageConnectionString, storageContainerName);
//            var eventProcessorHost = new EventProcessorHost(
//                EventProcessorHost.GenerateHostName("consolehost"),
//                _eventHubName,
//                _consumerGroup,
//                _eventHubConnectionString,
//                storageConnectionString,
//                storageContainerName,
//                "AzureWebJobsStorage");

//            var eventProcessorOptions = new Microsoft.Azure.EventHubs.Processor.EventProcessorOptions
//            {
//                InitialOffsetProvider = partitionId => EventPosition.Earliest
//            };

//            eventProcessorOptions.SetExceptionHandler(async args =>
//            {
//                Console.WriteLine($"\nException for Partition {args.PartitionId}:\n{args.Exception.GetType()}:\n{args.Exception.Message}\n\n");
//                await Task.CompletedTask;
//            });

//            await eventProcessorHost.RegisterEventProcessorFactoryAsync(
//                new EventProcessorFactory<YourProcessor>(),
//                eventProcessorOptions);

//            Console.WriteLine("Event processor started, press enter to exit...");
//            Console.ReadLine();

//            await eventProcessorHost.UnregisterEventProcessorAsync();
//        }
//    }

//    internal class YourProcessor : IEventProcessor
//    {
//        public Task CloseAsync(PartitionContext context, CloseReason reason)
//        {
//            Console.WriteLine($"Processor Shutting Down. Partition '{context.PartitionId}', Reason: '{reason}'.");
//            return Task.CompletedTask;
//        }

//        public Task OpenAsync(PartitionContext context)
//        {
//            Console.WriteLine($"Initialized processor for Partition '{context.PartitionId}'.");
//            return Task.CompletedTask;
//        }

//        public async Task ProcessErrorAsync(PartitionContext context, Exception error)
//        {
//            Console.WriteLine($"\nException for Partition {context.PartitionId}:\n{error.GetType()}:\n{error.Message}\n\n");
//            await Task.CompletedTask;
//        }

//        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
//        {
//            foreach (EventData eventData in messages)
//            {
//                Console.WriteLine($"Received Event: {Encoding.UTF8.GetString(eventData.Body.ToArray())}");
//            }

//            await context.CheckpointAsync();
//        }
//    }
//}