using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Primitives;
using eventhub_shared.PartitionManager;
using eventhub_shared.Enumerations;

namespace eventhub.consumer
{
    internal class ProcessLotterySalesEvents
    {
        private readonly EventHubConsumerClient _consumer;

        private readonly string _eventHubName;
        private readonly string _eventHubConnectionString;
        private readonly string _appInsightsConnectionString;
        //private readonly string _storageContainerConnectionString;
        //private readonly string _storageContainerName;
        private int _secondsToRun = 300; // Default to 5 minutes
        private int _eventIteratorCounter = 1;


        public ProcessLotterySalesEvents(string eventHubName,
                                              string eventHubConnectionString,
                                              string appInsightsConnectionString,
                                              string storageContainerConnectionString,
                                              string storageContainerName,
                                              int secondsToRun)
        {
            _eventHubName = eventHubName;
            _eventHubConnectionString = eventHubConnectionString;
            _appInsightsConnectionString = appInsightsConnectionString;
            _secondsToRun = secondsToRun;
            //_storageContainerConnectionString = storageContainerConnectionString;
            //_storageContainerName = storageContainerName;
        }

        public async Task ConsumeEventsAsync()
        {
            var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            using CancellationTokenSource cancellationSource = new CancellationTokenSource();
            cancellationSource.CancelAfter(TimeSpan.FromSeconds(_secondsToRun));

            // Select specfic partition
            //string[] partitionIds = await _consumer.GetPartitionIdsAsync();
            string[] partitionIds = new string[] { PartitionMapper.MapPartition(TransactionTypeEnum.Lottery) };
            var selectedPartition = partitionIds[0];

            var receiver = new PartitionReceiver(
                consumerGroup,
                selectedPartition,
                EventPosition.Earliest,
                _eventHubConnectionString,
                _eventHubName);

            try
            {
                while (!cancellationSource.IsCancellationRequested)
                {
                    int batchSize = 50;
                    TimeSpan waitTime = TimeSpan.FromSeconds(1);

                    IEnumerable<EventData> eventBatch = await receiver.ReceiveBatchAsync(
                        batchSize,
                        waitTime,
                        cancellationSource.Token);

                    foreach (EventData eventData in eventBatch)
                    {
                        byte[] eventBodyBytes = eventData.EventBody.ToArray();
                        Console.WriteLine($"Partition {selectedPartition} -- {eventData.Properties["TransactionType"]} Event {Environment.NewLine} {eventData.EventBody} {Environment.NewLine}");
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // This is expected if the cancellation token is
                // signaled.
            }
            finally
            {
                await receiver.CloseAsync();
            }
        }
    }
}
