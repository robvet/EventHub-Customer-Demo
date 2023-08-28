////In this example:

////The ConsumeEventsAsync method creates an EventHubConsumerClient for each partition and starts a task to process events from that partition.
////Each task reads events using the ReadEventsAsync method, which returns an asynchronous enumerable of PartitionEvent objects.
////The processing of events is done inside the foreach loop for each partition.
////The consumers are kept running for 30 seconds, but you can adjust the duration according to your requirements.
////Make sure to handle exceptions, implement checkpointing to track the progress of event consumption, and ensure proper error handling in a production scenario. Additionally, consider using asynchronous programming best practices to ensure the efficiency and responsiveness of your consumer application.









//using Azure.Messaging.EventHubs;
//using Azure.Messaging.EventHubs.Consumer;
//using System;
//using System.Text;
//using System.Threading.Tasks;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        string connectionString = "YOUR_EVENTHUB_CONNECTION_STRING";
//        string eventHubName = "YOUR_EVENTHUB_NAME";
//        string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

//        await ConsumeEventsAsync(connectionString, eventHubName, consumerGroup);

//        Console.WriteLine("Event consumption completed!");
//    }

//    static async Task ConsumeEventsAsync(string connectionString, string eventHubName, string consumerGroup)
//    {
//        var consumerClients = new List<EventHubConsumerClient>();

//        try
//        {
//            // Create a consumer client for each partition
//            string[] partitionIds = (await new EventHubClient(connectionString, eventHubName).GetPartitionIdsAsync()).ToArray();

//            foreach (string partitionId in partitionIds)
//            {
//                var consumerClient = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName, partitionId);
//                consumerClients.Add(consumerClient);

//                // Start processing events
//                Task.Run(async () =>
//                {
//                    await foreach (PartitionEvent receivedEvent in consumerClient.ReadEventsAsync())
//                    {
//                        // Process the event data
//                        Console.WriteLine($"Event received on partition {receivedEvent.PartitionId}: {Encoding.UTF8.GetString(receivedEvent.Data.Body.ToArray())}");
//                    }
//                });
//            }

//            // Keep the consumers running for a while
//            await Task.Delay(TimeSpan.FromSeconds(30));
//        }
//        finally
//        {
//            // Close the consumer clients
//            foreach (var consumerClient in consumerClients)
//            {
//                await consumerClient.CloseAsync();
//            }
//        }
//    }
//}

















//using Azure.Messaging.EventHubs;
//using Azure.Messaging.EventHubs.Consumer;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        string connectionString = "YOUR_EVENTHUB_CONNECTION_STRING";
//        string eventHubName = "YOUR_EVENTHUB_NAME";
//        string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

//        await ConsumeEventsAsync(connectionString, eventHubName, consumerGroup);

//        Console.WriteLine("Event consumption completed!");
//    }

//    static async Task ConsumeEventsAsync(string connectionString, string eventHubName, string consumerGroup)
//    {
//        var consumerClients = new List<EventHubConsumerClient>();

//        try
//        {
//            // Create a consumer client for each partition
//            string[] partitionIds = (await new EventHubClient(connectionString, eventHubName).GetPartitionIdsAsync()).ToArray();

//            foreach (string partitionId in partitionIds)
//            {
//                var consumerClient = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName, partitionId);
//                consumerClients.Add(consumerClient);

//                // Start processing events for each partition
//                _ = Task.Run(async () =>
//                {
//                    await foreach (PartitionEvent receivedEvent in consumerClient.ReadEventsAsync())
//                    {
//                        // Process the event data
//                        Console.WriteLine($"Event received on partition {receivedEvent.PartitionId}: {Encoding.UTF8.GetString(receivedEvent.Data.Body.ToArray())}");
//                    }
//                });
//            }

//            // Keep the consumers running for a while
//            await Task.Delay(TimeSpan.FromSeconds(30));
//        }
//        finally
//        {
//            // Close the consumer clients
//            foreach (var consumerClient in consumerClients)
//            {
//                await consumerClient.CloseAsync();
//            }
//        }
//    }
//}

