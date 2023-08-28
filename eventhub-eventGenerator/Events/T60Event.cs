using eventhub_shared.Contracts;

namespace eventhub.producer.Events
{
    /// <summary>
    /// Lottery ticket purchase event
    /// </summary>
    internal class T60Event : ITransactionEvent
    {
        public T60Event(int quantity,
                        decimal price,
                        string lotteryType,
                        string winningNumbers,
                        Guid transactionId,
                        string transactionType,
                        DateTime transactionDate,
                        long transactionCounter)
        {
            TransactionId = Guid.NewGuid();
            TransactionDate = DateTime.UtcNow;
            TransactionType = "T60";
            Quantity = quantity;
            Price = price;
            LotteryType = lotteryType;
            WinningNumbers = winningNumbers;
            TransactionCounter = transactionCounter;
        }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string LotteryType { get; set; }
        public string WinningNumbers { get; set; }
        public Guid TransactionId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public long TransactionCounter { get; set; }
    }
}
