//using Azure.Core;
//using Azure.Messaging.EventHubs.Consumer;

//In Azure Event Hub, retries for event processing are managed by the consumer's retry policy. When an event processing error occurs, the consumer's retry policy determines how many times the event processing will be retried and the time intervals between retries.

//By default, the Azure SDK for .NET implements an exponential retry policy. This means that it will automatically attempt retries with increasing intervals between each retry. The goal of this approach is to give transient issues, such as network glitches or temporary resource unavailability, a chance to recover without overloading the system.

//You don't need to explicitly configure the retry policy for the consumer when using the Azure SDK for .NET; it's handled internally. However, you can customize the retry policy if needed.

//Here's an example of how you can create a custom retry policy for the EventHubConsumerClient using the RetryOptions class:


//using Azure.Messaging.EventHubs.Consumer;
//using Azure.Core;
//using System;

//class Program
//{
//    static void Main(string[] args)
//    {
//        var retryOptions = new RetryOptions(
//            maximumRetries: 5,
//            delay: TimeSpan.FromSeconds(1),
//            maxDelay: TimeSpan.FromSeconds(30),
//            mode: RetryMode.Exponential);

//        var consumerClient = new EventHubConsumerClient(EventHubConsumerClient.DefaultConsumerGroupName, "YOUR_CONNECTION_STRING", "YOUR_EVENTHUB_NAME", retryOptions);

//        // Rest of your event processing logic
//    }
//}

//maximumRetries sets the maximum number of retry attempts before giving up.
//delay is the initial delay before the first retry.
//maxDelay is the maximum delay between retries.
//mode specifies the retry mode, in this case, it's set to Exponential.

//Remember to replace "YOUR_CONNECTION_STRING" and "YOUR_EVENTHUB_NAME" with your actual Event Hub connection string and name.

//It's important to consider the trade-off between retries and dead-lettering. While retries can help handle transient issues, there should be a limit. If an event consistently fails processing, it's recommended to move it to a Dead-Letter Queue after a certain number of retries to prevent a continuous processing loop and allow for manual intervention if needed.