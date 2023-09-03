//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ExtraCode
//{
//    internal class SingelPartition_PartitionReceiver
//    {


//    }
//}


//var connectionString = "<< CONNECTION STRING FOR THE EVENT HUBS NAMESPACE >>";
//var eventHubName = "<< NAME OF THE EVENT HUB >>";
//var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

//using CancellationTokenSource cancellationSource = new CancellationTokenSource();
//cancellationSource.CancelAfter(TimeSpan.FromSeconds(30));

//string firstPartition;

//await using (var producer = new EventHubProducerClient(connectionString, eventHubName))
//{
//    firstPartition = (await producer.GetPartitionIdsAsync()).First();
//}

//var receiver = new PartitionReceiver(
//    consumerGroup,
//    firstPartition,
//    EventPosition.Earliest,
//    connectionString,
//    eventHubName);

//try
//{
//    while (!cancellationSource.IsCancellationRequested)
//    {
//        int batchSize = 50;
//        TimeSpan waitTime = TimeSpan.FromSeconds(1);

//        IEnumerable<EventData> eventBatch = await receiver.ReceiveBatchAsync(
//            batchSize,
//            waitTime,
//            cancellationSource.Token);

//        foreach (EventData eventData in eventBatch)
//        {
//            byte[] eventBodyBytes = eventData.EventBody.ToArray();
//            Debug.WriteLine($"Read event of length {eventBodyBytes.Length} from {firstPartition}");
//        }
//    }
//}
//catch (TaskCanceledException)
//{
//    // This is expected if the cancellation token is
//    // signaled.
//}
//finally
//{
//    await receiver.CloseAsync();
//}
