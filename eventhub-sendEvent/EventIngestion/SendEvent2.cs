using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventhub_demo_eventIngester.EventIngestion
{
    //public class SendEvent2
    //{
    //    private static EventHubBufferedProducerClient producer;

    //    public async Task SendEvents(List<EventData> events)
    //    {
    //        if (producer == null)
    //        {
    //            var credential = new DefaultAzureCredential();
    //            var clientOptions = new EventHubBufferedProducerClientOptions();
    //            clientOptions.ConnectionOptions.TransportType = EventHubsTransportType.AmqpWebSockets;
    //            var connectionString = "<<Event Hub connection string>>";
    //            var eventHubName = "<<Event Hub name>>";

    //            producer = new EventHubBufferedProducerClient(connectionString, eventHubName, clientOptions);
    //        }

    //        var optionsList = new List<EnqueueOptions>();
    //        foreach (var evt in events)
    //        {
    //            optionsList.Add(new EnqueueOptions());
    //        }

    //        await producer.SendAsync(events, optionsList);
    //    }
    //}
}
