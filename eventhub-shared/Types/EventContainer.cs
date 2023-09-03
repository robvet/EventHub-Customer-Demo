using eventhub_shared.Contracts;
using eventhub_shared.Enumerations;

namespace eventhub_shared.Types
{
    public class EventContainer
    {
        public EventContainer(TransactionTypeEnum transactionType, ITransactionEvent eventPayload)
        {
            TransactionTypeEnum = transactionType;
            EventPayload = eventPayload;
        }

        public TransactionTypeEnum TransactionTypeEnum { get; set; }
        public ITransactionEvent EventPayload { get; set; }
    }
}
