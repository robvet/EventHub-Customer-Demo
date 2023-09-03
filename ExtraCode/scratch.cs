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