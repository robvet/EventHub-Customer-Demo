using eventhub_shared.Contracts;
using eventhub_shared.Enumerations;

namespace eventhub.producer.Events
{
    /// <summary>
    /// Lottery ticket purchase event
    /// </summary>
    internal class LotterySaleEvent : ITransactionEvent
    {
        public LotterySaleEvent(int quantity,
                        decimal price,
                        string lotteryType,
                        string winningNumbers,
                        Guid transactionId,
                        string transactionType,
                        DateTime transactionDate,
                        long orderCount)
        {
            TransactionId = Guid.NewGuid();
            TransactionDate = DateTime.UtcNow;
            //TransactionType = TransactionTypeEnum.T300.ToString();
            TransactionTypeEnum = TransactionTypeEnum.Lottery;
            Quantity = quantity;
            Price = price;
            LotteryType = lotteryType;
            WinningNumbers = winningNumbers;
            OrderCount = orderCount;
        }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string LotteryType { get; set; }
        public string WinningNumbers { get; set; }
        public Guid TransactionId { get; set; }
        public TransactionTypeEnum TransactionTypeEnum { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public long OrderCount { get; set; }
    }
}
