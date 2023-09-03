using eventhub_shared.Enumerations;

namespace eventhub_shared.PartitionManager
{
    public static class PartitionMapper
    {
        public static string MapPartition(TransactionTypeEnum transactionType)
        {
            var partitionId = "0";

            switch (transactionType)
            {
                case TransactionTypeEnum.Gasoline:
                    partitionId = "0";
                    break;

                case TransactionTypeEnum.Grocery:
                    partitionId = "1";
                    break;
                case TransactionTypeEnum.Lottery:
                    partitionId = "2";
                    break;

                default:
                    partitionId = "0";
                    break;
            }

            return partitionId;
        }
    }
}
