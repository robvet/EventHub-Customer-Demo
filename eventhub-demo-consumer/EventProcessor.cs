//using Azure.Core;
//using Azure.Messaging.EventHubs;
//using Azure.Messaging.EventHubs.Processor;
//using Azure.Storage.Blobs;
//using System.Collections.Concurrent;
//using System.Text;

//class EventProcessor
//{
//    private readonly string _eventHubConnectionString;
//    private readonly string _eventHubName;
//    private readonly string _storageConnectionString;
//    private readonly string _containerName;
//    private readonly string _storageContainerConnectionString;
//    private readonly string _storageContainerName;

//    public EventProcessor(string eventHubConnectionString, 
//                          string eventHubName, 
//                          string storageConnectionString, 
//                          string containerName,
//                          string storageContainerConnectionString,
//                          string storageContainerName)
//    {
//        _eventHubConnectionString = eventHubConnectionString;
//        _eventHubName = eventHubName;
//        _storageConnectionString = storageConnectionString;
//        _containerName = containerName;
//        _storageContainerConnectionString = storageContainerConnectionString;
//        _storageContainerName = storageContainerName;
//    }

//    public async Task StartAsync(CancellationToken cancellationToken)
//    {
//        var eventProcessorClientOptions = new Azure.Messaging.EventHubs.EventProcessorClientOptions
//        {
//            ConnectionOptions = new EventHubConnectionOptions { TransportType = EventHubsTransportType.AmqpWebSockets },
//            RetryOptions = new EventHubsRetryOptions { MaximumRetries = 5, Delay = TimeSpan.FromSeconds(1) }
//        };
//    };

//        var processor1 = new EventProcessorClient(storageClient, "S53ConsumerGroup", _eventHubConnectionString, _eventHubName, eventProcessorClientOptions);

//        var processor2 = new EventProcessorClient(storageClient, "S60ConsumerGroup", _eventHubConnectionString, _eventHubName, eventProcessorClientOptions);

//        var processor3 = new EventProcessorClient(storageClient, "T60ConsumerGroup", _eventHubConnectionString, _eventHubName, eventProcessorClientOptions);


//        processor1.StartProcessingAsync(partitionID)

//        // Sample code to process an event

//        processor1.ProcessEventAsync += async eventArgs =>
//        {
//            // TODO: process event

//            // Access event data
//            var eventData = eventArgs.Data;

//            // Convert event data to string
//            string eventString = Encoding.UTF8.GetString(eventData.Body.ToArray());

//            // Log the event
//            Console.WriteLine($"Received Event: {{PartitionKey: '{eventData.PartitionKey}', Data: '{eventString}'}}");

//            // Complete event processing
//            await eventArgs.UpdateCheckpointAsync();
//        };
//        processor2.ProcessEventAsync += eventArgs =>
//        {
//            // TODO: process event
//            return Task.CompletedTask;
//        };
//        processor3.ProcessEventAsync += eventArgs =>
//        {
//            // TODO: process event
//            return Task.CompletedTask;
//        };

//        processor1.ProcessErrorAsync += errorArgs =>
//        {
//            // TODO: handle error
//            return Task.CompletedTask;
//        };
//        processor2.ProcessErrorAsync += errorArgs =>
//        {
//            // TODO: handle error
//            return Task.CompletedTask;
//        };
//        processor3.ProcessErrorAsync += errorArgs =>
//        {
//            // TODO: handle error
//            return Task.CompletedTask;
//        };

//        await Task.WhenAll(
//            processor1.StartProcessingAsync(),
//            processor2.StartProcessingAsync(),
//            processor3.StartProcessingAsync(),
//            cancellationToken);
//    }
//}
