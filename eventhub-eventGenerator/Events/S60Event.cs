using eventhub_shared.Contracts;

namespace eventhub.producer.Events
{
    /// <summary>
    /// Convenience store transaction event
    /// </summary>
    public class S60Event : ITransactionEvent
    {
        public S60Event(decimal totalAmount,
                        long customerId,
                        int paymentMethod,
                        string storeLocation,
                        string clerkName,
                        bool discountApplied,
                        Guid transactionId,
                        string transactionType,
                        DateTime transactionDate,
                        long transactionCounter,
                        List<Item> items)
        {
            TransactionId = Guid.NewGuid();
            TransactionDate = DateTime.UtcNow;
            TransactionType = "S60";
            TotalAmount = totalAmount;
            CustomerId = customerId;
            PaymentMethod = paymentMethod;
            StoreLocation = storeLocation;
            ClerkName = clerkName;
            DiscountApplied = discountApplied;
            TransactionCounter = transactionCounter;
            Items = items;
        }

        public decimal TotalAmount { get; set; }

        public long CustomerId { get; set; }

        public int PaymentMethod { get; set; }

        public string StoreLocation { get; set; }

        public string ClerkName { get; set; }

        public bool DiscountApplied { get; set; }

        public List<Item> Items { get; set; }

        public Guid TransactionId { get; set; }

        public string TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public long TransactionCounter { get; set; }
    }
}
