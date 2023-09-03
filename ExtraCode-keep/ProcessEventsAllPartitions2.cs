//using Azure.Messaging.EventHubs;
//using Azure.Messaging.EventHubs.Consumer;
//using Azure.Messaging.EventHubs.Primitives;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//public class ConcurrentPartitionReceiver
//{
//    private readonly string eventHubConnectionString;
//    private readonly string eventHubName;
//    private readonly string consumerGroup;

//    public ConcurrentPartitionReceiver(string eventHubConnectionString, string eventHubName, string consumerGroup)
//    {
//        this.eventHubConnectionString = eventHubConnectionString;
//        this.eventHubName = eventHubName;
//        this.consumerGroup = consumerGroup;
//    }

//    public async Task ReceiveFromMultiplePartitionsAsync(string[] partitionIds)
//    {
//        List<Task> tasks = new List<Task>();

//        foreach (var partitionId in partitionIds)
//        {
//            tasks.Add(ReceiveFromPartitionAsync(partitionId));
//        }

//        await Task.WhenAll(tasks);
//    }

//    private async Task ReceiveFromPartitionAsync(string partitionId)
//    {
//        try
//        {
//            var consumerOptions = new PartitionReceiverOptions();
//            var receiver = new PartitionReceiver(consumerGroup, partitionId, EventPosition.Earliest, eventHubConnectionString, eventHubName, consumerOptions);

//            await foreach (var eventData in receiver.ReceiveBatchAsync(10, TimeSpan.FromSeconds(1)))
//            {
//                Console.WriteLine($"Received event from Partition: {partitionId}, Data: {System.Text.Encoding.UTF8.GetString(eventData.Body.ToArray())}");
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"An exception occurred while receiving from partition {partitionId}: {ex.Message}");
//        }
//    }
//}