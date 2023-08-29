using eventhub.producer.Events;
using eventhub_demo_eventIngester.EventIngestion;
using eventhub_shared.Contracts;
using eventhub_shared.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eventhub.producer.EventGenerator
{
    public class GenerateEvents
    {
        private static long A53Counter = 0;
        private static long S60Counter = 0;
        private static long T60Counter = 0;
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
                        var a53Event = CreateA53Event();
                        await _sendEvent.SendAsync(new EventContainer("A53", a53Event));
                        break;
                    case 1:
                        var s60Event = CreateS60Event();
                        await _sendEvent.SendAsync(new EventContainer("S60", s60Event));
                        break;
                    case 2:
                        var t60Event = CreateT60Event();
                        await _sendEvent.SendAsync(new EventContainer("T60", t60Event));
                        break;
                }
            }
        }

        private ITransactionEvent CreateA53Event()
        {
            var eventClass = new A53Event("Diesel",
                                          10,
                                          1.5m,
                                          "Cash",
                                          "123456",
                                          1,
                                          A53Counter++);

            return eventClass;

        }

        private ITransactionEvent CreateS60Event()
        {

            // create products purchased
            var items = new List<Item>()
            {
                new Item("item1", 2, 3.99m),
                new Item("item2", 1, 5.99m)
            };

            var eventClass = new S60Event(9.99m,
                                          12345678,
                                          1,
                                          "Store1",
                                          "John Doe",
                                          true,
                                          Guid.NewGuid(),
                                          "Sale",
                                          DateTime.Now,
                                          S60Counter++,
                                          items);

            return eventClass;
        }

        private ITransactionEvent CreateT60Event()
        {
            var eventClass = new T60Event(1,
                                          1.99m,
                                          "Texas Lotto",
                                          "123-456-789",
                                          Guid.NewGuid(),
                                          "Sale",
                                          DateTime.Now,
                                          T60Counter++);

            return eventClass;
        }
    }
}
