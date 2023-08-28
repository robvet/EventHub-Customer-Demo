namespace eventhub.producer.Events
{
    public class Item
    {
        public Item(string itemName, int itemQuantity, decimal itemPrice)
        {
            ItemName = itemName;
            ItemQuantity = itemQuantity;
            ItemPrice = itemPrice;
        }

        public string ItemName { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemPrice { get; set; }
    }
}
