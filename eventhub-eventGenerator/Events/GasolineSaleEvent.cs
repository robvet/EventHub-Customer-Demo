using eventhub_shared.Contracts;
using eventhub_shared.Enumerations;

namespace eventhub.producer.Events
{
    /// <summary>
    /// Gasoline transaction event
    /// </summary>
    public class GasolineSaleEvent : ITransactionEvent
    {
        public GasolineSaleEvent(string fuelType,
                        decimal fuelAmount,
                        decimal fuelPrice,
                        string paymentMethod,
                        string receiptNumber,
                        int pumpNumber,
                        long orderCount)
        {
            TransactionId = Guid.NewGuid();
            TransactionDate = DateTime.UtcNow;
            //TransactionType = TransactionTypeEnum.Gasoline;
            TransactionTypeEnum = TransactionTypeEnum.Gasoline;
            FuelType = fuelType;
            FuelAmount = fuelAmount;
            FuelPrice = fuelPrice;
            PaymentMethod = paymentMethod;
            ReceiptNumber = receiptNumber;
            PumpNumber = pumpNumber;
            OrderCount = orderCount;
        }

        public string FuelType { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal FuelPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string ReceiptNumber { get; set; }
        public int PumpNumber { get; set; }
        public Guid TransactionId { get; set; }
        //public string TransactionType { get; set; }
        public TransactionTypeEnum TransactionTypeEnum { get; set; }
        public DateTime TransactionDate { get; set; }
        public long OrderCount { get; set; }
    }
}
