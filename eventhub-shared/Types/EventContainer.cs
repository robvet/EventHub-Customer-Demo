using eventhub_shared.Contracts;

namespace eventhub_shared.Types
{
    public class EventContainer
    {
        public EventContainer(string transactionType, ITransactionEvent eventPayload)
        {
            TransactionType = transactionType;
            EventPayload = eventPayload;
        }

        public string TransactionType { get; set; }
        public ITransactionEvent EventPayload { get; set; }
    }
}
