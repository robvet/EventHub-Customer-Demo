using eventhub.producer.Events;
using eventhub_demo_eventIngester.EventIngestion;
using eventhub_shared.Contracts;
using eventhub_shared.Enumerations;
using eventhub_shared.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eventhub.producer.EventGenerator
{
    public class GenerateEvents
    {
        private static long T100Counter = 0;
        private static long T200Counter = 0;
        private static long T300Counter = 0;
        private readonly int numOfEvents = 20;

        private ISendEvent _sendEvent; 
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public GenerateEvents(ILogger logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        //public async Task GenerateEventsAsync(string eventHubName,
        //                                     string eventHubConnectionString,
        //                                     string appInsightsConnectionString,
        //                                     IConfiguration config)
        public async Task GenerateEventsAsync()
        {
            _sendEvent = new SendEvent(_logger, _config);

            for (int i = 1; i <= numOfEvents; i++)
            {
                var transaction = new Random().Next(0, 3);

                switch (transaction)
                {
                    case 0:
                        var T100Event = CreateT100Event();
                        await _sendEvent.SendAsync(
                            new EventContainer(TransactionTypeEnum.Gasoline, T100Event));
                        break;

                    case 1:
                        var T200Event = CreateT200Event();
                        await _sendEvent.SendAsync(
                            new EventContainer(TransactionTypeEnum.Grocery, T200Event));
                        break;

                    case 2:
                        var T300Event = CreateT300Event();
                        await _sendEvent.SendAsync(
                            new EventContainer(TransactionTypeEnum.Lottery, T300Event));
                        break;
                }
            }
        }

        private ITransactionEvent CreateT100Event()
        {
            var eventClass = new GasolineSaleEvent("Diesel",
                                          10,
                                          1.5m,
                                          "Cash",
                                          "123456",
                                          1,
                                          T100Counter++);

            return eventClass;

        }

        private ITransactionEvent CreateT200Event()
        {

            // create products purchased
            var items = new List<Item>()
            {
                new Item("item1", 2, 3.99m),
                new Item("item2", 1, 5.99m)
            };

            var eventClass = new GrocerySaleEvent(9.99m,
                                          12345678,
                                          1,
                                          "Store1",
                                          "John Doe",
                                          true,
                                          Guid.NewGuid(),
                                          "Sale",
                                          DateTime.Now,
                                          T200Counter++,
                                          items);

            return eventClass;
        }

        private ITransactionEvent CreateT300Event()
        {
            var eventClass = new LotterySaleEvent(1,
                                          1.99m,
                                          "Texas Lotto",
                                          "123-456-789",
                                          Guid.NewGuid(),
                                          "Sale",
                                          DateTime.Now,
                                          T300Counter++);

            return eventClass;
        }
    }
}
