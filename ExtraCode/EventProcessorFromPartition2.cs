//using Azure.Messaging.EventHubs;
//using Azure.Messaging.EventHubs.Consumer;
//using Azure.Messaging.EventHubs.Primitives;
//using Azure.Messaging.EventHubs.Producer;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace eventhub.consumer
//{
//    internal class EventProcessorFromPartition2
//    {
        

//        private readonly EventHubConsumerClient _consumer;

//        private readonly string _eventHubName;
//        private readonly string _eventHubConnectionString;
//        private readonly string _appInsightsConnectionString;
//        //private readonly string _storageContainerConnectionString;
//        //private readonly string _storageContainerName;
//        private int _eventIterator = 1;


//        public EventProcessorFromPartition2(string eventHubName,
//                                           string eventHubConnectionString,
//                                           string appInsightsConnectionString,
//                                           string storageContainerConnectionString,
//                                           string storageContainerName)
//        {
//            _eventHubName = eventHubName;
//            _eventHubConnectionString = eventHubConnectionString;
//            _appInsightsConnectionString = appInsightsConnectionString;
//            //_storageContainerConnectionString = storageContainerConnectionString;
//            //_storageContainerName = storageContainerName;


//            //_consumer = new EventHubConsumerClient(consumerGroup,
//            //                                       _eventHubConnectionString,
//            //                                       _eventHubName);

            

           

//        }

//        public async Task ConsumeEventsAsync()
//        {
//            using CancellationTokenSource cancellationSource = new CancellationTokenSource();
//            cancellationSource.CancelAfter(TimeSpan.FromSeconds(300));

//            string A53Partition = "0";
//            string S60Partition = "1";
//            string T60Partition = "2";

//            string a53ConsumerGroup = "A53consumerGroup";
//            string S60ConsumerGroup = "S60consumerGroup";
//            string T60consumerGroup = "T60consumerGroup";

//            // Configure recevier for A53 transactions
//            await using (var producer = new EventHubProducerClient(_eventHubConnectionString,
//                                                                  _eventHubName))
//            {
//                A53Partition = (await producer.GetPartitionIdsAsync()).First();
//            }

//            var A53receiver = new PartitionReceiver(
//                a53ConsumerGroup,
//                A53Partition,
//                EventPosition.Earliest,
//                _eventHubConnectionString,
//                _eventHubName);

//            //// Configure recevier for S60 transactions
//            //await using (var producer = new EventHubProducerClient(_eventHubConnectionString,
//            //                                                      _eventHubName))
//            //{
//            //    S60Partition = (await producer.GetPartitionIdsAsync()).First();
//            //}

//            //var S60receiver = new PartitionReceiver(
//            //    S60ConsumerGroup,
//            //    S60Partition,
//            //    EventPosition.Earliest,
//            //    _eventHubConnectionString,
//            //    _eventHubName);

//            //// Configure recevier for S60 transactions
//            //await using (var producer = new EventHubProducerClient(_eventHubConnectionString,
//            //                                                      _eventHubName))
//            //{
//            //    T60Partition = (await producer.GetPartitionIdsAsync()).First();
//            //}

//            //var T60receiver = new PartitionReceiver(
//            //    T60consumerGroup,
//            //    T60Partition,
//            //    EventPosition.Earliest,
//            //    _eventHubConnectionString,
//            //    _eventHubName);

//            //try
//            //{
//            //    while (!cancellationSource.IsCancellationRequested)
//            //    {
//            //        int batchSize = 50;
//            //        TimeSpan waitTime = TimeSpan.FromSeconds(1);

//            //        IEnumerable<EventData> eventBatch = await A53receiver.ReceiveBatchAsync(
//            //            batchSize,
//            //            waitTime,
//            //            cancellationSource.Token);

//            //        foreach (EventData eventData in eventBatch)
//            //        {
//            //            byte[] eventBodyBytes = eventData.EventBody.ToArray();
//            //            Console.WriteLine($"Event {_eventIterator++}  received on partition {A53Partition}: {eventData.EventBody} {Environment.NewLine} {Environment.NewLine}");
//            //            //Debug.WriteLine($"Read event of length {eventBodyBytes.Length} from {firstPartition}");
//            //        }
//            //    }
//            //}
//            //catch (TaskCanceledException)
//            //{
//            //    // This is expected if the cancellation token is
//            //    // signaled.
//            //}
//            //finally
//            //{
//            //    await A53receiver.CloseAsync();
//            //}



















//            var task1 = Task.Run(async () =>
//            {
//                try
//                {
//                    while (!cancellationSource.IsCancellationRequested)
//                    {
//                        int batchSize = 50;
//                        TimeSpan waitTime = TimeSpan.FromSeconds(1);

//                        IEnumerable<EventData> eventBatch = await A53receiver.ReceiveBatchAsync(
//                            batchSize,
//                            waitTime,
//                            cancellationSource.Token);

//                        foreach (EventData eventData in eventBatch)
//                        {
//                            byte[] eventBodyBytes = eventData.EventBody.ToArray();
//                            Console.WriteLine($"Event {_eventIterator++}  received on partition {A53Partition}: {eventData.EventBody} {Environment.NewLine} {Environment.NewLine}");
//                            //Debug.WriteLine($"Read event of length {eventBodyBytes.Length} from {firstPartition}");
//                        }
//                    }
//                }
//                catch (TaskCanceledException)
//                {
//                    // This is expected if the cancellation token is
//                    // signaled.
//                }
//                finally
//                {
//                    await A53receiver.CloseAsync();
//                }
//            });

//            //var task2 = Task.Run(async () =>
//            //{
//            //    //while (!cancellationSource.IsCancellationRequested)
//            //    //{
//            //        try
//            //        {
//            //            while (!cancellationSource.IsCancellationRequested)
//            //            {
//            //                int batchSize = 20;
//            //                TimeSpan waitTime = TimeSpan.FromSeconds(1);

//            //                IEnumerable<EventData> eventBatch = await S60receiver.ReceiveBatchAsync(
//            //                    batchSize,
//            //                    waitTime,
//            //                    cancellationSource.Token);

//            //                foreach (EventData eventData in eventBatch)
//            //                {
//            //                    byte[] eventBodyBytes = eventData.EventBody.ToArray();
//            //                    Console.WriteLine(
//            //                        $"Event {_eventIterator++}  received on partition {S60Partition}: {eventData.EventBody} {Environment.NewLine} {Environment.NewLine}");
//            //                    //Debug.WriteLine($"Read event of length {eventBodyBytes.Length} from {firstPartition}");
//            //                }
//            //            }
//            //        }
//            //        catch (TaskCanceledException)
//            //        {
//            //            // This is expected if the cancellation token is
//            //            // signaled.
//            //        }
//            //        finally
//            //        {
//            //            await S60receiver.CloseAsync();
//            //        }
//            //    //}
//            //});

//            //var task3 = Task.Run(async () =>
//            //{
//            //    //while (!cancellationSource.IsCancellationRequested)
//            //    //{
//            //        try
//            //        {
//            //            while (!cancellationSource.IsCancellationRequested)
//            //            {
//            //                int batchSize = 20;
//            //                TimeSpan waitTime = TimeSpan.FromSeconds(1);

//            //                IEnumerable<EventData> eventBatch = await T60receiver.ReceiveBatchAsync(
//            //                    batchSize,
//            //                    waitTime,
//            //                    cancellationSource.Token);

//            //                foreach (EventData eventData in eventBatch)
//            //                {
//            //                    byte[] eventBodyBytes = eventData.EventBody.ToArray();
//            //                    Console.WriteLine(
//            //                        $"Event {_eventIterator++} received on partition {T60Partition}: {eventData.EventBody} {Environment.NewLine} {Environment.NewLine}");
//            //                    //Debug.WriteLine($"Read event of length {eventBodyBytes.Length} from {firstPartition}");
//            //                }
//            //            }
//            //        }
//            //        catch (TaskCanceledException)
//            //        {
//            //            // This is expected if the cancellation token is
//            //            // signaled.
//            //        }
//            //        finally
//            //        {
//            //            await T60receiver.CloseAsync();
//            //        }
//            //    //}
//            //});

//            //await Task.WhenAll(task1, task2, task3);
//            await Task.WhenAll(task1);



//            //try
//            //{
//            //    using CancellationTokenSource cancellationSource = new CancellationTokenSource();
//            //    cancellationSource.CancelAfter(TimeSpan.FromSeconds(30));

//            //    string firstPartition = (await _consumer.GetPartitionIdsAsync(cancellationSource.Token)).First();
//            //    EventPosition startingPosition = EventPosition.Earliest;

//            //    await foreach (PartitionEvent partitionEvent in _consumer.ReadEventsFromPartitionAsync(
//            //        firstPartition,
//            //        startingPosition,
//            //        cancellationSource.Token))
//            //    {
//            //        string readFromPartition = partitionEvent.Partition.PartitionId;
//            //        ReadOnlyMemory<byte> eventBodyBytes = partitionEvent.Data.EventBody.ToMemory();
//            //        Console.WriteLine($"Event {_eventIterator++}  received on partition {partitionEvent.Partition.PartitionId}: {partitionEvent.Data.EventBody} {Environment.NewLine} {Environment.NewLine}");
//            //        //Debug.WriteLine($"Read event of length {eventBodyBytes.Length} from {readFromPartition}");
//            //    }
//            //}
//            //catch (TaskCanceledException)
//            //{
//            //    // This is expected if the cancellation token is
//            //    // signaled.
//            //}
//            //finally
//            //{
//            //    await _consumer.CloseAsync();
//            //}

//        }
//    }
//}
