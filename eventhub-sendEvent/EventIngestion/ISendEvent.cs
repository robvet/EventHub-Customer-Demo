using eventhub_shared.Types;

namespace eventhub_demo_eventIngester.EventIngestion
{
    public interface ISendEvent
    {
        Task SendAsync(EventContainer eventContainer);
    }
}