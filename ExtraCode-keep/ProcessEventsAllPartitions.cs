//using Azure.Messaging.EventHubs;
//using Azure.Messaging.EventHubs.Consumer;

//namespace eventhub.consumer
//{
//    internal class ProcessEventsAllPartitions
//    {
//        private string _consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
//        private EventHubConsumerClient _consumer;
        
//        private readonly string _eventHubName;
//        private readonly string _eventHubConnectionString;
//        private int _eventIterator = 1; private readonly string _storageContainerConnectionString;
//        private readonly string _storageContainerName;

//        public ProcessEventsAllPartitions(string eventHubName,
//                             string eventHubConnectionString,
//                             string consumerGroupName,
//                             string storageContainerConnectionString,
//                             string storageContainerName)
//        {
//            _eventHubName = eventHubName;
//            _eventHubConnectionString = eventHubConnectionString;
//            _consumerGroup = consumerGroupName;
//            _storageContainerConnectionString = storageContainerConnectionString;
//            _storageContainerName = storageContainerName;

//            // Options pattern for EventHubConsumerClient
//            //var consumerOptions = new EventHubConsumerClientOptions
//            //{
//            //    ConnectionOptions = new EventHubConnectionOptions { TransportType = EventHubsTransportType.AmqpWebSockets },
//            //    RetryOptions = new EventHubsRetryOptions { MaximumRetries = 5, Delay = TimeSpan.FromSeconds(1) }
//            //};

//            // Create a consumer client that listens for events from all partitions
//            _consumer = new EventHubConsumerClient(_consumerGroup,
//                                                    _eventHubConnectionString,
//                                                    _eventHubName);
//                                                    //consumerOptions);
//        }

//        public async Task ConsumeEventsAsync()
//        {
//            //string[] partitionIds = await _consumer.GetPartitionIdsAsync();
//            string[] partitionIds = new string[] { "0" };

//            try
//            {
//                using CancellationTokenSource cancellationSource = new CancellationTokenSource();
//                cancellationSource.CancelAfter(TimeSpan.FromSeconds(300));

//                // Read events from all partitions concurrently
//                var tasks = new List<Task>();

//                foreach (var partitionId in partitionIds)
//                {
                   
//                    tasks.Add(ConsumeEventsFromPartitionAsync(partitionId, cancellationSource.Token));
//                }

//                try
//                {
//                    await Task.WhenAll(tasks).ConfigureAwait(false);
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Exception thrown by Task.WhenAll: {ex}");
//                }


//            }
//            catch (TaskCanceledException ex)
//            {
//                Console.WriteLine($"Exception: {ex.Message}");
//            }
//            catch (EventHubsException ex)
//            {
//                Console.WriteLine($"Exception: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Exception: {ex.Message}");
//            }
//            finally
//            {
//                await _consumer.CloseAsync();
//            }
//        }

//        private async Task ConsumeEventsFromPartitionAsync(string partitionId, CancellationToken cancellationToken)
//        {
//            try
//            {
//                await foreach (var partitionEvent in _consumer.ReadEventsFromPartitionAsync(partitionId, EventPosition.Earliest, new ReadEventOptions { MaximumWaitTime = TimeSpan.FromSeconds(1) }, cancellationToken))
//                {

//                    if (partitionEvent.Data != null)
//                    {
//                        Console.WriteLine($"Event {_eventIterator++} received on partition {partitionId}: {partitionEvent.Data.EventBody} {Environment.NewLine} {Environment.NewLine}");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception to diagnose issues
//                Console.WriteLine($"Error occurred on partition {partitionId}: {ex.Message}");
//            }
//        }
//    }
//}
