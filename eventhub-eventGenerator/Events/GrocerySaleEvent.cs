using eventhub_shared.Contracts;
using eventhub_shared.Enumerations;

namespace eventhub.producer.Events
{
    /// <summary>
    /// Convenience store transaction event
    /// </summary>
    public class GrocerySaleEvent : ITransactionEvent
    {
        public GrocerySaleEvent(decimal totalAmount,
                        long customerId,
                        int paymentMethod,
                        string storeLocation,
                        string clerkName,
                        bool discountApplied,
                        Guid transactionId,
                        string transactionType,
                        DateTime transactionDate,
                        long orderCount,
                        List<Item> items)
        {
            TransactionId = Guid.NewGuid();
            TransactionDate = DateTime.UtcNow;
            TransactionTypeEnum = TransactionTypeEnum.Grocery;
            //TransactionType = TransactionTypeEnum.T200.ToString();
            TotalAmount = totalAmount;
            CustomerId = customerId;
            PaymentMethod = paymentMethod;
            StoreLocation = storeLocation;
            ClerkName = clerkName;
            DiscountApplied = discountApplied;
            OrderCount = orderCount;
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

        //public string TransactionType { get; set; }
        public TransactionTypeEnum TransactionTypeEnum { get; set; }

        public DateTime TransactionDate { get; set; }

        public long OrderCount { get; set; }
    }
}
