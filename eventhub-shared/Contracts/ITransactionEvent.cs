using eventhub_shared.Enumerations;

namespace eventhub_shared.Contracts
{
    public interface ITransactionEvent
    {
        Guid TransactionId { get; set; }
        TransactionTypeEnum TransactionTypeEnum { get; set; }
        DateTime TransactionDate { get; set; }
        long OrderCount { get; set; }
    }
}
