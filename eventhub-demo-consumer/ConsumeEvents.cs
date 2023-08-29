using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Storage.Blobs;
using Azure.Messaging.EventGrid;
using Azure;
using System.Net;

namespace eventhub.consumer
{
    internal class ConsumeEvents
    {
        private string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
        private EventHubConsumerClient _consumerGroupA53;
        private EventHubConsumerClient _consumerGroupS60;
        private EventHubConsumerClient _consumerGroupT60;

        private CancellationToken cancellationToken;
        private readonly string _eventHubName;
        private readonly string _eventHubConnectionString;
        private readonly string _appInsightsConnectionString;
        private readonly string _storageContainerConnectionString;
        private readonly string _storageContainerName;
        private int _eventIterator = 1;

        public ConsumeEvents(string eventHubName,
                             string eventHubConnectionString,
                             string appInsightsConnectionString,
                             string storageContainerConnectionString,
                             string storageContainerName)
        {
            _eventHubName = eventHubName;
            _eventHubConnectionString = eventHubConnectionString;
            _appInsightsConnectionString = appInsightsConnectionString;
            _storageContainerConnectionString = storageContainerConnectionString;
            _storageContainerName = storageContainerName;

            // Create a blob container client that the event processor will use 
            BlobContainerClient storageClient =
                new BlobContainerClient(storageContainerConnectionString, storageContainerName);

            var consumerOptions = new EventHubConsumerClientOptions
            {
                ConnectionOptions = new EventHubConnectionOptions { TransportType = EventHubsTransportType.AmqpWebSockets },
                RetryOptions = new EventHubsRetryOptions { MaximumRetries = 5, Delay = TimeSpan.FromSeconds(1) }
            };

            var consumer = new EventHubConsumerClient(consumerGroup,
                                                      _eventHubConnectionString,
                                                      _eventHubName);

            //// Create the Event Grid client options
            //var clientOptions = new EventGridPublisherClientOptions()
            //{
            //    TransportType = EventGridTransportType.Rest
            //    TransportType = EventGridTransportType.Metadata
            //};
           
            //var credential = new AzureKeyCredential("<eventGridTopicKey>");
            //var endpoint = new Uri("https://<eventGridDomain>.eventgrid.azure.net/api/events");

            //// Create the Event Grid publisher client using the options and credentials
            //var publisherClient = new EventGridPublisherClient(endpoint, credential, clientOptions);
        }


        public async Task ConsumeEventsAsync()
        {

            // This works but it's not async
            // Reads from all partitions
            // Uses the basic EventHubConsumerClient
            try
            {
                await using (var consumer = new EventHubConsumerClient(consumerGroup, _eventHubConnectionString, _eventHubName))
                {
                    await foreach (var partitionEvent in consumer.ReadEventsAsync(cancellationToken))
                    {
                        Console.WriteLine($"Event {_eventIterator++}  received on partition {partitionEvent.Partition.PartitionId}: {partitionEvent.Data.EventBody} {Environment.NewLine} {Environment.NewLine}");


                        //// Create the event subject, type, and data
                        //var subject = "myapp/mydata";
                        //var eventType = "MyCompany.MyApp.MyEventType";
                        //var eventData = new { message = "This is a new event from my app" };
                        
                        //await publisherClient.SendEventAsync(new EventGridEvent(subject, eventType, eventData));
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                var message = ex.Message;
                // This is expected if the cancellation token is
                // signaled.
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                // This is expected if the cancellation token is
                // signaled.
            }
            finally
            {
                //await consumer.CloseAsync();
            }












     
        }



     
    }
}

//string consumerGroup1 = "CONSUMER GROUP 1";
//string consumerGroup2 = "CONSUMER GROUP 2";
//string consumerGroup3 = "CONSUMER GROUP 3";


//string partitionId = "0";

//// Read events from partition 0
//var eventPosition = EventPosition.FromStart(partitionId: "0");
//await consumer.ReadEventsAsync(eventPosition, cancellationToken);


//// Read events from the specified partitions for each consumer group
//await foreach (PartitionEvent partitionEvent in _consumerGroupA53.ReadEventsAsync(new EventPosition(partitionId), cancellationToken))
//{
//    // Process the event here...
//}

//await foreach (PartitionEvent partitionEvent in _consumerGroupS60.ReadEventsAsync(new[] { "2", "3" }))
//{
//    // Process the event here...
//}

//await foreach (PartitionEvent partitionEvent in _consumerGroupT60.ReadEventsAsync(new[] { "4", "5" }))
//{
//    // Process the event here...
//}



//string partitionId = "0";

//var consumerOptions = new EventHubConsumerClientOptions
//{
//    ConnectionOptions = new EventHubConnectionOptions { TransportType = EventHubsTransportType.AmqpWebSockets },
//    RetryOptions = new EventHubsRetryOptions { MaximumRetries = 5, Delay = TimeSpan.FromSeconds(1) }
//};

//var consumerClient = new EventHubConsumerClient(consumerGroup, _eventHubConnectionString, _eventHubName, partitionId, consumerOptions);

//await foreach (PartitionEvent partitionEvent in consumerClient.ReadEventsAsync())
//{
//    Console.WriteLine($"Received event: {Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray())}");
//}




//EventPosition eventPosition = EventPosition.FromOffset(Convert.ToInt64(0));// .FromEnqueuedTime(DateTimeOffset.UtcNow);

//await using (var consumer1 = new EventHubConsumerClient(consumerGroup1, _eventHubConnectionString, _eventHubName))
//await using (var consumer2 = new EventHubConsumerClient(consumerGroup2, _eventHubConnectionString, _eventHubName))
//await using (var consumer3 = new EventHubConsumerClient(consumerGroup3, _eventHubConnectionString, _eventHubName))
//{













//    var partitions = await consumer1.GetPartitionIdsAsync();

//    var tasks = new Task[partitions.Length * 3];

//    tasks[0] = Task.Run(async () =>
//    {
//        //await foreach (var partitionEvent in consumer1.ReadEventsAsync(partitions[i]))
//        await foreach (var partitionEvent in consumer1.ReadEventsFromPartitionAsync(partitions[0], eventPosition))
//        {
//            Console.WriteLine($"Event received on partition {partitionEvent.Partition.PartitionId} by {consumerGroup1}: {partitionEvent.Data.EventBody}");
//        }
//    });

//tasks[1] = Task.Run(async () =>
//{
//    //await foreach (var partitionEvent in consumer1.ReadEventsAsync(partitions[i]))
//    await foreach (var partitionEvent in consumer2.ReadEventsFromPartitionAsync(partitions[1], eventPosition))
//    {
//        Console.WriteLine($"Event received on partition {partitionEvent.Partition.PartitionId} by {consumerGroup1}: {partitionEvent.Data.EventBody}");
//    }
//});

//tasks[2] = Task.Run(async () =>
//{
//    //await foreach (var partitionEvent in consumer1.ReadEventsAsync(partitions[i]))
//    await foreach (var partitionEvent in consumer3.ReadEventsFromPartitionAsync(partitions[2], eventPosition))
//    {
//        Console.WriteLine($"Event received on partition {partitionEvent.Partition.PartitionId} by {consumerGroup1}: {partitionEvent.Data.EventBody}");
//    }
//});


//for (int i = 0; i < partitions.Length; i++)
//{
//    if (i % 2 == 0)
//    {
//        tasks[i] = Task.Run(async () =>
//        {
//            //await foreach (var partitionEvent in consumer1.ReadEventsAsync(partitions[i]))
//            await foreach (var partitionEvent in consumer1.ReadEventsFromPartitionAsync(partitions[i], eventPosition))
//            {
//                Console.WriteLine($"Event received on partition {partitionEvent.Partition.PartitionId} by {consumerGroup1}: {partitionEvent.Data.EventBody}");
//            }
//        });
//    }
//    else
//    {
//        ////tasks[i] = Task.Run(async () =>
//        ////{
//        ////    await foreach (var partitionEvent in consumer2.ReadEventsAsync(partitions[i]))
//        ////    {
//        ////        Console.WriteLine($"Event received on partition {partitionEvent.Partition.PartitionId} by {consumerGroup2}: {partitionEvent.Data.EventBody}");
//        ////    }
//        ////});
//    }
//}

//await Task.WhenAll(tasks);



////// Round two - not working
////var consumerClients = new List<EventHubConsumerClient>();


////try
////{
////    // Create consumer clients for each partition
////    //string[] partitionIds = (await new EventHubClient(_eventHubConnectionString, _eventHubName).GetPartitionIdsAsync()).ToArray();
////    //string[] partitionIds = (await new EventHubConsumerClient(_eventHubConnectionString, _eventHubName).GetPartitionIdsAsync()).ToArray();
////    string[] partitionIds = (await new EventHubConsumerClient(_eventHubConnectionString, _eventHubName).GetPartitionIdsAsync()).ToArray();


////    //foreach (string partitionId in partitionIds)
////    //{
////    //    var consumerClient = new EventHubConsumerClient(consumerGroup, _eventHubConnectionString, _eventHubName, partitionId);
////    //    consumerClients.Add(consumerClient);

////    //    // Start processing events for each partition
////    //    _ = Task.Run(async () =>
////    //    {
////    //        await foreach (PartitionEvent receivedEvent in consumerClient.ReadEventsAsync())
////    //        {
////    //            // Process the event data
////    //            ////Console.WriteLine($"Event received on partition {receivedEvent.PartitionId}: {Encoding.UTF8.GetString(receivedEvent.Data.Body.ToArray())}");
////    //        }
////    //    });
////    //}

////    // Keep the consumers running for a while
////    await Task.Delay(TimeSpan.FromSeconds(30));
////}
////catch (Exception ex)
////{
////    var msg = ex.Message;
////}
////finally
////{
////    // Close the consumer clients
////    foreach (var consumerClient in consumerClients)
////    {
////        await consumerClient.CloseAsync();
////    }
////}


// Old
//var consumer = new EventHubConsumerClient(consumerGroup, eventHubConnectionString, eventHubName);

//processor.AddEventProcessorTelemetryInitializer(
//                   (string partitionId, EventProcessorTelemetryContext context) =>
//                   {
//        context.SetProperty("PartitionId", partitionId);
//        context.SetProperty("ConsumerGroup", consumerGroup);
//    });


//// Register handlers for processing events and handling errors
//processor.ProcessEventAsync += ProcessEventHandler;
//processor.ProcessErrorAsync += ProcessErrorHandler;

//// Add the Application Insights integration

//await QueryPartitionProperties(consumer);


//// Start the processing
//await processor.StartProcessingAsync();

//// Wait for 30 seconds for the events to be processed
//await Task.Delay(TimeSpan.FromSeconds(30));

//// Stop the processing
//await processor.StopProcessingAsync();

//Task ProcessEventHandler(ProcessEventArgs eventArgs)
//{
//    var partitionId = eventArgs.Partition.PartitionId;
//    Console.WriteLine($"\tPartitionId read: {partitionId}");

//    long offset = eventArgs.Data.Offset;
//    Console.WriteLine($"\tOffset read: {offset}");

//    var data = eventArgs.Data.Body.ToArray();
//    var dataString = Encoding.UTF8.GetString(data);

//    Console.WriteLine("\t\t{0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
//    return Task.CompletedTask;
//}

//Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
//{
//    // Write details about the error to the console window
//    Console.WriteLine($"\tPartition '{eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
//    Console.WriteLine(eventArgs.Exception.Message);
//    return Task.CompletedTask;
//}
//}

//private async Task QueryPartitionProperties(EventHubConsumerClient consumer)
//{
//    try
//    {
//        string[] partitions = await consumer.GetPartitionIdsAsync();

//        foreach (string partition in partitions)
//        {
//            //string firstPartition = partitions.FirstOrDefault();

//            PartitionProperties partitionProperties = await consumer.GetPartitionPropertiesAsync(partition);

//            Console.WriteLine($"Partition: {partitionProperties.Id}");
//            Console.WriteLine($"\tThe partition contains no events: {partitionProperties.IsEmpty}");
//            Console.WriteLine($"\tThe first sequence number is: {partitionProperties.BeginningSequenceNumber}");
//            Console.WriteLine($"\tThe last sequence number is: {partitionProperties.LastEnqueuedSequenceNumber}");
//            Console.WriteLine($"\tThe last offset is: {partitionProperties.LastEnqueuedOffset}");
//            Console.WriteLine($"\tThe last enqueued time is: {partitionProperties.LastEnqueuedTime}, in UTC.");
//        }


//    }
//    finally
//    {
//        await consumer.CloseAsync();
//    }

//}




//// Build a consumer group for A53 partition
//_consumerGroupA53 = new EventHubConsumerClient(
//     //EventHubConsumerClient.DefaultConsumerGroupName, 
//     "A53", _eventHubConnectionString, _eventHubName,
//    new EventHubConsumerClientOptions
//    {
//        ConnectionOptions = new EventHubConnectionOptions
//        {
//            TransportType = EventHubsTransportType.AmqpWebSockets
//        },
//        RetryOptions = new EventHubsRetryOptions
//        {
//            MaximumRetries = 5,
//            TryTimeout = TimeSpan.FromSeconds(10)
//        }
//    });

//// Build a consumer group for S60 partition
//_consumerGroupS60 = new EventHubConsumerClient(
//     //EventHubConsumerClient.DefaultConsumerGroupName, 
//     "S60", _eventHubConnectionString, _eventHubName,
//    new EventHubConsumerClientOptions
//    {
//        ConnectionOptions = new EventHubConnectionOptions
//        {
//            TransportType = EventHubsTransportType.AmqpWebSockets
//        },
//        RetryOptions = new EventHubsRetryOptions
//        {
//            MaximumRetries = 5,
//            TryTimeout = TimeSpan.FromSeconds(10)
//        }
//    });

//// Build a consumer group for T60 partition
//EventHubConsumerClient consumerGroupT60 = new EventHubConsumerClient(
//     //EventHubConsumerClient.DefaultConsumerGroupName, 
//     "T60", _eventHubConnectionString, _eventHubName,
//    new EventHubConsumerClientOptions
//    {
//        ConnectionOptions = new EventHubConnectionOptions
//        {
//            TransportType = EventHubsTransportType.AmqpWebSockets
//        },
//        RetryOptions = new EventHubsRetryOptions
//        {
//            MaximumRetries = 5,
//            TryTimeout = TimeSpan.FromSeconds(10)
//        }
//    });