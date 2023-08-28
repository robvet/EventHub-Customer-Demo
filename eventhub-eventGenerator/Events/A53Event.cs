using eventhub_shared.Contracts;

namespace eventhub.producer.Events
{
    /// <summary>
    /// Gasoline transaction event
    /// </summary>
    public class A53Event : ITransactionEvent
    {
        public A53Event(string fuelType,
                        decimal fuelAmount,
                        decimal fuelPrice,
                        string paymentMethod,
                        string receiptNumber,
                        int pumpNumber,
                        long transactionCounter)
        {
            TransactionId = Guid.NewGuid();
            TransactionDate = DateTime.UtcNow;
            TransactionType = "A53";
            FuelType = fuelType;
            FuelAmount = fuelAmount;
            FuelPrice = fuelPrice;
            PaymentMethod = paymentMethod;
            ReceiptNumber = receiptNumber;
            PumpNumber = pumpNumber;
            TransactionCounter = transactionCounter;
        }

        public string FuelType { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal FuelPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string ReceiptNumber { get; set; }
        public int PumpNumber { get; set; }
        public Guid TransactionId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public long TransactionCounter { get; set; }
    }
}
