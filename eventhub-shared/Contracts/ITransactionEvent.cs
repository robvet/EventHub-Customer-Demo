namespace eventhub_shared.Contracts
{
    public interface ITransactionEvent
    {
        Guid TransactionId { get; set; }
        string TransactionType { get; set; }
        DateTime TransactionDate { get; set; }
        long OrderCount { get; set; }
    }
}
