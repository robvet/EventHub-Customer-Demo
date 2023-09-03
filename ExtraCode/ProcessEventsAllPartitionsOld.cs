//using Azure.Messaging.EventHubs;
//using Azure.Messaging.EventHubs.Consumer;

//namespace eventhub.consumer
//{
//    internal class ProcessEventsAllPartitionsOld
//    {
//        private string _consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
//        private EventHubConsumerClient _consumer;
        
//        private readonly string _eventHubName;
//        private readonly string _eventHubConnectionString;
//        private readonly string _appInsightsConnectionString;
//        private readonly string _storageContainerConnectionString;
//        private readonly string _storageContainerName;
//        private int _eventIterator = 1;

//        public ProcessEventsAllPartitionsOld(string eventHubName,
//                             string eventHubConnectionString,
//                             string appInsightsConnectionString,
//                             string storageContainerConnectionString,
//                             string storageContainerName)
//        {
//            _eventHubName = eventHubName;
//            _eventHubConnectionString = eventHubConnectionString;
//            _appInsightsConnectionString = appInsightsConnectionString;
//            _storageContainerConnectionString = storageContainerConnectionString;
//            _storageContainerName = storageContainerName;

//            // Options pattern for EventHubConsumerClient
//            var consumerOptions = new EventHubConsumerClientOptions
//            {
//                ConnectionOptions = new EventHubConnectionOptions { TransportType = EventHubsTransportType.AmqpWebSockets },
//                RetryOptions = new EventHubsRetryOptions { MaximumRetries = 5, Delay = TimeSpan.FromSeconds(1) }
//            };

//            // Create a consumer client that listens for events from all partitions in the
//             _consumer = new EventHubConsumerClient(_consumerGroup,
//                                                    _eventHubConnectionString,
//                                                    _eventHubName);

//            // Extra Features
//            // Create a blob container client that the event processor will use 
//            //BlobContainerClient storageClient =
//            //    new BlobContainerClient(storageContainerConnectionString, storageContainerName);

//            //// Create the Event Grid client options
//            //var clientOptions = new EventGridPublisherClientOptions()
//            //{
//            //    TransportType = EventGridTransportType.Rest
//            //    TransportType = EventGridTransportType.Metadata
//            //};

//            //var credential = new AzureKeyCredential("<eventGridTopicKey>");
//            //var endpoint = new Uri("https://<eventGridDomain>.eventgrid.azure.net/api/events");

//            //// Create the Event Grid publisher client using the options and credentials
//            //var publisherClient = new EventGridPublisherClient(endpoint, credential, clientOptions);
//        }

//        public async Task ConsumeEventsAsync()
//        {

//            // This works but it's not async
//            // Reads from all partitions
//            // Uses the basic EventHubConsumerClient
//            try
//            {
//                using CancellationTokenSource cancellationSource = new CancellationTokenSource();
//                cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

//                await using (var _consumer = new EventHubConsumerClient(_consumerGroup, _eventHubConnectionString, _eventHubName))
//                {
//                    await foreach (var partitionEvent in _consumer.ReadEventsAsync(cancellationSource.Token))
//                    {
//                        Console.WriteLine($"Event {_eventIterator++}  received on partition {partitionEvent.Partition.PartitionId}: {partitionEvent.Data.EventBody} {Environment.NewLine} {Environment.NewLine}");
//                    }
//                }
//            }
//            catch (TaskCanceledException ex)
//            {
//                var message = ex.Message;
//                Console.WriteLine($"Exception: {ex.Message}");  
//                // This is expected if the cancellation token is
//                // signaled.
//            }
//            catch (Exception ex)
//            {
//                var message = ex.Message;
//                Console.WriteLine($"Exception: {ex.Message}");
//                // This is expected if the cancellation token is
//                // signaled.
//            }
//            finally
//            {
//                //await consumer.CloseAsync();
//            }
//        }
//    }
//}