////The examples I provided in my previous responses do use consumer groups. Consumer groups allow you to have multiple independent streams of events from a single Event Hub. Each consumer group maintains its own offset, which determines where in the event stream the consumer starts reading from.

////If you want to listen to events from all partitions concurrently, you can create multiple consumer clients, each for a different partition, within the same consumer group. This way, you can achieve parallel event processing across multiple partitions.

////Here's a revised version of the code that demonstrates how to consume events from multiple partitions concurrently using consumer groups:

////This code creates a separate consumer client for each partition, allowing you to concurrently consume events from all partitions using the same consumer group.

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
//            // Create consumer clients for each partition
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
